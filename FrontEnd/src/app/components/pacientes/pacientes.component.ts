import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Caso, Cliente, Departamento, Municipio, Paciente } from 'src/app/services/api-backend';
import { DepartamentosMunicipiosService } from 'src/app/services/api-backend/api/departamentosMunicipios.service';
import { PacientesService } from 'src/app/services/api-backend/api/pacientes.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-pacientes',
  templateUrl: './pacientes.component.html',
  styleUrls: ['./pacientes.component.scss']
})
export class PacientesComponent implements OnInit {
  dataDepartamentos:Departamento[]=[];
  dataMunicipios:Municipio[]=[];
  departamento: string='-';
  municipio: string='-';
  nombresPaciente:string='';
  apellidosPaciente:string='';
  dpi:string='';
  fechaNacimiento:string=''
  direccion:string=''
  constructor(private servicioDepMun:DepartamentosMunicipiosService,
    private pacienteService:PacientesService) { }

  ngOnInit(): void {

    this.cargarDepartamentos();
  }

  cargarDepartamentos(){
    this.servicioDepMun.departamentosMunicipiosGetDepartamentosGet()
    .subscribe(res =>{
      this.dataDepartamentos = <Departamento[]>res
      console.log(this.dataDepartamentos)
    }, error=>{
      console.log(error);
    }
    )
  }
  cargarMunicipio(codDepartamento:string){
    if(codDepartamento !='-'){
      this.servicioDepMun.departamentosMunicipiosGetMunicipiosCodDepartentoGet(Number.parseInt(codDepartamento))
      .subscribe(res =>{
        this.dataMunicipios = <Municipio[]>res

      },error=>{
        console.log(error)
      }
      )
    }
  }
  cambioDepartamento(){
    console.log(this.departamento)
    this.cargarMunicipio(this.departamento)
  }

  crearPaciente(){
    let paciente:Paciente={
      nombres:this.nombresPaciente,
      apellidos:this.apellidosPaciente,
      dpi:this.dpi,
      fechaNacimiento:this.fechaNacimiento,
      direccion:this.direccion,
      idMunicipio:Number.parseInt(this.municipio),
    }
    Swal.fire({
      title:'Desea crear el paciente',
      icon:'question',
      showCancelButton:true,
      cancelButtonColor:'red',
      cancelButtonText:'No, cancelar',
      confirmButtonColor:'green',
      confirmButtonText:'Si, continuar'
    }).then(res =>{
      if(res.isConfirmed){
        this.pacienteService.pacientesCrearPacientePost(paciente)
        .subscribe(result =>{
          if(result.estado){
            Swal.fire({
              icon:'success',
              text:'Pacient creado correctamente'
            })
            this.limpiarDatosCrearPaciente()
          }
        })
      }
    })
  }
  limpiarDatosCrearPaciente(){
    this.departamento=''
    this.municipio=''
    this.nombresPaciente=''
    this.apellidosPaciente=''
    this.dpi=''
    this.fechaNacimiento=''
    this.direccion=''
  }

  //-------------------Creacion de caso
  nombreBusqueda:string=''
  noSeleccionado:boolean= true
  dataPaciente:Paciente[]=[]
  dataSourcePaciente:MatTableDataSource<Paciente> = new MatTableDataSource(this.dataPaciente)
  @ViewChild(MatPaginator) paginatorPaciente:MatPaginator;
  @ViewChild(MatTable) TablaPacientesCrearCaso:MatTable<Paciente>
  columnaTablaPacientes:String[] = ['nombre','apellido', 'direccion','accion']
  pacienteSeleccionado:Paciente
  fechaCasoNuevo:string=''
  viendoHistorial:boolean=true
  buscarPaciente(){
    this.pacienteService.pacientesGetPacienteGet(this.nombreBusqueda)
    .subscribe(res =>{
      this.dataPaciente = <Paciente[]>res
      this.dataSourcePaciente.data =this.dataPaciente
      console.log(this.dataPaciente)
      setTimeout(() => {
        this.dataSourcePaciente.paginator = this.paginatorPaciente
      }, 50);
    })
  }

  SeleccionarPacienteCrearCaso(pacienteSelected:Paciente){
    this.noSeleccionado= false
    this.viendoHistorial= false
    this.pacienteSeleccionado = pacienteSelected
  }
  regresar(){
    this.noSeleccionado= true
    this.viendoHistorial = false
  }
  crarCaso(){
    let caso:Caso={
      idPaciente:this.pacienteSeleccionado.idPaciente,
      fechaInicio:this.fechaCasoNuevo
    }
    Swal.fire({
      title:'Desea crear el paciente',
      icon:'question',
      showCancelButton:true,
      cancelButtonColor:'red',
      cancelButtonText:'No, cancelar',
      confirmButtonColor:'green',
      confirmButtonText:'Si, continuar'
    }).then(res =>{
      if(res.isConfirmed){
        this.pacienteService.pacientesCrearCasoPost(caso)
        .subscribe(result =>{
          if(result.estado){
            Swal.fire({
              icon:'success',
              text:'Caso creado correctamente'
            })
            this.fechaCasoNuevo = ''
            this.noSeleccionado=true
          }
        },error =>{
          Swal.fire({
            icon:'error',
            text:error.error.error
          })
        })
      }
    })

    
    
  }
  dataCasos:Caso[]=[]
  dataSourceCasos:MatTableDataSource<Caso> = new MatTableDataSource(this.dataCasos)
  @ViewChild(MatPaginator) paginatorCasos:MatPaginator;
  @ViewChild(MatTable) tablaVerCasos:MatTable<Caso>
  columnaTablaCasos:String[] = ['fechaInicio','fechaFin','motivo','accion']
  CasosPaciente(paciente:Paciente){
    this.noSeleccionado = false
    this.viendoHistorial = true
    this.dataCasos = paciente.casos
    this.dataSourceCasos.data= this.dataCasos
    setTimeout(() => {
      this.dataSourceCasos.paginator = this.paginatorCasos
    }, 50);
  }
  async CerrarCaso(caso:Caso){
    const { value: formValues } = await Swal.fire({
      html:
    '<label for="swal-input1">Motivo </label>' +
    '<input id="swal-input1" type="text" class="swal2-input">' +
    '<br>' +
    '<label for="swal-input1">Fecha Finalizacion</label>' +
    '<input id="swal-input2" type="date" class="swal2-input">',
    focusConfirm: false,
    preConfirm: () => {
      return [
        (<HTMLInputElement>document.getElementById('swal-input1')).value,
        (<HTMLInputElement>document.getElementById('swal-input2')).value
      ]
    }
    })
    if (formValues) {
      if(formValues[0] ==''){
        Swal.fire({
          title:'Error',
          icon:'error',
          text:'No se espeficivo el motivo de finalizacion'
        })
        return
      }
      if(formValues[1] ==''){
        Swal.fire({
          title:'Error',
          icon:'error',
          text:'No se espeficivo la fecha de finalizacion'
        })
        return
      }
      caso.fechaFin = formValues[1]
      caso.motivoFinalizacion = formValues[0]
      this.pacienteService.pacientesCerrarCasoPost(caso)
      .subscribe(res =>{
        if(res.estado){
          Swal.fire({
            icon:'success',
            text:'Caso cerrado correctamente'
          })
          this.noSeleccionado=true
          this.viendoHistorial = false
        }
      })
    }
  }
}

import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Cita, Clinica, Paciente, Sucursale } from 'src/app/services/api-backend';
import { CamaService } from 'src/app/services/api-backend/api/cama.service';
import { CitasService } from 'src/app/services/api-backend/api/citas.service';
import { ClinicaService } from 'src/app/services/api-backend/api/clinica.service';
import { PacientesService } from 'src/app/services/api-backend/api/pacientes.service';
import { SucursalesService } from 'src/app/services/api-backend/api/sucursales.service';
import { SharedDataService } from 'src/app/services/utils/shared-data.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-citas',
  templateUrl: './citas.component.html',
  styleUrls: ['./citas.component.scss']
})
export class CitasComponent implements OnInit {
  idCaso:number=0
  paciente:Paciente
  nombrePacientes:string=''
  dataPacientes:any[]=[]
  dataSourcePacientes:MatTableDataSource<any>
  @ViewChild(MatPaginator) paginatorPacientes:MatPaginator
  @ViewChild(MatTable) tablaPacientes:MatTable<any>
  columnasTablaPacientes:string[]=['nombre','apellido','accion']
  
  idSucursal: string="-"
  dataSucursal:Sucursale[]=[]
  dataClinicas:Clinica[]=[]
  idClinica: string= '-'
  fechaCita:string= ''
  
  //busqueda de citas
  idCaso1:number=0
  paciente1:Paciente
  nombrePacientes1:string=''
  dataPacientes1:any[]=[]
  dataSourcePacientes1:MatTableDataSource<any>
  @ViewChild('paginatorLegal') paginatorPacientes1:MatPaginator
  @ViewChild(MatTable) tablaPacientes1:MatTable<any>
  columnasTablaPacientes1:string[]=['nombre','apellido','accion']


  dataCitasEncontradas:Cita[]=[];
  dataSourceCitasEncontradas:MatTableDataSource<Cita>
  @ViewChild('paginarotCitas') paginatorCitas:MatPaginator
  @ViewChild(MatTable) tablacitas:MatTable<any>
  columnasCitas:string[]=['Fecha','Sucursal','Clinica','accion']

  constructor(private servicioPacientes: PacientesService,
    private sucursaleservices: SucursalesService, 
    private clinicasService:ClinicaService,
    private citasService:CitasService,
    private router:Router,
    private ShareData:SharedDataService
    ) { }


  ngOnInit(): void {
    this.cargarSucursales()
  }

  buscarNombrePaciente(){
    this.servicioPacientes.pacientesBuscarPacienteCasoAbiertoGet(this.nombrePacientes)
    .subscribe(result => {
    this.dataPacientes = <any> result
    this.dataSourcePacientes = new MatTableDataSource(this.dataPacientes)
  setTimeout(() => (
    this.dataSourcePacientes.paginator = this.paginatorPacientes
  ),50)
    },error =>{
      console.log(error)
     })
  }

  buscarNombrePaciente1(){
    this.servicioPacientes.pacientesBuscarPacienteCasoAbiertoGet(this.nombrePacientes1)
    .subscribe(result => {
    this.dataPacientes1 = <any> result
    this.dataSourcePacientes1 = new MatTableDataSource(this.dataPacientes1)
  setTimeout(() => (
    this.dataSourcePacientes1.paginator = this.paginatorPacientes1
  ),50)
    },error =>{
      console.log(error)
     })
  }

  seleccionarPaciente1(element:any){
    this.idCaso1 = <number>element.idCaso
    this.paciente1 = <Paciente>element.paciente
    console.log(this.idCaso1,this.paciente1)
    this.citasService.citasHistorialCitasPacientePost(this.paciente1.idPaciente)
    .subscribe(res =>{
      this.dataCitasEncontradas = <Cita[]>res
      this.dataSourceCitasEncontradas = new MatTableDataSource(this.dataCitasEncontradas)
      setTimeout(() => (
        this.dataSourceCitasEncontradas.paginator = this.paginatorCitas
      ),50)
      
    })
    }

  seleccionarPaciente(element:any){
    this.idCaso = <number>element.idCaso
    this.paciente = <Paciente>element.paciente
    console.log(this.idCaso,this.paciente)
    }

  cargarSucursales(){
      this.sucursaleservices.sucursalesGetSucursalesGet()
      .subscribe(result => {
        this.dataSucursal = <Sucursale[]>result
        console.log(this.dataSucursal)  
  },error =>{
    Swal.fire({
      title:'Error',
      icon:'error'
    })
  })}

  cargarClinicas(){
    if(this.idSucursal == '-'){
      return
    }
    this.dataClinicas = []
    this.clinicasService.clinicaGetClinicasSucursalGet(Number.parseInt(this.idSucursal))
    .subscribe(res =>{
      this.dataClinicas = <Clinica[]>res
      console.log(this.dataClinicas)
    })          
  }

  AgendarCita(){
    let Cita:Cita={
      idCaso:this.idCaso,
      idClinica:Number.parseInt(this.idClinica),
      fechaCita:this.fechaCita
    }
    Swal.fire({
      title:'Desea Asignar la cita?',
      icon:'question',
      showCancelButton:true,
    }).then(res =>{
      
      if(res.isConfirmed){
        this.citasService.citasCrearCitaPost(Cita)
      .subscribe(result =>{
        Swal.fire({
          title:"Cita agendada correctamente!",
          icon:'success'
        })
        this.fechaCita =''
        this.paciente= null
        this.idClinica = '-'
      }, error =>{
        console.log(error)
      })
        
      }
    })
  }


  Selecionarcita(element:Cita){
    this.ShareData.shareData({
      value: element,
      type: 'Cita'
    })
    this.router.navigateByUrl('IngresarCita')
  }

}

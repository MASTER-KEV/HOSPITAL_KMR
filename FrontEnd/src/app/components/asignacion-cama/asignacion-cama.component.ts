import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { AsignacionesCama, Cama, Habitacione, Paciente, Sucursale } from 'src/app/services/api-backend';
import { CamaService } from 'src/app/services/api-backend/api/cama.service';
import { PacientesService } from 'src/app/services/api-backend/api/pacientes.service';
import { SucursalesService } from 'src/app/services/api-backend/api/sucursales.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-asignacion-cama',
  templateUrl: './asignacion-cama.component.html',
  styleUrls: ['./asignacion-cama.component.scss']
})
export class AsignacionCamaComponent implements OnInit {
  idCaso:number=0
  paciente:Paciente
  nombrePacientes:string=''
  dataPacientes:any[]=[]
  dataSourcePacientes:MatTableDataSource<any>
  @ViewChild(MatPaginator) paginatorPacientes:MatPaginator
  @ViewChild(MatTable) tablaPacientes:MatTable<any>
  columnasTablaPacientes:string[]=['nombre','apellido','accion']

  idCama='-'
  idTipoDeHabitacion: string="1"
  idSucursal: string="-"
  dataSucursal:Sucursale[]=[]
  dataHabitacion:Habitacione[]=[]
  DataCamas:Cama[]=[]
  constructor(private servicioPacientes: PacientesService,
              private servicioSucursales: SucursalesService,
              private camaservices: CamaService) { }

  ngOnInit(): void {
    this.cargarSucursales();
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

  seleccionarPaciente(element:any){
    this.idCaso = <number>element.idCaso
    this.paciente = <Paciente>element.paciente
    console.log(this.idCaso,this.paciente)
    }
    cargarSucursales(){
      this.servicioSucursales.sucursalesGetSucursalesGet()
      .subscribe(result => {
        this.dataSucursal = <Sucursale[]>result
        console.log(this.dataSucursal)  
      })}
  
    
  cargarHabitaciones(){
    if(this.idSucursal=="-"){ 
      return 
    }
    this.camaservices.camaGetHabitacionesSucursalGet(Number.parseInt(this.idSucursal))
    .subscribe(result => {
      this.dataHabitacion = <Habitacione[]>result
      console.log(this.dataHabitacion)  
    })}

    cargarCamas(){
      if(this.idTipoDeHabitacion=="-"){ 
        return 
      }
      this.idCama='-'
      this.DataCamas = []
      this.camaservices.camaGetCamasSucursalPost(Number.parseInt(this.idTipoDeHabitacion))
      .subscribe(res => {
          let Dataetmp:Cama[]=<Cama[]>res
          Dataetmp.forEach(element => {
            if(element.asignacionesCamas.length ==0){
              this.DataCamas.push(element)
            }
        });
      })
    }

    asignar(){
      Swal.fire({
        title:'Desea realizar la asignacion?',
        icon:'question',
        showCancelButton:true
      }).then(res =>{
        if(res.isConfirmed){
          let asignacion:AsignacionesCama={
            idCaso: this.idCaso,
            idCama: Number.parseInt(this.idCama)
          }
          this.camaservices.camaAsingarCamaPost(asignacion)
          .subscribe(ress=>{
            if(ress){
              Swal.fire({
                title:'Cama asignada correctametne',
                icon:'success'
              })
              this.paciente = null
            }
          })
        }
      })
    }
}

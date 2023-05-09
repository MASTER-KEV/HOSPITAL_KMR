import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Cita,Clinica, Paciente, Sucursale } from 'src/app/services/api-backend';
import { CitasService } from 'src/app/services/api-backend/api/citas.service';
import { ClinicaService } from 'src/app/services/api-backend/api/clinica.service';
import { PacientesService } from 'src/app/services/api-backend/api/pacientes.service';
import { SucursalesService } from 'src/app/services/api-backend/api/sucursales.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-citas-ofline',
  templateUrl: './citas-ofline.component.html',
  styleUrls: ['./citas-ofline.component.scss']
})
export class CitasOflineComponent implements OnInit {


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

  idCaso1:number=0
  paciente1:Paciente
  nombrePacientes1:string=''
  dataPacientes1:any[]=[]
  dataSourcePacientes1:MatTableDataSource<any>
  @ViewChild('paginatorLegal') paginatorPacientes1:MatPaginator
  @ViewChild(MatTable) tablaPacientes1:MatTable<any>
  columnasTablaPacientes1:string[]=['nombre','apellido','accion']
  constructor(
    private servicioPacientes: PacientesService,
    private sucursaleservices: SucursalesService, 
    private clinicasService:ClinicaService,
    private citasService:CitasService,
    private router:Router,
  ) { }

  ngOnInit(): void {
    this.cargarSucursales()
  }

  cargarSucursales(){
    this.sucursaleservices.sucursalesGetSucursalesGet()
    .subscribe(result => {
      this.dataSucursal = <Sucursale[]>result
      console.log(this.dataSucursal)  
})}


cargarClinicas(){
  if(this.idSucursal == '-'){
    return
  }
  this.dataClinicas = []
  this.dataSucursal.forEach(e =>{
    if(e.idSucursal == Number.parseInt(this.idSucursal)){
      this.dataClinicas = e.clinicas
    }
  })          
}
  buscarNombrePaciente(){
    this.servicioPacientes.pacientesCasoAbierto()
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
}


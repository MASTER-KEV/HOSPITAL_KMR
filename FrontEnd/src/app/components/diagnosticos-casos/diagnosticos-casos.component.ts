import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Diagnostico, DiagnosticosCaso, Paciente } from 'src/app/services/api-backend';
import { DiagnosticosService } from 'src/app/services/api-backend/api/diagnosticos.service';
import { PacientesService } from 'src/app/services/api-backend/api/pacientes.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-diagnosticos-casos',
  templateUrl: './diagnosticos-casos.component.html',
  styleUrls: ['./diagnosticos-casos.component.scss']
})
export class DiagnosticosCasosComponent implements OnInit {
idCaso:number=0
paciente:Paciente
nombrePacientes:string=''
dataPacientes:any[]=[]
dataSourcePacientes:MatTableDataSource<any>
@ViewChild(MatPaginator) paginatorPacientes:MatPaginator
@ViewChild(MatTable) tablaPacientes:MatTable<any>
columnasTablaPacientes:string[]=['nombre','apellido','accion']

Diagnostico:Diagnostico
nombreDiagnosticos:string=''
dataDiagnosticos:Diagnostico[]=[]
dataSourceDiagnostico:MatTableDataSource<Diagnostico>
@ViewChild(MatPaginator) paginatorDiagnosticos:MatPaginator
@ViewChild(MatTable) tableDiagnosticos:MatTable<Diagnostico>
columnasTablaDiagnosticos:string[]=['nombre','accion']

constructor(private servicioPacientes: PacientesService, 
  private servicioDiagnosticos: DiagnosticosService ) { }
  
  ngOnInit(): void {
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

  buscarDiagnostico(){
    this.servicioDiagnosticos.diagnosticosGetDiagnosticosPost(this.nombreDiagnosticos)
    .subscribe(result =>{
    this.dataDiagnosticos = <Diagnostico[]> result
    this.dataSourceDiagnostico = new MatTableDataSource(this.dataDiagnosticos)
  setTimeout(() => (
    this.dataSourceDiagnostico.paginator = this.paginatorDiagnosticos
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

  seleccionarDiagnostico(element:Diagnostico){
    this.Diagnostico = element
    console.log(this.Diagnostico)
    }

  cargarDiagnosticoCasos(){
  let diagnosticoCaso:DiagnosticosCaso={
    idCaso:this.idCaso,
    idDiagnostico:this.Diagnostico.idDiagnostico,
    observaciones:'diagnostico nuevo'
  }
  this.servicioDiagnosticos.diagnosticosAgregarDiagnosticoPost(diagnosticoCaso)
  .subscribe(res =>{
    Swal.fire({
      icon:'success',
      text:'Diagnostico agreagado correctamente'
    })
    this.paciente=null
    this.Diagnostico=null
  }
    )
  }

}

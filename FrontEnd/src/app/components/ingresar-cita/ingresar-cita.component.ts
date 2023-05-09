import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Cita, Diagnostico, Examene, ExamenesCaso, MedicamentosCaso, MedicamentosRecetum, Producto, Receta } from 'src/app/services/api-backend';
import { DiagnosticosService } from 'src/app/services/api-backend/api/diagnosticos.service';
import { ExamenesService } from 'src/app/services/api-backend/api/examenes.service';
import { ProductosService } from 'src/app/services/api-backend/api/productos.service';
import { DiagnosticosCitum } from 'src/app/services/api-backend';
import Swal from 'sweetalert2';
import { SharedDataService } from 'src/app/services/utils/shared-data.service';
import { CitasService } from 'src/app/services/api-backend/api/citas.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ingresar-cita',
  templateUrl: './ingresar-cita.component.html',
  styleUrls: ['./ingresar-cita.component.scss']
})
export class IngresarCitaComponent implements OnInit {
  CitaGeneral:Cita;
  constructor(private servicioDiagnosticos: DiagnosticosService,
              private servicioExamenes:ExamenesService,
              private productosService:ProductosService,
              private ShareData:SharedDataService,
              private CitasCervice:CitasService,
              private router:Router) { }
  
  nombreDiagnosticos:string=''
  Observaciones:string=''
  dataDiagnosticos:Diagnostico[]=[]
  dataSourceDiagnostico:MatTableDataSource<Diagnostico>
  @ViewChild('paginatorbusquedaDiags') paginatorDiagnosticos:MatPaginator
  @ViewChild(MatTable) tableDiagnosticos:MatTable<Diagnostico>
  columnasTablaDiagnosticos:string[]=['nombre','accion']

  nombreDiagnosticos1:string=''
  dataDiagnosticos1:Diagnostico[]=[]
  dataSourceDiagnostico1:MatTableDataSource<Diagnostico>
  @ViewChild('paginatorbusquedaDiags') paginatorDiagnosticos1:MatPaginator
  @ViewChild(MatTable) tableDiagnosticos1:MatTable<Diagnostico>
  columnasTablaDiagnosticos1:string[]=['nombre','accion']


  nombreExamen:string=''
  dataExamenes:Examene[]=[]
  dataSourceExamenes:MatTableDataSource<Diagnostico>
  @ViewChild('paginatorbusquedaex') paginatorExamen:MatPaginator
  @ViewChild(MatTable) tableExamenes:MatTable<Diagnostico>
  columnasTablaExamenes:string[]=['nombre','accion']

  nombreExamen1:string=''
  dataExamenes1:Examene[]=[]
  dataSourceExamenes1:MatTableDataSource<Diagnostico>
  @ViewChild('paginatorbusquedaex1') paginatorExamen1:MatPaginator
  @ViewChild(MatTable) tableExamenes1:MatTable<Diagnostico>
  columnasTablaExamenes1:string[]=['nombre','accion']


  nombreProducto:string=''
  dataProductos:Producto[]=[]
  dataSourceProductos:MatTableDataSource<Diagnostico>
  @ViewChild('paginatorprods') paginatorProductos:MatPaginator
  @ViewChild(MatTable) tablaProductos:MatTable<Diagnostico>
  columnaProductos:string[]=['nombre','accion']

  nombreProducto1:string=''
  dataProductos1:Producto[]=[]
  dataSourceProductos1:MatTableDataSource<Diagnostico>
  @ViewChild('paginatorprods') paginatorProductos1:MatPaginator
  @ViewChild(MatTable) tablaProductos1:MatTable<Diagnostico>
  columnaProductos1:string[]=['nombre','accion']
  ngOnInit(): void {
    this.ShareData.shared$.subscribe(res => {
      this.CitaGeneral = <Cita> res.value 
    })
    console.log(this.CitaGeneral)
  }

  buscarExamen(){
    this.servicioExamenes.examenesGetExamenesGet(this.nombreExamen)
    .subscribe(res =>{
      this.dataExamenes = <Examene[]>res
      this.dataSourceExamenes = new MatTableDataSource(this.dataExamenes)
      setTimeout(() => {
        this.dataSourceExamenes.paginator = this.paginatorExamen
      }, 50);
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
  seleccionarExamen(element:Examene){
    this.dataExamenes = []
    this.dataExamenes1.push(element)
    this.dataSourceExamenes1 = new MatTableDataSource(this.dataExamenes1)
    setTimeout(() => {
      this.dataSourceExamenes1.paginator = this.paginatorDiagnosticos1
    }, 50);
  }
  seleccionarDiagnostico(element:Diagnostico){
    this.dataDiagnosticos= []
    this.dataDiagnosticos1.push(element)
    this.dataSourceDiagnostico1 = new MatTableDataSource(this.dataDiagnosticos1)
    setTimeout(() => (
      this.dataSourceDiagnostico1.paginator = this.paginatorDiagnosticos1
    ),50)
  }

  SeleccionarProducto(element:Producto){
    this.dataProductos= []
    this.dataProductos1.push(element)
    this.dataSourceProductos1 = new MatTableDataSource(this.dataProductos1)
    setTimeout(() => (
      this.dataSourceProductos1.paginator = this.paginatorProductos1
    ),50)
  }

  eliminardiag(element:Diagnostico){
    this.dataDiagnosticos1 = this.removeItemFromArr(this.dataDiagnosticos1,element)
    this.dataSourceDiagnostico1 = new MatTableDataSource(this.dataDiagnosticos1)
    setTimeout(() => (
      this.dataSourceDiagnostico1.paginator = this.paginatorDiagnosticos1
    ),50)
  }
  eliminarExamen(elemetn:Examene){
    this.dataExamenes1 = this.removeItemFromArr(this.dataExamenes1, elemetn)
    this.dataSourceExamenes1 = new MatTableDataSource(this.dataExamenes1)
    setTimeout(() => {
      this.dataSourceExamenes1.paginator = this.paginatorDiagnosticos1
    }, 50);
  }

  eliminarProducto(elemetn:Producto){
    this.dataProductos1 = this.removeItemFromArr(this.dataProductos1, elemetn)
    this.dataSourceProductos1 = new MatTableDataSource(this.dataProductos1)
    setTimeout(() => {
      this.dataSourceProductos1.paginator = this.paginatorProductos1
    }, 50);
  }
  removeItemFromArr( arr, item ) {
    return arr.filter( function( e ) {
        return e !== item;
    } );
  }
  buscarProducto(){
    this.productosService.buscarProductoGet(this.nombreProducto)
    .subscribe(res =>{
      this.dataProductos = <Producto[]>res
      this.dataSourceProductos = new MatTableDataSource(this.dataProductos)
      setTimeout(() => {
        this.dataSourceProductos.paginator = this.paginatorProductos
      }, 50);
    })
  }

  GuardarCita(){
    // let citaTemp:Cita = ...this.CitaGeneral;
    Swal.fire({
      title:'Desea registrar la cita?',
      icon:'question',
      showCancelButton:true
    }).then(res =>{
      if(res.isConfirmed){
        this.dataDiagnosticos1.forEach(e =>{
          let diag:DiagnosticosCitum={
            idCita: this.CitaGeneral.idCita,
            idDiagnostico: e.idDiagnostico
          }
          this.CitaGeneral.diagnosticosCita.push(diag)
        })
        this.dataExamenes1.forEach(e =>{
          let examne:ExamenesCaso={
            idExamen:e.idExamen,
            idCita:this.CitaGeneral.idCita
          }
          this.CitaGeneral.examenesCasos.push(examne)
        })
        let receta:Receta={
          idCita:this.CitaGeneral.idCita,
          medicamentosReceta:[]
        }
        this.dataProductos1.forEach(e =>{
          let Med:MedicamentosRecetum={
            idProducto:e.idProducto,
          }
          receta.medicamentosReceta.push(Med)
        })
        this.CitaGeneral.receta.push( receta)

        this.CitasCervice.citasGuardarCitaPost(this.CitaGeneral)
        .subscribe(resultados =>{
          if(resultados){
            Swal.fire({
              title:"Cita Ingresada Correctamente",
              icon:'success'
            }).then(r=>{
              this.router.navigateByUrl('/')
            })
          }
        })
      }
    })
  }

}

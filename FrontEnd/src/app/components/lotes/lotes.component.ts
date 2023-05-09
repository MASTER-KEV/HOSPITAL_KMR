import { Component, OnInit } from '@angular/core';
import { Bodega, Lote, Producto, Sucursale } from 'src/app/services/api-backend';
import { BodegasLotesService } from 'src/app/services/api-backend/api/bodegasLotes.service';
import { ProductosService } from 'src/app/services/api-backend/api/productos.service';
import { SucursalesService } from 'src/app/services/api-backend/api/sucursales.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-lotes',
  templateUrl: './lotes.component.html',
  styleUrls: ['./lotes.component.scss']
})
export class LotesComponent implements OnInit {
  producto:string='-';
  sucursal: string='-';
  bodega:string='-';
  existencia: number=0;
  estado: string='A';
  preciounitario: number=0;
  marca: string='-';
  fechaingreso: string=' ';
  fechacaducidad: string=' ';

  //variables para los select
  dataProductos:Producto[]=[];
  dataBodegas: Bodega []=[];
  dataSucursales: Sucursale[]=[];
  constructor(private serviciobodegasLotes: BodegasLotesService, private servicioproductos: ProductosService, private servicioSucursales: SucursalesService) { }

  ngOnInit(): void {
    this.cargarProductos()
    this.cargarSucursales()
    this.limpiarPantalla()
  }
  cargarProductos(){
    this.servicioproductos.listarProductosGet()
    .subscribe(res=>{
      this.dataProductos = <Producto[]>res
      console.log(this.dataProductos)
    },error=>{
      console.log(error);
    })
    }
  cargarBodegas(idSucursal: string){
    console.log(idSucursal)
    if( idSucursal !='-'){
      this.serviciobodegasLotes.bodegasLotesListarBodegasSucursalGet(Number.parseInt(idSucursal))
    .subscribe(res =>{
      this.dataBodegas= <Bodega[]>res
      console.log(this.dataBodegas)
    }, error=>{
      console.log(error)
    })
    } 
}
   cargarSucursales(){
    this.servicioSucursales.sucursalesGetSucursalesGet()
    .subscribe(res=>{
      this.dataSucursales = <Sucursale[]>res
      console.log(this.dataSucursales)
    },error=>{
      console.log(error);
    
    })
   }
cambioBodega(){
  this.cargarBodegas(this.sucursal)
  }



  crearLote(){
    Swal.fire({
      icon: 'question',
      title: 'Desea crear el lote?',
      showCancelButton:true,
    }).then(res =>{
      if(res.isConfirmed){
        let loteNuevo:Lote={
          idProducto: Number.parseInt(this.producto),
          idBodega: Number.parseInt(this.bodega),
          exitencia: 0,
          estado: 'A',
          precioUnitario: 0,
          marca: '-',
          fechaIngreso: this.fechaingreso,
          fechaCaducidad: this.fechacaducidad
          
        }
        console.log(loteNuevo)
        this.serviciobodegasLotes.bodegasLotesCrearLotePost(loteNuevo)
        .subscribe(res=>{
          if(res.estado==1){
            Swal.fire({
              icon: 'success',
              title: 'lote creado con exito',
              
            })
            this.limpiarPantalla()
          }
          },error=>{console.log(error)})
        }
      })
    }

  limpiarPantalla(){
    this.producto=''
    this.bodega=''
    this.existencia=0
    this.estado=''
    this.preciounitario=0
    this.marca= ''
    this.fechaingreso=' '
    this.fechacaducidad =' '
  }
  }

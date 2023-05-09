import { Component, OnInit } from '@angular/core';
import { Bodega, Sucursale } from 'src/app/services/api-backend';
import { BodegasLotesService } from 'src/app/services/api-backend/api/bodegasLotes.service';
import { SucursalesService } from 'src/app/services/api-backend/api/sucursales.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-bodegas',
  templateUrl: './bodegas.component.html',
  styleUrls: ['./bodegas.component.scss']
})
export class BodegasComponent implements OnInit {
  IdBodega: number=0;
  IdSucursal: number=0;
  nombreBodega: string='';

    //variables para los select
    dataSucursal:Sucursale[]=[];

  constructor(private serviciobodegasLotes: BodegasLotesService, 
              private servicioSucursales: SucursalesService) {  }
  ngOnInit(): void {
    this.cargarSucursales();
  }
  cargarSucursales(){
    this.servicioSucursales.sucursalesGetSucursalesGet()
    .subscribe(res=>{
      this.dataSucursal = <Sucursale[]>res
      console.log(this.dataSucursal)
    }),error=>{
      console.log(error);
    }
  }

  crearBodega(){
    Swal.fire({
      icon: 'question',
      title: 'Desea crear la bodega?',
      showCancelButton:true,
    }).then(res =>{
      if(res.isConfirmed){
        let bodegaNueva:Bodega={
          idSucursal: this.IdSucursal,
          nombre:this.nombreBodega,
          
          

        }
        this.serviciobodegasLotes. bodegasLotesCrearBodegaPost(bodegaNueva)
        .subscribe(res=>{
          if(res.estado==1){
            Swal.fire({
              icon: 'success',
              title: 'Bodega creada con exito',
              
            })
            this.limpiarPantalla()
          }
          },error=>{console.log(error)})
        }
      })
    }
  limpiarPantalla(){
    this.IdSucursal= 0
    this.nombreBodega=''
  }
  }
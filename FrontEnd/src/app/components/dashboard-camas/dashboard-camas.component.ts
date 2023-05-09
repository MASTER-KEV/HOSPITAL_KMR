import { Component, OnInit, OnDestroy } from '@angular/core';
import { Sucursale } from 'src/app/services/api-backend';
import { CamaService } from 'src/app/services/api-backend/api/cama.service';
import { SucursalesService } from 'src/app/services/api-backend/api/sucursales.service';
import { CamasSocketService } from './camasSocket.service';

@Component({
  selector: 'app-dashboard-camas',
  templateUrl: './dashboard-camas.component.html',
  styleUrls: ['./dashboard-camas.component.scss']
})
export class DashboardCamasComponent implements OnInit,OnDestroy {
  dataSucursal:Sucursale[]=[];
  IdSucursal:string = '-';
  Sucursal:Sucursale;
  DataGeneral
  constructor(private socketCamas:CamasSocketService,
    private servicioSucursales: SucursalesService,
    private camaservices: CamaService) 
  {
    // this.socketCamas.infoCamas.subscribe(res =>{
    //   console.log(res)
    // })
  }

  ngOnInit(): void {
    this.cargarSucursales()
  } 
  Dato:string = '----';
  CambioSucursal(){
    try{
      this.socketCamas.detener();
    }catch(e){
      console.log(e);
    }
    if(this.IdSucursal == '-'){
      this.DataGeneral = null
      return
    }
    this.socketCamas.iniciar(this.IdSucursal)
    this.socketCamas.infoCamas.subscribe(res =>{
      let datoTem:any[] =<any[]> res
      if(datoTem.length == 0){

      }else{
        this.DataGeneral = <any[]>res
        console.log(this.DataGeneral);
      }
      
    })
    this.camaservices.camaSalasSucursalGet(this.IdSucursal)
    .subscribe(rest =>{
      
    },error =>{
      this.socketCamas.detener();
    })
    
   
  }
  
  cargarSucursales(){
    this.servicioSucursales.sucursalesGetSucursalesGet()
    .subscribe(res=>{
      this.dataSucursal = <Sucursale[]>res
      // console.log(this.dataSucursal)
    }),error=>{
      console.log(error);
    }
  }

  ngOnDestroy(){
    this.socketCamas.detener();
  }

  cantidadCamasHabitacion(habi){
    if(habi.Camas.length == 0){
      return "0"
    }
    let cantidad = habi.Camas.filter(e => e.AsignacionesCamas.length==0).length;
    return cantidad+'/'+habi.Camas.length
  }

}

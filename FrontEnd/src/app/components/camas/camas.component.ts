import { Component, OnInit } from '@angular/core';
import { Cama, Habitacione, Sucursale } from 'src/app/services/api-backend';
import { CamaService } from 'src/app/services/api-backend/api/cama.service';
import { SucursalesService } from 'src/app/services/api-backend/api/sucursales.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-camas',
  templateUrl: './camas.component.html',
  styleUrls: ['./camas.component.scss']
})
export class CamasComponent implements OnInit {

  idCama:string=""
  idTipoDeHabitacion: string="1"
  idSucursal: string="-"
  dataSucursal:Sucursale[]=[]
  dataHabitacion:Habitacione[]=[]
  nombreHabitacion: string=null
  nombreCama:string=null

  constructor(private sucursaleservices: SucursalesService, 
    private camaservices: CamaService) { }

  ngOnInit(): void {
    this.cargarHabitaciones()
    this.cargarSucursales()
  }

  cargarSucursales(){
    this.sucursaleservices.sucursalesGetSucursalesGet()
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

  crearCamas(){ 
    let cama:Cama={ 
    nombre:this.nombreCama,
    idHabitacion:Number.parseInt(this.idTipoDeHabitacion)
    }
    this.camaservices.camaCrearCamaPost(cama)
    .subscribe(result => { 
      if(result.estado){ 
       Swal.fire({
        title:"Cama creada correctamente",
        icon:"success"
        })
      this.nombreHabitacion=""
      this.idSucursal="-"
      this.idTipoDeHabitacion="-"
      }
      
    }, error=>{
      Swal.fire({ 
        title:"Error",
        icon:"error",
        text:error.error.error
      })
     })

    }
}

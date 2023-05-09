import { Component, OnInit } from '@angular/core';
import { Habitacione, Sucursale } from 'src/app/services/api-backend';
import { CamaService } from 'src/app/services/api-backend/api/cama.service';
import { SucursalesService } from 'src/app/services/api-backend/api/sucursales.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-habitaciones',
  templateUrl: './habitaciones.component.html',
  styleUrls: ['./habitaciones.component.scss']
})
export class HabitacionesComponent implements OnInit {
idTipoDeHabitacion: string="1"
idSucursal: string="-"
nombreHabitacion: string=null
dataSucursal:Sucursale[]=[]
  constructor(private sucursaleservices: SucursalesService, 
    private camaservices: CamaService ) { }

  ngOnInit(): void {
    this.cargarSucursales()
  }
  cargarSucursales(){
    this.sucursaleservices.sucursalesGetSucursalesGet()
    .subscribe(result => {
      this.dataSucursal = <Sucursale[]>result
      console.log(this.dataSucursal)  
    })}
  crearHabitacion(){
    let habitacion:Habitacione={
    idTipoHabitacion:Number.parseInt(this.idTipoDeHabitacion),
    idSucursal:Number.parseInt(this.idSucursal),
    nombre:this.nombreHabitacion
    }
    this.camaservices.camaCrearHabitacionPost(habitacion)
    .subscribe(result => { 
      if(result.estado){ 
       Swal.fire({
        title:"Habitacion creada correctamente",
        icon:"success"
        })
      this.nombreHabitacion=""
      this.idSucursal="-"
      this.idTipoDeHabitacion="1"
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



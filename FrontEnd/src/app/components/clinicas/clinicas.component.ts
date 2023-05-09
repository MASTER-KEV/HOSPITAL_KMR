import { Component, OnInit } from '@angular/core';
import { Clinica, Sucursale } from 'src/app/services/api-backend';
import { ClinicaService } from 'src/app/services/api-backend/api/clinica.service';
import { SucursalesService } from 'src/app/services/api-backend/api/sucursales.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-clinicas',
  templateUrl: './clinicas.component.html',
  styleUrls: ['./clinicas.component.scss']
})
export class ClinicasComponent implements OnInit {
  IdSucursal: number=0;

  datasucursal:Sucursale[]=[];
  nomclinica: string='';
  constructor(
    private servsucursal:SucursalesService,
    private servclinica:ClinicaService
  ) { }

  ngOnInit(): void {
    this.cargarsucursales();
  }

  cargarsucursales(){
    this.servsucursal.sucursalesGetSucursalesGet()
    .subscribe(res =>{
      this.datasucursal=<Sucursale[]>res
    },error =>{
      console.log(error)
    })
  }

  crearclinica(){
    Swal.fire({
      icon:'question',
      title: 'Decea crear la Clinica',
      showCancelButton:true
    }).then(res=>{
      if(res.isConfirmed) {
        let nuevaclinica:Clinica={
          nombre:this.nomclinica,
          idSucursal:this.IdSucursal
        }
        this.servclinica.clinicaCrearClinicaPost(nuevaclinica).subscribe(resp =>{
          if (resp.estado==2){
            Swal.fire({
              icon:'success',
              title: 'Clinica creada correctamente'
            })
            this.nomclinica=''
            this.IdSucursal=0
          }
        },error =>{
          console.log(error)
        })
      }
    })
  }

}



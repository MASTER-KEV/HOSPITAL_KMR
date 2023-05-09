import { Component, OnInit } from '@angular/core';
// import { Result } from '@zxing/library';
import { Departamento, Municipio, Sucursale } from 'src/app/services/api-backend';
import { DepartamentosMunicipiosService } from 'src/app/services/api-backend/api/departamentosMunicipios.service';
import { SucursalesService } from 'src/app/services/api-backend/api/sucursales.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-sucursales',
  templateUrl: './sucursales.component.html',
  styleUrls: ['./sucursales.component.scss']
})
export class SucursalesComponent implements OnInit {
  nombreSucursal: string='';
  direccionSucursal: string='';
  telefono: number=0;
  departamento: string='-';
  municipio: string='-';
  estado: string='A';
  //variables Select
  dataDepartamentos:Departamento[]=[];
  dataMunicipios:Municipio[]=[];

  constructor(private servicioDepMun:DepartamentosMunicipiosService,
              private servicioSucursales:SucursalesService) { }

  ngOnInit(): void {
    this.cargarDepartamentos();
  }
  cargarDepartamentos(){
    this.servicioDepMun.departamentosMunicipiosGetDepartamentosGet()
    .subscribe(res =>{
      this.dataDepartamentos = <Departamento[]>res
      console.log(this.dataDepartamentos)
    }, error=>{
      console.log(error);
    }
    )
  }
  cargarMunicipio(codDepartamento:string){
    if(codDepartamento !='-'){
      this.servicioDepMun.departamentosMunicipiosGetMunicipiosCodDepartentoGet(Number.parseInt(codDepartamento))
      .subscribe(res =>{
        this.dataMunicipios = <Municipio[]>res

      },error=>{
        console.log(error)
      }
      )
    }
  }
  cambioDepartamento(){
    console.log(this.departamento)
    this.cargarMunicipio(this.departamento)
  }
  crearSucursal(){
    Swal.fire({
      icon: 'question',
      title: 'Desea crear la sucursal?',
      showCancelButton:true,
    }).then(res =>{
      if(res.isConfirmed){
        let sucursalNueva:Sucursale={
          idMunicipio: Number.parseInt(this.municipio),
          telefono:this.telefono.toString(),
          direccion:this.direccionSucursal,
          nombre:this.nombreSucursal,
          estado:'A',
          

        }
        this.servicioSucursales.sucursalesCrearSucursalPost(sucursalNueva)
        .subscribe(res=>{
          if(res.estado==1){
            Swal.fire({
              icon: 'success',
              title: 'Sucursal creada con exito',
              
            })
          }
          },error=>{console.log(error)})
        }
      })
    }
  
  }

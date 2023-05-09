import { viewClassName } from '@angular/compiler';
import { createViewChild } from '@angular/compiler/src/core';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
// import { Result } from '@zxing/library';
import { Role, RolesUsuario } from 'src/app/services/api-backend';
import { RolesService } from 'src/app/services/api-backend/api/roles.service';
import { RolesUsuarioService } from 'src/app/services/api-backend/api/rolesUsuario.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-roles-usuario',
  templateUrl: './roles-usuario.component.html',
  styleUrls: ['./roles-usuario.component.scss']
})
export class RolesUsuarioComponent implements OnInit {
  idUsuario:number=0;
  idUsuarioAgregar:number=0;
  //Variales de la tabla Roles Uusuarios
  DataRolesUsuario:RolesUsuario[]=[];
  DataSourseRolesUsuario:MatTableDataSource<RolesUsuario>;
  @ViewChild(MatPaginator) PaginatorRolesUsuario:MatPaginator;
  @ViewChild(MatTable) TablaRolesUsuario:MatTable<RolesUsuario>
  ColumnasTableRolesUsuario:String[] = ['rol','accion']
  //Variables de la tabla de Roles
  DataRoles:Role[] = [];
  DataSourceRoles:MatTableDataSource<Role>;
  @ViewChild(MatTable) TablaRoles: MatTable<Role>;
  @ViewChild(MatPaginator) paginatorRoles : MatPaginator;
  columnasTablaRoles:string[] = ['nombre','estado','accion']
  constructor(private servicioRolesUsuario:RolesUsuarioService,
              private servicioRoles:RolesService) { }

  ngOnInit(): void {
    this.cargarDatosRoles();
  }
  buscarRolesUsuario(){
    this.DataRolesUsuario =[];
    console.log(this.idUsuario)
    this.servicioRolesUsuario.rolesUsuarioGetRolesUsuarioGet(this.idUsuario).subscribe(resultado=>{
     this.DataRolesUsuario = <RolesUsuario[]> resultado;
     this.DataSourseRolesUsuario = new MatTableDataSource(this.DataRolesUsuario);
     setTimeout(() =>{
        this.DataSourseRolesUsuario.paginator = this.PaginatorRolesUsuario;
     },50)
    },error=>{
      console.log(error)
    });
  }

  eliminarRol(elemento:RolesUsuario){
    Swal.fire({
      title:'Desea Eliminar este rol al usuario?',
      showCancelButton:true,

    }).then(res =>{
      if(res.isConfirmed){
        this.servicioRolesUsuario.rolesUsuarioEliminarRolUsuarioPost(elemento.idRol,elemento.idRolUsario)
        .subscribe(Result =>{
          if(Result.estado==1){
            Swal.fire({
              title:'Se Elimino el rol correctamente',
              icon:'success'
            })
            this.buscarRolesUsuario()
          }
        }),error =>{
          console.log(error)
        }
      }
    }
      )
    }
  
  agregarRolUsuario(elemento:Role){
    Swal.fire({
      title:'Desea agregar este rol al usuario?',
      showCancelButton:true,
  }) .then(res =>{
      if(res.isConfirmed){
        let rolUsuario:RolesUsuario={
          idRol:elemento.idRol,
          idUsuario: this.idUsuarioAgregar
          
        }
        this.servicioRolesUsuario.rolesUsuarioAgregarRolUsuarioPost(rolUsuario)
        .subscribe(Result =>{
          if(Result.estado==1){
            Swal.fire({
              title:'El Rol se Agrego Correctamente!!!',
              icon:'success'
            })
            this.buscarRolesUsuario()
          }
        },error =>{
          console.log(error)
        })
      }
  })
}
cargarDatosRoles(){
  this.DataRoles=[];
  this.servicioRoles.rolesGetRolesGet().subscribe(result =>{
    this.DataRoles = <Role[]> result;
    this.DataSourceRoles = new MatTableDataSource(this.DataRoles);
    setTimeout(() => {
      this.DataSourceRoles.paginator = this.paginatorRoles;
    }, 50);
  },error =>{
    Swal.fire({
      title:'Error',
      text:error.error.error,
      icon:'error'
    })
  })
}
}

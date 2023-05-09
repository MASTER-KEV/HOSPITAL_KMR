import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Role } from 'src/app/services/api-backend';
import { RolesService } from 'src/app/services/api-backend/api/roles.service';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-crear-roles',
  templateUrl: './crear-roles.component.html',
  styleUrls: ['./crear-roles.component.scss']
})
export class CrearRolesComponent implements OnInit {

  constructor(private servicioRoles:RolesService,
              private autenticationService:AuthenticationService,
              private datePipe:DatePipe) { }
  rolNuevo:string = "";
  cargando:boolean = false;

  //Variables para la tabla
  DataRoles:Role[] = [];
  DataSourceRoles:MatTableDataSource<Role>;
  @ViewChild(MatTable) TablaRoles: MatTable<Role>;
  @ViewChild(MatPaginator) paginatorRoles : MatPaginator;
  columnasTablaRoles:string[] = ['nombre','estado','accion']

  ngOnInit(): void {
    this.cargarDatosRoles();
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

  CrearRol(){
    if(this.rolNuevo.length== 0){
      Swal.fire({
        title:'Error',
        icon:'error',
        text:'No se ha ingresado un rol'
      })
      return;
    }
    this.cargando = true;
    let rolCrear:Role={
	nombre:this.rolNuevo,
    } 
    this.servicioRoles.rolesCrearRolPost(rolCrear).subscribe(result =>{
	if(result.message == 1){
	    Swal.fire({
		icon:'success',
		text:'Rol creado correctamente',
		title:'Exito'
	    }).then(res =>{
		this.cargando = false
    this.rolNuevo = ""	
	    })
	}

    },error =>{
	Swal.fire({
	    icon:'error',
	    title:'Error',
	    text:error.error.error
	})
	this.cargando = false;
    })
  }
  desactivarRol(rol:Role){
    Swal.fire({
      title:'Desea desactivar el rol '+rol.nombre+' ?',
      icon:'question',
      showCancelButton:true,
      cancelButtonColor:'red',
      cancelButtonText:'No, cancelar',
      confirmButtonColor:'green',
      confirmButtonText:'Si, desactivar'
    }).then(result =>{
      if(result.isConfirmed){
        this.servicioRoles.rolesDesactivarRolPost(rol.idRol)
        .subscribe(resultado =>{
          if(resultado.message == 1){
            Swal.fire({
              title:'Exito!!!',
              icon:'success',
              text:'Rol desactivado correctamente'
            })
          }
          this.cargarDatosRoles();
        })
      }
    })
  }
}


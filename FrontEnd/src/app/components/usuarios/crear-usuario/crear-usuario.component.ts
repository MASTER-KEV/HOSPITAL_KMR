import { Component, OnInit } from '@angular/core';
import { Usuario, UsuariosService } from 'src/app/services/api-backend';
import { DepartamentosMunicipiosService } from 'src/app/services/api-backend/api/departamentosMunicipios.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-crear-usuario',
  templateUrl: './crear-usuario.component.html',
  styleUrls: ['./crear-usuario.component.scss']
})
export class CrearUsuarioComponent implements OnInit {
  nombres:string ='';
  apellidos:string='';
  contras:string='';
  contra2:string='';
  fechaNacimiento:string='';
  username:string='';
  tipoUser:string='-';
  constructor(private userService:UsuariosService,private servicioDeps:DepartamentosMunicipiosService) { }

  ngOnInit(): void {
  }
  contraIgual(){
    if(this.contra2 != this.contras){
      return true
    }
    return false
  }

  CrearUsuario(){
    let user:Usuario={
      nombres:  this.nombres,
      apellidos:  this.apellidos,
      fechaNacimiento: this.fechaNacimiento,
      username: this.username ,
      password:  this.contras,
      idTipoUsuario: Number.parseInt(this.tipoUser) 
    }
    
    this.userService.crearUsuarioPost(user)
    .subscribe(result =>{
      if(result.estado == 1){
        Swal.fire({
          icon:'success',
          title:'Usuario creado correctamente',
        }).then(res=>{
          this.limpiarDatos()
        })
      }
    },error=>{
      console.log(error)
    })
  }

  limpiarDatos(){
    this.nombres ='';
    this.apellidos='';
    this.contras='';
    this.contra2='';
    this.fechaNacimiento='';
    this.username='';
    this.tipoUser='-';
  }

}
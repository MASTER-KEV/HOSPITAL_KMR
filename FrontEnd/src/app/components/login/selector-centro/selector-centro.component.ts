import { environment } from './../../../../environments/environment';
import { Component, OnInit } from '@angular/core';
import { UserModel } from 'src/app/models/UserModel';
import { CentrosProduccion } from 'src/app/models/CentrosProduccion';
import { MatDialogRef } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-selector-centro',
  templateUrl: './selector-centro.component.html',
  styleUrls: ['./selector-centro.component.scss']
})
export class SelectorCentroComponent implements OnInit {
  Centros:CentrosProduccion[]=[
 
  ]
  constructor(private dialogRef: MatDialogRef<SelectorCentroComponent>, private http: HttpClient) { }
  CentroSeleccionado
  centroSeleccionado:CentrosProduccion={
    nombreCentro:'',
    codCentro:''
  };
  selecion:boolean=true;
  ngOnInit(): void {
    let user: UserModel;
    user = JSON.parse(localStorage.getItem("user"));

    this.http.get(environment.apiUrl + '/Login/CentrosUsuario/'+user.username)
      .subscribe((centros: CentrosProduccion[]) => {
        let cantidad = centros.length; 

        if(cantidad ==1){
          this.centroSeleccionado = centros[0]
          localStorage.setItem('centroActual', JSON.stringify(this.centroSeleccionado));
          this.closeDialog();
        }
        
        centros.forEach(element => {
          this.Centros.push(element);
        });
      });
  }

  cambioCentro(centro){
    let dato = centro
    let centroActual = this.Centros.find(centro => centro.codCentro === dato )
    this.centroSeleccionado = centroActual;
    this.selecion=false;
  }

  onSubmit(){
    localStorage.setItem('centroActual', JSON.stringify(this.centroSeleccionado));
    this.closeDialog();
  }

  closeDialog(){
    this.dialogRef.close(1);
  }
}

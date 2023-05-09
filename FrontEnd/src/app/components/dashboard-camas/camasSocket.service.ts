import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import * as signalR from "@microsoft/signalr";
import { environment } from 'src/environments/environment';
import { Sucursale } from "src/app/services/api-backend";

@Injectable({
    providedIn: 'root'
  })
export class CamasSocketService {
    infoCamas = new Subject()
    camasSocket = null
    Sucursal:Sucursale;
    iniciado:boolean = false;
    constructor(){}
    iniciar(idSucursal:string){
        this.iniciado = true;
        let apiUrl = environment.apiUrl
        // console.log('ApiUrl: '+apiUrl);

        this.camasSocket = new signalR.HubConnectionBuilder().withUrl(apiUrl+'/hub/Camas').withAutomaticReconnect().build();
        this.camasSocket.on('Nueva', informacion =>{
            let objetoInfo = JSON.parse(informacion)
            this.infoCamas.next(objetoInfo)
        })
        
        this.camasSocket.start().then(() => {
            this.camasSocket.invoke('UnirseAlGrupo',idSucursal);      
          })
          ;
    }
    detener() {
        if (this.camasSocket != null)
          this.camasSocket.stop();
        
        this.camasSocket = null;
        this.iniciado= false
      }
}
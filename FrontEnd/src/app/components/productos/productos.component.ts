import { Component, OnInit } from '@angular/core';
import { Producto } from 'src/app/services/api-backend';
import { ProductosService } from 'src/app/services/api-backend/api/productos.service';

@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.scss']
})
export class ProductosComponent implements OnInit {
  nombreproducto:string=''
  archivobase64
  nombrearchivo:string=''
  tipo:string=''

  constructor(private servicosproductos:ProductosService) { }

  ngOnInit(): void {
  }
  cambioarchivo(event) {
    if (event.target.files && event.target.files.length)
    {
      let file=event.target.files[0]
      this.nombrearchivo=file.name
      if ((file.type=='image/jpeg')||(file.type=='image/png')) {
        let reader=new FileReader()
        reader.readAsDataURL (file)
        this.tipo=file.type
        reader.onload=()=>{
          this.archivobase64=reader.result
        }
      }
    }
  }
  cargarimagen(){
    let stringbase64:string=this.archivobase64
    let producto:Producto={
      nombre:this.nombreproducto,
      
    }
    if(this.nombrearchivo != ''){
      producto.archivobase64=stringbase64.substring(('data:'+this.tipo+';base64,').length)
      producto.nombrearchivo=this.nombrearchivo
    }
    this.servicosproductos.crearProductoPost(producto)
    .subscribe(res=>{
      if (res.estado){
        console.log('producto creado')
      }
    })
  }

}

import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { Produto } from '../../models/produto';

@Injectable({
  providedIn: 'root'
})
export class ProdutoSevice {

  private configUrl  = "http://localhost:5033/api/";

  constructor(private httpRequest: HttpClient) { }

  getProdutos() {
    return this.httpRequest.get<Produto[]>(this.configUrl + "Product");
  }

  postProdutos(Produto:Produto){
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpRequest.post<any>(this.configUrl + "Product", JSON.stringify(Produto), {headers: headers} );
  }

  putProdutos(Produto:Produto){
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpRequest.put<any>(this.configUrl + "Product", JSON.stringify(Produto), {headers: headers} );
  }

  delProdutos(id?:number){
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpRequest.delete<any>(this.configUrl + "Product?id="+ id);
  }

}

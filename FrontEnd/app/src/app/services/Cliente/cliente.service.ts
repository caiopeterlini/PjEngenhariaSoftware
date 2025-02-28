import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { cliente } from '../../models/cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteSevice {

  private configUrl  = "http://localhost:5033/api/";

  constructor(private httpRequest: HttpClient) { }

  getClientes() {
    return this.httpRequest.get<cliente[]>(this.configUrl + "Client");
  }

  postClientes(cliente:cliente){
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpRequest.post<any>(this.configUrl + "Client", JSON.stringify(cliente), {headers: headers} );
  }

  putClientes(cliente:cliente){
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpRequest.put<any>(this.configUrl + "Client", JSON.stringify(cliente), {headers: headers} );
  }

  delClientes(id?:number){
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpRequest.delete<any>(this.configUrl + "Client?id="+ id);
  }

}

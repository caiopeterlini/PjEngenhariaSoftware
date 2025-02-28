import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { Ordem } from '../../models/ordem';

@Injectable({
  providedIn: 'root'
})
export class OrdemSevice {

  private configUrl  = "http://localhost:5033/api/";

  constructor(private httpRequest: HttpClient) { }

  getOrdems() {
    return this.httpRequest.get<Ordem[]>(this.configUrl + "Order");
  }

  postOrdems(Ordem:Ordem){
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpRequest.post<any>(this.configUrl + "Order", JSON.stringify(Ordem), {headers: headers} );
  }


}

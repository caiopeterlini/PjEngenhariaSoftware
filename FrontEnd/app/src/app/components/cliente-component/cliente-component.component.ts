import { Component, OnInit } from '@angular/core';
import { cliente } from '../../models/cliente';
import { ClienteSevice } from '../../services/Cliente/cliente.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrModule, ToastrService } from 'ngx-toastr'
import { CommonModule } from '@angular/common';
import {  MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { MatIconModule} from '@angular/material/icon';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-cliente-component',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,MatFormFieldModule,MatInputModule,MatButtonModule,MatIconModule,RouterModule ],
  templateUrl: './cliente-component.component.html',
  styleUrl: './cliente-component.component.scss'
})
export class ClienteComponent implements OnInit {
  public checkoutForm : any
  private _ClienteSevice :  ClienteSevice;
  public clientes : cliente[] = [];
  public clienteSelecionado?: cliente;


  constructor( httpclient : HttpClient, private fb: FormBuilder) {
    this._ClienteSevice = new ClienteSevice(httpclient);
  }

  ngOnInit(): void {
    this.checkoutForm = this.fb.group({
      cpf: ['', Validators.required],
      nome: ['', Validators.required],
    });
    this.CarregarClientesCadastrados()
  }


  CarregarClientesCadastrados(){
    this.clientes = [];
    this._ClienteSevice.getClientes().subscribe({
      next: (data:any) => {
        data.forEach((p: { id: number | undefined; cpf: string | undefined; name: string | undefined; })=>{
          var cli= new cliente();
          cli.id = p.id;
          cli.Cpf = p.cpf;
          cli.Name = p.name;
          this.clientes?.push(cli);
        })
      },
      error: (err) => {
        console.error('Erro: ' + err);
      }
    });
  }

  SelecionarCliente(cliente: cliente) {
    this.clienteSelecionado = cliente;
    this.checkoutForm.patchValue({
      cpf: cliente.Cpf,
      nome: cliente.Name
    });
  }

  ExcluirCliente(cliente: cliente) {
    this._ClienteSevice.delClientes(cliente.id
    ).subscribe((data:any) => {
        this.CarregarClientesCadastrados()
    });
  }

  CancelarEdicao()
  {
    this.clienteSelecionado = undefined;
    this.checkoutForm.patchValue({
      cpf: " ",
      nome: " "
    });
  }

  onSubmit(){

    var clientePost = new cliente();
    clientePost.Cpf =  this.checkoutForm.get('cpf').value;
    clientePost.Name =  this.checkoutForm.get('nome').value;

    debugger;
      if(this.clienteSelecionado){
        clientePost.id = this.clienteSelecionado.id
        this._ClienteSevice.putClientes(clientePost
        ).subscribe((data:any) => {
            this.CarregarClientesCadastrados()
        });
      }
      else{


        this._ClienteSevice.postClientes(clientePost
        ).subscribe((data:any) => {
            this.CarregarClientesCadastrados()
        });
      }
    }
}

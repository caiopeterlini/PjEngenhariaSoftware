import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrModule, ToastrService } from 'ngx-toastr'
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { OrdemSevice } from '../../services/Ordem/ordem.service';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { ClienteSevice } from '../../services/Cliente/cliente.service';
import { cliente } from '../../models/cliente';
import { Ordem } from '../../models/ordem';
import { ProdutoSevice } from '../../services/Produto/produto.service';
import { Produto } from '../../models/produto';
import { ItensOrder } from '../../models/ItensOrder';

@Component({
  selector: 'app-produto-component',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,MatFormFieldModule,MatInputModule,MatButtonModule,MatIconModule,RouterModule ],
  templateUrl: './relatorio-component.component.html',
  styleUrl: './relatorio-component.component.scss'
})
export class RelatorioComponent implements OnInit {
  public checkoutForm: any
   private _OrdemSevice: OrdemSevice;
   private _ClienteSevice: ClienteSevice;
   private _ProdutoSevice :  ProdutoSevice;

   public ordens: Ordem[] = [];
   public ordensGrid: Ordem[] = [];
   public clientes: cliente[] = [];
   public produtos : Produto[] = [];



   constructor(httpclient: HttpClient, private fb: FormBuilder) {
     this._OrdemSevice = new OrdemSevice(httpclient);
     this._ClienteSevice = new ClienteSevice(httpclient);
     this._ProdutoSevice = new ProdutoSevice(httpclient);
   }

   ngOnInit(): void {
     this.checkoutForm = this.fb.group({
       clienteId: ['', Validators.required],
       clienteSelecionado: ['']
     });
     this.CarregarClientesCadastrados();
   }

   //#region processamento formulario

 CarregarprodutosCadastrados(){
   this.produtos = [];
   this._ProdutoSevice.getProdutos().subscribe({
     next: (data:any) => {
       data.forEach((p: { id: number | undefined; price: number ; name: string | undefined; })=>{
         var cli= new Produto();
         cli.id = p.id;
         cli.Price = p.price;
         cli.Name = p.name;
         this.produtos?.push(cli);
       })
     },
     error: (err) => {
       console.error('Erro: ' + err);
     }
   });
 }
 CarregarClientesCadastrados() {
   this.clientes = [];
   this._ClienteSevice.getClientes().subscribe({
     next: (data: any) => {
       data.forEach((p: { id: number | undefined; cpf: string | undefined; name: string | undefined; }) => {
         var cli = new cliente();
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

 //#endregion


   onSubmit() {
    this.ordensGrid = [];
       this._OrdemSevice.getOrdemsbyClientId(this.checkoutForm.get('clienteSelecionado').value
       ).subscribe((data: any[]) => {
        data.forEach(d => {
          this.ordensGrid.push({id:d.id,TotalPrice:d.totalPrice,ClientId:d.clientId,ItensP: d.itens});

        })
       });

   }
 }

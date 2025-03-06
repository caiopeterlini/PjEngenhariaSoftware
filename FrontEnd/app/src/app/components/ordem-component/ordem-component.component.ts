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
  selector: 'app-ordem-component',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule, FormsModule, MatInputModule, MatButtonModule, MatIconModule, RouterModule, MatSelectModule, MatOptionModule],
  templateUrl: './ordem-component.component.html',
  styleUrl: './ordem-component.component.scss'
})
export class OrdemComponent implements OnInit {
  public checkoutForm: any
  private _OrdemSevice: OrdemSevice;
  private _ClienteSevice: ClienteSevice;
  private _ProdutoSevice :  ProdutoSevice;
  showNotification: boolean = false;
  showNotificationWar: boolean = false;
  textoNotificacao: string = "";


  public ordens: Ordem[] = [];
  public clientes: cliente[] = [];
  public produtos : Produto[] = [];


  public ItensOrderSelecionados: ItensOrder[] = [];
  public total: number= 0;

  constructor(httpclient: HttpClient, private fb: FormBuilder) {
    this._OrdemSevice = new OrdemSevice(httpclient);
    this._ClienteSevice = new ClienteSevice(httpclient);
    this._ProdutoSevice = new ProdutoSevice(httpclient);
  }

  ngOnInit(): void {
    this.checkoutForm = this.fb.group({
      preco: ['', Validators.required],
      clienteId: ['', Validators.required],
      clienteSelecionado: ['']
    });
    this.CarregarordemsCadastrados()
    this.CarregarClientesCadastrados()
    this.CarregarprodutosCadastrados()
  }

  //#region processamento formulario
calcularTotalPreco(): number {
  this.total= 0;
     this.ItensOrderSelecionados.forEach(t =>{
      if(t.Produto)
      this.total += t.Produto?.Price * t.Quantity
    });

    return this.total
  }
AdicionarProduto(produto: Produto){
  var validacao  = this.ItensOrderSelecionados.find(c => c.Produto?.id == produto.id)
  var itens : ItensOrder = {  Produto: new Produto, Quantity:0 };
  if(validacao){
    validacao.Quantity += 1
  }
  else{
    itens.Produto = produto
    itens.Quantity = 1
    this.ItensOrderSelecionados.push(itens)
  }
}

RemoverProduto(produto: Produto) {

  var validacao  = this.ItensOrderSelecionados.find(c => c.Produto?.id == produto.id)

  if(validacao && validacao.Quantity!=1){
    validacao.Quantity -= 1
  }
  else{
    const index = this.ItensOrderSelecionados.findIndex(c => c.Produto?.id == produto.id)

      if (index !== -1) {
        this.ItensOrderSelecionados.splice(index, 1);
      }
  }
}
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
CarregarordemsCadastrados() {
  this.ordens = [];
  this._OrdemSevice.getOrdems().subscribe({
    next: (data: any) => {
      data.forEach((p: { id: number | undefined; TotalPrice: number | undefined; name: string | undefined; Itens: []; }) => {
        var o = new Ordem();
        o.id = p.id;
        o.TotalPrice = p.TotalPrice;
        o.ItensP = p.Itens
        this.ordens?.push(o);
      })
    },
    error: (err) => {
      console.error('Erro: ' + err);
    }
  });
}
//#endregion

show(): void {
  this.showNotification = true;
  setTimeout(() => {
    this.showNotification = false;
  }, 3000);
}


showWarning(): void {
  this.showNotificationWar = true;
  setTimeout(() => {
    this.showNotificationWar = false;
  }, 3000);
}

  onSubmit() {


    if(this.ItensOrderSelecionados.length > 0){

      var ordemPost = new Ordem();

      ordemPost.id  = this.ordens.length + 1
      ordemPost.ClientId =  +this.checkoutForm.get('clienteSelecionado').value;
      ordemPost.TotalPrice =  this.total
      ordemPost.ItensP =  this.ItensOrderSelecionados
      console.log(ordemPost);
      this._OrdemSevice.postOrdems(ordemPost
      ).subscribe((data: any) => {
        this.CarregarordemsCadastrados();
        this.textoNotificacao = "Ordem Enviada!"
        this.show();
      });
    }else{
      this.textoNotificacao = "Algum produto deve ser adicionado"
      this.showWarning();
    }

  }
}

import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrModule, ToastrService } from 'ngx-toastr'
import { CommonModule } from '@angular/common';
import {  MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { MatIconModule} from '@angular/material/icon';
import { ProdutoSevice } from '../../services/Produto/produto.service';
import { Produto } from '../../models/produto';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-produto-component',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,MatFormFieldModule,MatInputModule,MatButtonModule,MatIconModule,RouterModule ],
  templateUrl: './produto-component.component.html',
  styleUrl: './produto-component.component.scss'
})
export class ProdutoComponent implements OnInit {
  public checkoutForm : any
  private _ProdutoSevice :  ProdutoSevice;
  public produtos : Produto[] = [];
  public produtoSelecionado?: Produto;


  constructor( httpclient : HttpClient, private fb: FormBuilder) {
    this._ProdutoSevice = new ProdutoSevice(httpclient);
  }

  ngOnInit(): void {
    this.checkoutForm = this.fb.group({
      preco: ['', Validators.required],
      nome: ['', Validators.required],
    });
    this.CarregarprodutosCadastrados()
  }


  CarregarprodutosCadastrados(){
    this.produtos = [];
    this._ProdutoSevice.getProdutos().subscribe({
      next: (data:any) => {
        data.forEach((p: { id: number | undefined; price: number; name: string | undefined; })=>{
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

  SelecionarProduto(produto: Produto) {
    this.produtoSelecionado = produto;
    this.checkoutForm.patchValue({
      preco: produto.Price,
      nome: produto.Name
    });
  }

  ExcluirProduto(produto: Produto) {
    this._ProdutoSevice.delProdutos(produto.id
    ).subscribe((data:any) => {
        this.CarregarprodutosCadastrados()
    });
  }

  CancelarEdicao()
  {
    this.produtoSelecionado = undefined;
    this.checkoutForm.patchValue({
      preco: " ",
      nome: " "
    });
  }

  onSubmit(){

    var produtoPost = new Produto();
    produtoPost.Price =  +this.checkoutForm.get('preco').value;
    produtoPost.Name =  this.checkoutForm.get('nome').value;

      if(this.produtoSelecionado){
        produtoPost.id = this.produtoSelecionado.id
        this._ProdutoSevice.putProdutos(produtoPost
        ).subscribe((data:any) => {
            this.CarregarprodutosCadastrados()
        });
      }
      else{


        this._ProdutoSevice.postProdutos(produtoPost
        ).subscribe((data:any) => {
            this.CarregarprodutosCadastrados()
        });
      }
    }
}

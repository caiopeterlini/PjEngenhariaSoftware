import { Routes } from '@angular/router';
import { ClienteComponent } from './components/cliente-component/cliente-component.component';
import { ProdutoComponent } from './components/produto-component/produto-component.component';
import { OrdemComponent } from './components/ordem-component/ordem-component.component';
import { RelatorioComponent } from './components/relatorio-component/relatorio-component.component';

export const routes: Routes = [
  { path: 'cadastro-cliente', component: ClienteComponent },
  { path: 'cadastro-produto', component: ProdutoComponent },
  { path: 'relatorio-ordem', component: RelatorioComponent },
  { path: 'cadastro-ordem', component: OrdemComponent },
  { path: '**', component: ClienteComponent }
];

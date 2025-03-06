import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import {  provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';
import { provideRouter } from '@angular/router';
import { ClienteComponent } from './app/components/cliente-component/cliente-component.component';
import { ProdutoComponent } from './app/components/produto-component/produto-component.component';
import { RelatorioComponent } from './app/components/relatorio-component/relatorio-component.component';
import { OrdemComponent } from './app/components/ordem-component/ordem-component.component';
import { provideHttpClient } from '@angular/common/http';

bootstrapApplication(AppComponent, {
  providers: [
    provideAnimations(),
    provideToastr(),
    provideRouter([
      { path: 'cadastro-cliente', component: ClienteComponent },
      { path: 'cadastro-produto', component: ProdutoComponent },
      { path: 'relatorio-ordem', component: RelatorioComponent },
      { path: 'cadastro-ordem', component: OrdemComponent },
      { path: '**', component: ClienteComponent }
    ]),
    provideHttpClient(),
  ],
}).catch((err) => console.error(err));

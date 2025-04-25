import { Routes } from '@angular/router';
import { CadastroEmpresaComponent } from './components/cadastro-empresa/cadastro-empresa.component';
import { AutenticacaoComponent } from './components/autenticacao/autenticacao.component';
import { authGuard } from './auth.guard';

export const routes: Routes = [
  { path: 'cadastro', component: AutenticacaoComponent },
  { path: 'login', component: AutenticacaoComponent },
  {
    path: 'home',
    component: CadastroEmpresaComponent,
    canActivate: [authGuard],
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];

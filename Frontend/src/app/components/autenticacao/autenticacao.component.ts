import { Component } from '@angular/core';
import { TabsModule } from 'primeng/tabs';
import { CadastroUsuarioComponent } from '../cadastro-usuario/cadastro-usuario.component';
import { LoginUsuarioComponent } from '../login-usuario/login-usuario.component';
import { CadastroEmpresaComponent } from '../cadastro-empresa/cadastro-empresa.component';

@Component({
  selector: 'app-autenticacao',
  imports: [
    TabsModule,
    CadastroUsuarioComponent,
    LoginUsuarioComponent,
    CadastroEmpresaComponent,
  ],
  templateUrl: './autenticacao.component.html',
  styleUrl: './autenticacao.component.css',
})
export class AutenticacaoComponent {}

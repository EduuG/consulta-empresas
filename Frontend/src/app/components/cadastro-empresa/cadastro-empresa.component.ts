import { Component, inject, OnInit } from '@angular/core';
import { UiPrimeModule } from '../../shared/ui-prime.module';
import { EmpresaService } from '../../services/empresa.service';
import { DadosEmpresaComponent } from '../dados-empresa/dados-empresa.component';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { UsuarioInfo } from '../../models/usuario-info.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-cadastro-empresa',
  standalone: true,
  imports: [UiPrimeModule, DadosEmpresaComponent],
  templateUrl: './cadastro-empresa.component.html',
  providers: [],
})
export class CadastroEmpresaComponent implements OnInit {
  nome: string | null = null;

  constructor(private router: Router) {}

  empresaService = inject(EmpresaService);
  authService = inject(AuthService);

  ngOnInit(): void {
    this.authService.obterUsuarioInfo().subscribe(
      (response: UsuarioInfo) => {
        this.nome = response.nome.split(' ')[0];
      },
      (error: HttpErrorResponse) => {
        console.error('Erro ao obter informações do usuário:', error);
      }
    );
  }

  logout(): void {
    localStorage.removeItem('jwt');
    this.router.navigate(['/login']);
  }
}

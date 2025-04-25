import { Component } from '@angular/core';
import { UiPrimeModule } from '../../shared/ui-prime.module';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-usuario',
  imports: [UiPrimeModule],

  templateUrl: './login-usuario.component.html',
  styleUrl: './login-usuario.component.css',
})
export class LoginUsuarioComponent {
  constructor(private authService: AuthService, private router: Router) {}

  email = '';
  senha = '';

  // Expressão regular para validação do e-mail
  emailPattern: string = '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$';

  login() {
    const loginData = {
      email: this.email,
      senha: this.senha,
    };

    this.authService.login(loginData).subscribe({
      next: (response) => {
        this.authService.armazenarToken(response.token);
        console.log('Login bem-sucedido', response);
        this.router.navigate(['/home']);
      },
      error: (err) => {
        console.error('Erro no login', err);
      },
    });
  }

  validarEmail(email: string): boolean {
    const regex = new RegExp(this.emailPattern);
    return regex.test(email);
  }
}

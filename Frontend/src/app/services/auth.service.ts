import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // Expressão regular para validação do e-mail
  emailPattern: string = '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$';

  cadastrar(data: {
    nome: string;
    email: string;
    senha: string;
  }): Observable<any> {
    return this.http.post(`${this.apiUrl}/usuario/cadastro`, data);
  }

  login(data: { email: string; senha: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/usuario/login`, data);
  }

  armazenarToken(token: string): void {
    localStorage.setItem('jwt', token);
  }

  obterToken(): string | null {
    return localStorage.getItem('jwt');
  }

  estaAutenticado(): boolean {
    const token = this.obterToken();
    return token ? true : false;
  }

  obterUsuarioInfo(): any {
    return this.http.get<any>(`${this.apiUrl}/usuario/me`);
  }

  validarEmail(email: string): boolean {
    const regex = new RegExp(this.emailPattern);
    return regex.test(email);
  }
}

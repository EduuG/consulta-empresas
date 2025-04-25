import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { EmpresaResponse } from '../models/empresa-response.model';

@Injectable({ providedIn: 'root' })
export class EmpresaService {
  private empresasSubject = new BehaviorSubject<EmpresaResponse[]>([]);
  empresasCadastradas$ = this.empresasSubject.asObservable();

  constructor(private http: HttpClient) {}

  cnpj = '';
  empresasCadastradas: EmpresaResponse[] = [];
  first = 0;

  carregarEmpresas(): void {
    this.listarEmpresas().subscribe({
      next: (empresas) => {
        this.empresasSubject.next(empresas);
      },
      error: (err) => {
        console.error('Erro ao carregar empresas:', err);
      },
    });
  }

  cadastrarEmpresa() {
    const cnpjLimpo = this.cnpj.replace(/\D/g, '');

    this.consultarCNPJ(cnpjLimpo).subscribe({
      next: (res) => {
        console.log(res);
        this.empresasSubject.next([...this.empresasSubject.value, res]);
      },
      error: (err) => console.error(err),
    });

    // Limpar campo
    this.cnpj = '';

    this.first = 0;
  }

  consultarCNPJ(cnpj: string): Observable<EmpresaResponse> {
    const url = `http://localhost:5095/api/empresa/${cnpj}`;
    return this.http.get<EmpresaResponse>(url);
  }

  listarEmpresas() {
    const url = 'http://localhost:5095/api/empresa/listarEmpresas';
    return this.http.get<EmpresaResponse[]>(url);
  }
}

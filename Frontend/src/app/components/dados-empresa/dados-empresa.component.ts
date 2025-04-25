import { Component, inject, OnInit } from '@angular/core';
import { EmpresaService } from '../../services/empresa.service';
import { UiPrimeModule } from '../../shared/ui-prime.module';

@Component({
  selector: 'app-dados-empresa',
  imports: [UiPrimeModule],
  templateUrl: './dados-empresa.component.html',
})
export class DadosEmpresaComponent implements OnInit {
  empresaService = inject(EmpresaService);

  ngOnInit(): void {
    this.empresaService.carregarEmpresas();
  }

  atualizarLista() {
    this.empresaService.carregarEmpresas();
  }
}

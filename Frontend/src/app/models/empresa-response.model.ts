export interface EmpresaResponse {
  cnpj: string;
  nome: string;
  fantasia: string;
  situacao: string;
  abertura: string;
  natureza_juridica: string;
  atividade_principal: { descricao: string }[];
  bairro: string;
  cep: string;
  complemento: string;
  logradouro: string;
  municipio: string;
  numero: string;
  tipo: string;
  uf: string;
}

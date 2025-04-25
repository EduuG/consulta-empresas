# Consulta de empresas

Aplicação simples desenvolvida com Angular e .NET, que integra a API ReceitaWS para consultar informações de empresas pelo CNPJ. Implementa autenticação via JWT e oferece uma interface interativa para cadastro e visualização de dados.

## Instruções de Execução

Para facilitar a inicialização de todos os serviços necessários, criei um script bem básico chamado `start.sh` na raiz do projeto. Esse script faz todo o trabalho para você, executando as etapas necessárias para rodar a aplicação localmente.

### Passos:

1. **Certifique-se de ter o Docker e o Docker Compose instalados.**

2. **Execute o script `start.sh`**:
   Na raiz do projeto, basta rodar o seguinte comando no terminal para iniciar todos os containers e serviços:

   ```bash
   ./start.sh
   ```

   O script irá:

   - Iniciar os containers Docker necessários via `docker-compose`.
   - Iniciar o backend com o comando `dotnet run`.
   - Iniciar o frontend com o comando `npm run start`.

3. **Interromper a execução**: Para parar os serviços, pressione `Ctrl+C`. O script irá derrubar todos os containers e processos automaticamente.

## Requisitos

- Docker
- Docker Compose
- .NET SDK 9.0 (para o backend)
- Node.js e npm (para o frontend)

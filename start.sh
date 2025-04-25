#!/bin/bash

NEWLINE=$'\n'

# Verifica se os containers Docker já estão rodando
if [ -z "$(docker compose ps --services --status running)" ]; then
  echo "${NEWLINE}Subindo os containers Docker...${NEWLINE}"
  docker compose up -d
fi

# Maneira preguiçosa de esperar os containers subirem. Sucetível a bugs :P
sleep 10

# Inicia o backend
echo "${NEWLINE}Iniciando Backend...${NEWLINE}"
cd Backend
dotnet run &
backend_pid=$!
cd ..

# Inicia o frontend
echo "${NEWLINE}Iniciando Frontend...${NEWLINE}"
cd Frontend
npm install && npm run start &
frontend_pid=$!
cd ..

# Função para limpar e derrubar tudo ao receber CTRL+C
cleanup() {
  echo -e "${NEWLINE}Parando todos os serviços...${NEWLINE}"
  kill $frontend_pid $backend_pid 2>/dev/null
  echo "${NEWLINE}Removendo containers, volumes e redes...${NEWLINE}"
  docker compose down -v --remove-orphans
  exit 0
}

# Captura o CTRL+C para executar a limpeza
trap cleanup SIGINT

echo -e "${NEWLINE}Tudo pronto! Acesse o frontend no endereço indicado."
echo "Pressione CTRL+C para derrubar os serviços... ${NEWLINE}"

# Mantém o script rodando infinitamente
while true; do
  sleep 1
done

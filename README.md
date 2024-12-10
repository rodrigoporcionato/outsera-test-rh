# outsera-test-rh
Teste solicitado pela cia Outsera

![alt text](image.png)


# **Iniciar o Projeto Localmente**

Este guia irá ajudá-lo a configurar e rodar o projeto localmente utilizando Docker e Docker Compose.

## **Requisitos**

Docker ou dotrnet instalado.

## Passos para executar localmente


### Executar o Build 
```bash
docker-compose up --build
```

Abrir via browser o projeto angular requisitado no caminho `http://localhost/dashboard`

Para a API, use o endpoit `http://localhost:5001/api/movies` para listar tudo

Ou para ver os intervalos use `http://localhost:5001/api/Movies/producers/intervals`

Também pode usar o swagger se desejar em `http://localhost:5001/swagger/index.html`


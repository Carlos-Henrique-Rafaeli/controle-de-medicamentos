# Projeto Controle De Medicamentos

## Demonstração

![]( https://i.imgur.com/nCxo6K2.gif )

## Projeto

Desenvolvido durante o curso Fullstack da [Academia do Programador](https://www.academiadoprogramador.net) 2025

## Descrição

Este projeto tem como objetivo desenvolver um sistema de gestão para um ambiente relacionado à gestão de medicamentos, pacientes, fornecedores, funcionários e prescrições médicas. Este sistema é responsável
por registrar, visualizar, editar e excluir dados de fornecedores, pacientes, medicamentos, funcionários, requisições de entrada e saída de medicamentos, além de gerar relatórios relacionados às prescrições médicas.



## Detalhes e Funcionalidades

## Fornecedores

* Cadastro, visualização, edição e exclusão.

* Validação de CNPJ único para evitar duplicidade.

## Pacientes

* Cadastro, visualização, edição e exclusão.

* Validação de Cartão do SUS único para evitar duplicidade.

## Medicamentos

* Cadastro, visualização, edição e exclusão.

* Controle de estoque, com atualização automática.

* Alerta para medicamentos com menos de 20 unidades.

## Funcionários

* Cadastro, visualização, edição e exclusão.

* Validação de CPF único para evitar duplicidade.

## Requisições de Saída:

* Cadastro e visualização de requisições de saída de medicamentos

* Vinculação com prescrições médicas.

* Atualização automática do estoque ao registrar saída.

# Requisições de Entrada:

* Cadastro e visualização de requisições de entrada de medicamentos.

* Atualização automática do estoque ao registrar entrada.
 Prescrições Médicas:

* Cadastro de prescrições médicas com medicamentos e dosagens.

* Geração de relatórios de prescrições.

* Validação da disponibilidade de medicamentos no estoque.

* Alertas para limites de medicamentos excedidos.




## Requisitos


- .NET SDK (recomendado .NET 8.0 ou superior) para compilação e execução do projeto.
---
## Como Usar

#### Clone o Repositório
```
https://github.com/Carlos-Henrique-Rafaeli/controle-de-medicamentos
```

#### Navegue até a pasta raiz da solução
```
cd ControleDeMedicamentos
```

#### Restaure as dependências
```
dotnet restore
```

#### Navegue até a pasta do projeto
```
cd ControleDeMedicamentos
```

#### Execute o projeto
```
dotnet run
```

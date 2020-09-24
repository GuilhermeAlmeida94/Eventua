# Eventua

<p align="center">
<img alt="Last commit on GitHub" src="https://img.shields.io/github/last-commit/Guiphb/Eventua?color=%839192">
<img alt="Made by Johnny" src="https://img.shields.io/badge/made%20by-Guilherme Almeida-%20?color=%839192">
<img alt="Project top programing language" src="https://img.shields.io/github/languages/top/Guiphb/Eventua?color=%839192">
<img alt="GitHub license" src="https://img.shields.io/github/license/Guiphb/Eventua?color=%839192">
</p> 

## Sobre o projeto
### Projeto tutorial desenvolvido apartir de um curso da <a href="https://www.udemy.com/course/angular-dotnetcore-efcore/">Udemy</a> com atualizações na versão dos frameworks .NET Core e Angular.

## Tecnologias utilizadas
 - .NET Core 3.1
 - Angular 10.0.3

## Back-end
### Para criar o banco de dados execute o seguinte comando na pasta Eventua.Repository:
```bash
dotnet ef --startup-project ../Eventua.API database update
```
### Para rodar o back-end execute o seguinte comando na pasta Eventua.API:
```bash
dotnet run
```
## Front-end
### Para restaurar os módulos utilizados no projeto execute o seguinte comando na pasta Eventua-App:
```bash
npm update
```
### Para rodar o front-end e abrir o navegador execute o seguinte comando na pasta Eventua-App:
```bash
ng serve -o
```
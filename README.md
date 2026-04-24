<h1 align="center">💬 Chat App</h1>

<p align="center">
  Aplicação fullstack de chat desenvolvida com React Native (Expo) e .NET, com autenticação de usuários e troca de mensagens.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/React_Native-0.7x-61DAFB?style=for-the-badge&logo=react&logoColor=black" />
  <img src="https://img.shields.io/badge/Expo-5x-000020?style=for-the-badge&logo=expo&logoColor=white" />
  <img src="https://img.shields.io/badge/TypeScript-5.x-3178C6?style=for-the-badge&logo=typescript&logoColor=white" />
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity_Framework-8.0-68217A?style=for-the-badge" />
  <img src="https://img.shields.io/badge/JWT-Auth-000000?style=for-the-badge" />
</p>

---

## 📋 Sobre o projeto

Este projeto foi desenvolvido com o objetivo de criar uma aplicação de chat completa, abordando tanto o desenvolvimento mobile quanto backend.

A aplicação permite que usuários se cadastrem, realizem login, iniciem conversas e troquem mensagens, simulando o funcionamento básico de plataformas de chat.

---

## 🎯 Objetivos

* ✅ Implementar autenticação com JWT
* ✅ Criar uma API estruturada em camadas
* ✅ Desenvolver um app mobile com navegação moderna (Expo Router)
* ✅ Permitir troca de mensagens entre usuários
* ✅ Aplicar boas práticas de organização de código (Clean Architecture)

---

## 🛠️ Tecnologias

### 📱 Mobile (React Native)

| Tecnologia   | Uso                           |
| ------------ | ----------------------------- |
| React Native | Desenvolvimento mobile        |
| Expo         | Ambiente de desenvolvimento   |
| Expo Router  | Navegação baseada em arquivos |
| TypeScript   | Tipagem estática              |

---

### 🔧 Backend

| Tecnologia            | Uso           |
| --------------------- | ------------- |
| ASP.NET Core (.NET 8) | API REST      |
| Entity Framework Core | ORM           |
| JWT Bearer            | Autenticação  |
| BCrypt                | Hash de senha |

---

### 🗄️ Banco de dados

| Tecnologia | Uso                       |
| ---------- | ------------------------- |
| SQL Server | Banco de dados relacional |

---

## 📁 Estrutura do projeto

```
Projeto/
├── Projeto.Api            # Controllers e configuração da API
├── Projeto.Application    # Regras de negócio e serviços
├── Projeto.Domain         # Entidades e interfaces
├── Projeto.Infrastructure # Banco de dados e repositórios
├── Projeto.Communication  # DTOs (Request/Response)
├── app/                   # Aplicação mobile (React Native + Expo)
│   ├── (auth)             # Login e cadastro
│   ├── (tabs)             # Navegação principal
│   ├── chat               # Tela de chat
│   ├── components         # Componentes reutilizáveis
│   ├── services           # Comunicação com API
│   └── utils              # Funções auxiliares
```

---

## 🚀 Como rodar o projeto

### 📌 Pré-requisitos

* .NET SDK 8.0
* Node.js 18+
* SQL Server
* Expo CLI (`npm install -g expo-cli`)
* Expo Go ou emulador Android/iOS

---

### ⚙️ Backend

```bash
# Restaurar dependências
dotnet restore

# Aplicar migrations
dotnet ef database update

# Rodar a API
dotnet run
```

A API estará disponível em:

```
http://localhost:5000
```

---

### 📱 Frontend

```bash
# Entrar na pasta do app
cd app

# Instalar dependências
npm install

# Iniciar projeto
npx expo start
```

---

## 🔗 Integração com a API

No frontend, configure a URL da API:

```ts
const API_URL = "http://SEU_IP:5000";
```

> ⚠️ Se estiver usando celular físico, utilize o IP da sua máquina na rede.

---

## 🔐 Funcionalidades

* Cadastro de usuário
* Login com autenticação JWT
* Criação de conversas
* Listagem de conversas
* Envio de mensagens
* Agrupamento de mensagens por data

---

## 🧠 Arquitetura

O backend segue uma separação em camadas inspirada em **Clean Architecture**, com:

* Controllers responsáveis pelas requisições HTTP
* Services contendo regras de negócio
* Repositórios para acesso a dados
* DTOs para comunicação entre camadas

O frontend é desacoplado da API, utilizando uma camada de services para comunicação.

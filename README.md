 📌 Korp ERP (Teste Técnico)
   
  Este projeto é uma solução robusta de **Gestão de Estoque e Faturamento**, desenvolvida como um teste técnico para demonstrar competências em
  arquitetura de microserviços, resiliência de sistemas e desenvolvimento frontend moderno. O sistema permite o controle rigoroso de produtos e a emissão 
      de notas fiscais com integração em tempo real entre os serviços.
    
  ## 🏗️ Arquitetura e Estrutura
    
   O projeto adota uma estratégia de **Monorepo**, facilitando a gestão do código tanto para o Frontend quanto para o Backend. A arquitetura é dividida em:
    
   -   **Backend:** Dois microserviços independentes (**Estoque** e **Faturamento**) seguindo os princípios da **Clean Architecture** e do padrão
    **Repository**. Eles se comunicam via chamadas HTTP REST de forma resiliente.
  -   **Frontend:** Uma aplicação SPA de alta performance que consome as APIs, garantindo uma experiência de usuário fluida e reativa.
 
  ## 🚀 Tecnologias Utilizadas
 
  ### **Backend**
  -   **C# / .NET 10**: Utilizando as versões mais recentes da plataforma.
  -   **Entity Framework Core**: Para mapeamento objeto-relacional (ORM).
  -   **MariaDB / MySQL**: Banco de dados para persistência de dados.
  -   **Polly**: Biblioteca de resiliência e tratamento de falhas transientes.
  -   **Swagger (OpenAPI)**: Para documentação e testes das APIs.
 
  ### **Frontend**
  -   **Angular 21**: Utilizando as funcionalidades mais modernas da versão 20+.
  -   **Standalone Components**: Arquitetura sem módulos (NgModule-less).
  -   **Signals**: Gerenciamento de estado reativo e granular.
  -   **Modern Control Flow**: Uso de `@if`, `@for` e `@switch` para templates limpos.
  -   **Angular Material**: Componentes de interface elegantes e acessíveis.
  -   **SCSS / CSS Custom Overrides**: Estilização avançada para feedback visual de alertas.
 
  ## ✨ Principais Funcionalidades
 
  -   **📦 Gestão de Estoque**: Cadastro, edição e visualização de produtos com controle de saldo.
  -   **📑 Faturamento Inteligente**: Emissão de Notas Fiscais com validação automática de estoque no microserviço de Estoque.
  -   **🛡️ Resiliência com Polly**: Implementação de políticas de **Wait and Retry** com *Exponential Backoff*. Caso o serviço de Estoque esteja
    temporariamente offline, o serviço de Faturamento realiza até 3 tentativas automáticas antes de reportar falha, simulando um ambiente real de alta      
    disponibilidade.
  -   **⚡ Interface Reativa**: Uso extensivo de Signals para garantir que a UI reflita o estado dos dados instantaneamente.
 
  ## ⚙️ Pré-requisitos
 
  Para rodar o projeto localmente, você precisará de:
  -   [.NET SDK 10+](https://dotnet.microsoft.com/download)
  -   [Node.js (LTS)](https://nodejs.org/)
  -   [Angular CLI](https://angular.io/cli)
  -   [MySQL / MariaDB Server](https://mariadb.org/)
 
  ## 🛠️ Como executar o projeto
 
  ### **1. Configuração do Banco de Dados**
  Certifique-se de que o seu servidor MySQL/MariaDB esteja rodando e atualize as strings de conexão nos arquivos `appsettings.json` de cada API (Estoque e
    Faturamento) se necessário.
 
  ### **2. Rodando o Backend**
  Abra dois terminais (um para cada serviço):
   
    *A API rodará por padrão na porta `5111`.*
   
  **Microserviço de Faturamento:**
    cd Korp.Faturamento.API
    dotnet run

    *A API rodará por padrão na porta `5222`.*
   
  ### **3. Rodando o Frontend**
  Em um novo terminal:
    cd Korp-Frontend
    npm install
    npm start

   *Acesse a aplicação em [http://localhost:4200](http://localhost:4200).*
  
   ---
  
   ## 👨‍💻 Autor
  
   Desenvolvido por **[Rodrigo Amorim Neves]**.
  
   [![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](www.linkedin.com/in/rodrigo-amorim7) 
   [![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/AmorimRodrigoo)

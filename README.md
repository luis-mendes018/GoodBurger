<h1>🍔Good Hamburguer🍔</h1>
<h2>Sistema para gestão de pedidos</h2>
<br>
<br>

<h3>Arquitetura Utitlizada: Vertical Slices + Minimal API</h3>

<h4>Por que?</h4>

<p>
 O problema era simples: um sistema para registro de pedidos.
 O cardápio possui informações fixas e imutáveis, então optei por deixá-las em memória.
 Dessa forma, o foco ficou apenas no CRUD de pedidos.
</p>
<br>

<h4>Mas afinal, o que é vertical slices?</h4>

<p>
Vertical Slice Architecture é um padrão de arquitetura que organiza o sistema por funcionalidades completas, em vez de camadas técnicas.
</p>


  <h4> Principais características</h4>
  
  <br>

 <p>

<ul>
  <li>Não exige Controllers, Services ou Repositories como camadas fixas globais</li>
  <li>A estrutura é organizada por funcionalidades (casos de uso), não por camadas técnicas</li>
  <li>Cada feature é independente e contém suas próprias necessidades internas</li>
  <li>O endpoint pode conter a lógica do caso de uso ou delegar para componentes específicos da própria feature</li>
  <li>Evita abstrações desnecessárias e camadas genéricas sem propósito claro</li>
  <li>Foco no fluxo completo da funcionalidade, do input até a resposta</li>
  <li>Código mais direto, coeso e simples de evoluir</li>
</ul>
   
 </p> 
<br>
<h4>Por que não usei arquitetura tradicional nesse contexto?</h4>

<p>
Se eu utilizasse uma arquitetura tradicional em camadas (Clean Architecture + DDD), com controllers, repositórios e outras abstrações,
o desenvolvimento seria mais lento e com complexidade desnecessária.
</p>


<p>
Isso seria um caso clássico de <strong>overengineering</strong>, já que eu estaria adicionando complexidade estrutural sem necessidade real para o problema em questão.
</p>

<br>
<h2>⚙️Instruções de configuração</h2>
<br>

<p>

  Para executar o projeto, é necessário que você tenha os seguintes recursos instalados na sua
  máquina:

  <ul>
    <li>.NET 10</li>
    <li>Docker desktop</li>
    <li>Visual Studio 2026 IDE</li>
  </ul>

</p>
<br>
<h3> Como executar o projeto</h3>

<ol>
  <li>
    Clone o repositório e abra o projeto:
    <pre><code>git clone https://github.com/luis-mendes018/GoodBurger.git</code></pre>
  </li>

  <li>
    Inicialize o Docker Desktop.
  </li>

  <li>
    Abra o PowerShell (de preferência como administrador) e execute os seguintes comandos:
    <pre><code>
cd GoodBurger
docker compose up -d
    </code></pre>
  </li>

  <li>
    Com o projeto aberto na IDE, abra o Console do Gerenciador de Pacotes e aplique as migrations:
    <pre><code>update-database</code></pre>
  </li>

  <li>
    Execute a API e, em seguida, o projeto Blazor.
  </li>
</ol>

Executando a API
<img width="499" height="38" alt="execucao_API" src="https://github.com/user-attachments/assets/914e5100-7329-4760-a361-1a673b6db848" />


Clique no botão direito no projeto GoodBurger.Client e escolha a opção "iniciar sem depurar" para executar o projeto blazor

Executando o blazor
<img width="285" height="343" alt="execucao_blazor" src="https://github.com/user-attachments/assets/9c9d266f-f520-4523-bbc0-4c582afe450f" />


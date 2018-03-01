# AgendaCRUDCSharp

**********************
** Apresentação:    **
**********************
Segue uma aplicação Windows Forms com a implementação de operações C.R.U.D no banco de dados SQL Server 
usando micro-ORM Dapper, metroFramework e Web Service.

**********************
** Recursos Usados: **
**********************
- VS 2015 Community;
- Dapper;
- WebService(SOAP);
- MetroFramework;

*********************
** Objetivo:       **
*********************
Cosumir um Web Service para gerenciar as informações de uma agenda telefônica em uma tabela de banco de dados SQL Server 
com uma aplicação Windows Forms usando a linguagem C#.

*********************
** Motivação:      **
*********************
A Modern UI ou metroFramework é o nome da inteface de usuário(UI), desenvolvida pela Microsoft, principalmente para uso em Windows Phone, 
gratuito para criar interfaces com estilo elegante de forma relativamente simples, interface plana, com cores básicas e desenhos geométricos, 
com a mobilidade horizontal (no PC) ou vertical (no celular).

Foi escolhido a micro ORM, para criar essa aplicação básica com acesso a dados, por que não vale apena uma Entity Framework ou NHibernate que 
são mais indicadas para projetos mais complexos. Contudo, se não há a necesidade de criar ADO .NET puro, o micro ORM que apresenta recursos que 
vão facilitar o desenvolvimento e que tenha desempenho e vai ser justamente aqui que o Dapper se encaixará.

***********************************
** Por que o uso do WebService ? **
***********************************
- Recursos podem ser acessados na Internet via TCP/IP;
- Independente de Sistema Operacional e de qualquer lugar como se o componente estivesse instalado na sua máquina;
- Um componente que não sofre as restições dos Firewalls e totalmente integrado ao seu ambiente;

********************************
** Ao Iniciar:                **
********************************
Após baixar os arquivos, faça o backup do banco (Agenda.bak) e com botão direiro no projeto "Set as startup project" 
em cima do projeto windowsfroms (CRUD_Dapper2).

# EAgendaMedica

O projeto 'EAgendaMedica' simboliza a conclusão do meu curso na Academia do Programador, representando o desenvolvimento completo de um software para gestão de agenda médica. O backend foi estruturado em camadas, abrangendo Apresentação, Distribuição, Aplicação, Domínio, Infraestrutura, Testes Unitários e de Integração. Além disso, o frontend foi implementado utilizando Angular e Bootstrap, proporcionando uma experiência de usuário moderna e responsiva.

Este sistema oferece funcionalidades que possibilitam o cadastro, edição e exclusão de médicos, assim como de suas atividades, abrangendo consultas e cirurgias. Os médicos são identificados por nome e CRM, enquanto as atividades incluem descrição, data, horário de início e fim, tipo (consulta ou cirurgia), e uma lista de médicos participantes.

Destaca-se a implementação de restrições que impedem o cadastro de atividades caso o médico associado esteja envolvido em outra atividade simultânea. Além disso, são estabelecidos intervalos mínimos de descanso de 20 minutos após consultas e 4 horas após cirurgias, visando garantir a adequada participação do médico em novas atividades.

O projeto, embora desafiador, foi enriquecedor, incorporando aprendizados provenientes de experiências prévias. Expresso minha gratidão aos colegas e professores, cujo apoio foi fundamental ao longo de todo o processo.

## Tecnologias Utilizadas
- C#
- .NET Core
- Entity Framework (EF) Core
- ASP.NET Web Apis
- MSTest
- HTML
- CSS
- TypeScript
- Angular (Framework)
- Bootstrap

## Status do Projeto

O projeto encontra-se na versão 1.0, representando um estágio concluído com sucesso. A versão atual oferece um conjunto robusto de funcionalidades, incluindo o cadastro e gerenciamento de médicos, agendamento de atividades como consultas e cirurgias, restrições inteligentes para evitar conflitos de horário e intervalos de descanso adequados para os médicos.

Embora a versão 1.0 marque um ponto de conclusão, estou aberto a retornar ao projeto no futuro para realizar atualizações e implementações adicionais, impulsionado pelo desejo contínuo de aprendizado e aprimoramento.

## Instruções de Instalação

Backend:

- Utilize o Microsoft Visual Studio para abrir o projeto EAgendaMedica.
- No Console do Gerenciador de Pacotes, execute o comando "update-database" no diretório do projeto EAgendaMedica para aplicar as migrações e criar o banco de dados.
- Inicialize o projeto EAgendaMedica.WebApi.

Frontend:

- Utilize o Visual Studio Code (VSCode) para abrir o projeto Angular.
- No console, execute o comando "ng serve --open" no diretório do projeto Angular para iniciar o servidor de desenvolvimento e abrir o aplicativo no navegador.

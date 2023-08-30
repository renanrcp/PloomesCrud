# PloomesCrud

Um Crud simples e básico feito em ASP.NET Core 7 com minimal APIs e Swagger.

## Introdução
Esse exemplo usa Swagger para documentação.

Foi escolhido uma estrura básica de Pessoas, onde essas possuem `name`, `lastName`, `age` e `mainStack`.

Apesar de não possuir validação `mainStack` é para ser uma `runtime`/`framework`/`linguagem de programação` qualquer.

Esse exemplo usa `docker compose` sem `volumes` para deixar mais fácil o reset de banco de dados.

## Uso

### Local
Basta rodar o comando `docker compose up` com o [docker]() instalado em sua máquina.

**OBS**:
Você também pode rodar a API manualmente rodando `dotnet run` e alterando a connection string no arquivo [appsettings.Development.json](appsettings.Development.json)

Para acessar o swagger basta acessar a url [localhost:5071/swagger/index.html](http://localhost:5071/swagger/index.html)

A API Fica no path [localhost:5071/persons](http://localhost:5071/persons)


### Produção

Para acessar o swagger basta accessar a url [ploomescrud.azurewebsites.net](https://ploomescrud.azurewebsites.net/)

A API Fica no path [ploomescrud.azurewebsites.net/persons](https://ploomescrud.azurewebsites.net/persons)


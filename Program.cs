using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PloomesCrud.Data;
using PloomesCrud.Data.Models;
using PloomesCrud.Models;
using PloomesCrud.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));

builder.Services.AddHostedService<MigrationService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Ploomees Person");
});

app.MapGet("/persons", async ([FromServices] DataContext dataContext) =>
{
    var allPersons = await dataContext.Persons.ToArrayAsync();

    return TypedResults.Ok(allPersons.Select(person => person.AsResponse()));
})
.WithOpenApi();

app.MapGet("/persons/{personId}", async ([FromServices] DataContext dataContext, [FromRoute] int personId) =>
{
    var person = await dataContext.Persons.FindAsync(personId);

    if (person == null)
    {
        return Results.NotFound("Não foi possível encontrar uma pessoa com esse id.");
    }

    return TypedResults.Ok(person.AsResponse());
})
.Produces<PersonResponseModel>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.MapPost("/persons", async ([FromServices] DataContext dataContext, [FromBody] PersonCreateRequestModel personRequestModel) =>
{
    var personExists = await dataContext.Persons.AnyAsync(person =>
        person.Name.Equals(personRequestModel.Name) && person.LastName.Equals(personRequestModel.LastName));

    if (personExists)
    {
        return Results.UnprocessableEntity("Já existe uma pessoa com esse nome.");
    }

    var personModel = new PersonModel
    {
        Name = personRequestModel.Name,
        LastName = personRequestModel.LastName,
        Age = personRequestModel.Age,
        MainStack = personRequestModel.MainStack,
    };

    await dataContext.Persons.AddAsync(personModel);

    await dataContext.SaveChangesAsync();

    return TypedResults.Created($"/persons/{personModel.Id}", personModel.AsResponse());
})
.Produces<PersonResponseModel>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status422UnprocessableEntity)
.WithOpenApi();

app.MapPut("/persons", async ([FromServices] DataContext dataContext, [FromBody] PersonUpdateRequestModel personRequestModel) =>
{
    var personModel = await dataContext.Persons.FindAsync(personRequestModel.Id);

    if (personModel == null)
    {
        return Results.NotFound("Não foi possível encontrar uma pessoa com esse id.");
    }

    if (!personModel.Name.Equals(personRequestModel.Name) || !personModel.LastName.Equals(personRequestModel.LastName))
    {
        var personExists = await dataContext.Persons.AnyAsync(person =>
        person.Name.Equals(personRequestModel.Name) && person.LastName.Equals(personRequestModel.LastName));

        if (personExists)
        {
            return Results.UnprocessableEntity("Já existe uma pessoa com esse nome.");
        }
    }

    personModel.Name = personRequestModel.Name;
    personModel.LastName = personRequestModel.LastName;
    personModel.Age = personRequestModel.Age;
    personModel.MainStack = personRequestModel.MainStack;

    await dataContext.SaveChangesAsync();

    return TypedResults.Ok(personModel.AsResponse());
})
.Produces<PersonResponseModel>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status422UnprocessableEntity)
.WithOpenApi();

app.MapDelete("/persons/{personId}", async ([FromServices] DataContext dataContext, [FromRoute] int personId) =>
{
    var person = await dataContext.Persons.FindAsync(personId);

    if (person == null)
    {
        return Results.NotFound("Não foi possível encontrar uma pessoa com esse id.");
    }

    dataContext.Persons.Remove(person);

    await dataContext.SaveChangesAsync();

    return Results.NoContent();
})
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status204NoContent)
.WithOpenApi();

app.Run();



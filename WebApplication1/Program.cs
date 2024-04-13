using Microsoft.AspNetCore.Mvc;
using WebApplication1;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

AnimalDb animalDb = new AnimalDb();
WizytaDb wizytaDb = new WizytaDb();

app.MapGet("/Animals", () =>
{
    var animals = animalDb.GetListData();
    return Results.Ok(animals);
});

app.MapGet("/Animals/{id:int}", ([FromQuery] int id) =>
{
    var animal = animalDb.GetById(id);

    if (animal is null)
        return Results.NotFound($"animal id: {id} not found");
    
    return Results.Ok(animal);
});

app.MapPost("/Animals", ([FromBody] Animal animal) =>
{
    animalDb.Add(animal);
    return Results.Created();
});

app.MapPut("/Animals/{id:int}", ([FromRoute] int id, [FromBody] Animal animal) =>
{
    if (animalDb.GetListData().Exists(a => a.Id == id))
    {
        var a = animalDb.GetById(id);
        a.Name = animal.Name;
        a.Category = animal.Category;
        a.Weight = animal.Weight;
        a.Color = animal.Color;
        return Results.NoContent();
    }
    
    animalDb.Add(animal);
    return Results.Created();
});

app.MapDelete("/Animals/{id:int}", ([FromRoute] int id) =>
{
    animalDb.DeleteById(id);
    return Results.NoContent();
});

app.MapGet("/Visit/{id:int}", ([FromRoute] int id) =>
{
    var temp = wizytaDb.GetByAnimalId(id);

    if (temp.Count == 0)
        return Results.NotFound();
    
    return Results.Ok(temp);
});

app.MapPost("/Visit{idAnimal:int},{date:DateTime},{cost:double}", ([FromRoute] int idAnimal, [FromRoute] DateTime date, [FromRoute] double cost, 
    [FromBody] string description) =>
{
    if (animalDb.GetById(idAnimal) is not null)
    {
        wizytaDb.AddVisit(new Wizyta(date, animalDb.GetById(idAnimal), description, cost));
        return Results.NoContent();
    }
        
        
    return Results.NotFound($"Animal: {idAnimal} not exists in database");
});

animalDb.Add(new Animal(){Weight = 12.5, Category = "pies", Color = "black", Name = "abcd"});

app.Run();
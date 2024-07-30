using Microsoft.EntityFrameworkCore;
using Todos.WebAPI.Context;
using Todos.WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");
app.MapGet("/getall", (ApplicationDbContext context) => Results.Ok(context.Todos.ToList()));

app.MapGet("/create", (ApplicationDbContext context, string work) =>
{
    Todo todo = new()
    {
        Work = work
    };

    context.Add(todo);
    context.SaveChanges();
    Results.Ok(todo);
});


using (var scope = app.Services.CreateScope())
{
    var srv = scope.ServiceProvider;
    var context = srv.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

app.Run();

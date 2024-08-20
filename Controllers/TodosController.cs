using back_dotnet.Db;
using back_dotnet.Db.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_dotnet.Controllers;

public static class TodosController
{
    public static void MapTodosEndpoints(this  WebApplication app)
    {
        var routeGroup = app.MapGroup("todos");
        
        routeGroup.MapGet("", 
            (AppDbContext appDb, CancellationToken ct = default) 
                => appDb.Todos
                    .OrderBy(x => x.AddedDate)
                    .ToListAsync(ct));

        routeGroup.MapPost("", async (
            [FromBody] AddTodoRequest req, AppDbContext appDb, CancellationToken ct) =>
        {
            if (string.IsNullOrEmpty(req.Name))
                return Results.BadRequest($"{nameof(req.Name)} is required");
            
            var id = Guid.NewGuid();
            var todo = new Todo(req.Name, id);
            await appDb.AddAsync(todo, ct);

            await appDb.SaveChangesAsync(ct);

            return Results.Ok(todo);
        });
    }
}
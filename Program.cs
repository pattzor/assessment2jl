using myAPi;

#region Connection string
string? connectionstring = Environment.GetEnvironmentVariable("ConnectionString");
if(string.IsNullOrEmpty(connectionstring)) {
    connectionstring = "Server=localhost,1433;Database=Pluto;User Id=sa;Password=Password123;";
    Console.WriteLine("No environment variable found, using default");
}
else {
    Console.WriteLine("Environment variable ConnectionString found: ");
}
Console.WriteLine(connectionstring);
#endregion
    
SQL sql = new SQL(connectionstring);

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Add static files, served from wwwroot default
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/contacts", () =>
{
    List<Contact>? contacts = sql.GetContacts();

    if (contacts != null)
        return Results.Ok(contacts);

    return Results.BadRequest();
});

app.MapGet("/contact", (int id) =>
{
    var contact = sql.GetContact(id);

    if (contact.id == id)
        return Results.Ok(contact);

    return Results.NotFound();
});

app.MapPost("/contact", (Contact contact) =>
{
    if (sql.AddContact(contact))
        return Results.Ok(contact);

    return Results.NotFound();
});

app.MapDelete("/contact", (int id) =>
{
    if (sql.DeleteContact(id))
        return Results.Ok(id);

    return Results.NotFound();
});

app.MapPut("/contact", (Contact contact) => Results.Ok(sql.UpdateContact(contact)));

app.Run();
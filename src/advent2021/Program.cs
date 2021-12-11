global using Microsoft.AspNetCore.Mvc;
global using System.IO;

using Honeycomb.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHoneycomb(builder.Configuration);
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));

    options.TagActionsBy(api => new List<string> { api.GroupName ?? api.ActionDescriptor.DisplayName ?? "Other" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
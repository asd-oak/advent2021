global using Microsoft.AspNetCore.Mvc;
global using OpenTelemetry.Trace;
global using System.IO;

using advent2021.Middleware;
using Honeycomb.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHoneycomb(builder.Configuration);
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
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
    app.UseSwaggerUI(options =>
    {
        options.EnableTryItOutByDefault();
        options.EnableDeepLinking();
        options.DisplayRequestDuration();
        options.ConfigObject.AdditionalItems.Add("requestSnippetsEnabled", true);
    });
}

app.UseHttpsRedirection();

app.UseRequestAttributeTracing();

app.MapControllers();

app.Run();

// https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0#basic-tests-with-the-default-webapplicationfactory
public partial class Program { }
using System.Reflection;
using CarbonAware;
using CarbonAware.Aggregators.Configuration;
using CarbonAware.WebApi.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     var filePath = Path.Combine(System.AppContext.BaseDirectory, "CarbonAware.WebApi.xml");
     c.IncludeXmlComments(filePath);
     c.CustomOperationIds(apiDesc =>
        {
            return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
        });
});
builder.Services.Configure<CarbonAwareVariablesConfiguration>(builder.Configuration.GetSection(CarbonAwareVariablesConfiguration.Key));
builder.Services.AddCarbonAwareEmissionServices(builder.Configuration);
CarbonAwareVariablesConfiguration config = new CarbonAwareVariablesConfiguration();

builder.Configuration.GetSection(CarbonAwareVariablesConfiguration.Key).Bind(config);

builder.Services.AddHealthChecks();

// AppInsights connection string should be specified as an environment variable for this to work
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

if (config.WebApiRoutePrefix != null)
{
    app.UsePathBase(config.WebApiRoutePrefix);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
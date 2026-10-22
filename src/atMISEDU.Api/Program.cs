using Asp.Versioning;
using atMISEDU.Application.DependencyInjection.Extension;
using atMISEDU.Contract.Abstractions;
using atMISEDU.Contract.Constants;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Middlewares;
using atMISEDU.Contract.Services;
using atMISEDU.Persistence.DependencyInjection.Extensions;
using atMISEDU.Presentation.Abstractions;
using atMISEDUatMISEDUAPI.DependencyInjection.Extensions;
using atMISEDUatMISEDUAPI.DependencyInjection.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddCors(options => { options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });
builder.Services.AddEndpointsApiExplorer();

var swaggerSection = builder.Configuration.GetSection("DomainHosts");
var serverUrl = swaggerSection.GetValue<string>("Url") ?? "";
var baseURL = swaggerSection.GetValue<string>("BaseSwaggerURL") ?? "";
builder.Services.AddSwaggerGen(options =>
{
    options.AddServer(new OpenApiServer
    {
        Url = serverUrl
    });
});
var serviceName = "atMISEDU API";
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddApplicationPart(Assembly.GetExecutingAssembly());
builder.Services.AddConfigSetting(builder.Configuration);

//register api configuration
builder.Services.AddSingleton(new ApiConfig { Name = serviceName });
//Configure swagger
builder.Services.ConfigureOptions<SwaggerConfigureOptions>();

//Configure api versioning
builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader(),
            new HeaderApiVersionReader("x-api-version"),
            new QueryStringApiVersionReader());
    })
    .AddMvc()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    options.SuppressConsumesConstraintForFormFileParameters = true;
    options.InvalidModelStateResponseFactory = context => null;
});

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Environment.AddEnvironmentHelper();
builder.Services.AddProblemDetails();
builder.Environment.AddEnvironmentHelper();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddHttpClient();
var app = builder.Build();

app.UseValidateModel();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseApiLayerSwagger();
//app.UseHsts();
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.MapPresentation();

app.Run();
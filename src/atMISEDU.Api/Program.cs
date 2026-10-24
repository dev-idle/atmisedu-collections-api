using Asp.Versioning;
using atMISEDU.Application.DependencyInjection.Extension;
using atMISEDU.Contract.Abstractions;
using atMISEDU.Contract.Constants;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Errors;
using atMISEDU.Contract.Middlewares;
using atMISEDU.Contract.Services;
using atMISEDU.Contract.Shared;
using atMISEDU.Persistence.DependencyInjection.Extensions;
using atMISEDU.Presentation.Abstractions;
using atMISEDUatMISEDUAPI.DependencyInjection.Extensions;
using atMISEDUatMISEDUAPI.DependencyInjection.Options;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddCors(options => { options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });
builder.Services.AddEndpointsApiExplorer();

// Swagger UI calls the host it is served from, works with both http and https profile
builder.Services.AddSwaggerGen();
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
    options.SuppressConsumesConstraintForFormFileParameters = true;
    // Request can not be bound (invalid JSON, wrong data type): return 400 in Result format
    options.InvalidModelStateResponseFactory = context =>
    {
        var details = context.ModelState
            .Where(m => m.Value?.Errors.Count > 0)
            .SelectMany(m => m.Value!.Errors.Select(e => $"{m.Key}: {e.ErrorMessage}"))
            .ToArray();
        return new BadRequestObjectResult(new Result<object>
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
            MessageCode = MsgCode.ERR_BAD_REQUEST,
            Error = new Error(string.Empty, details)
        });
    };
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
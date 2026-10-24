using atMISEDU.Application.DependencyInjection.Extension;
using atMISEDU.Grpc.Interceptors;
using atMISEDU.Grpc.Services.v1.COLLECTIONS;
using atMISEDU.Persistence.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register gRPC, the interceptor converts exceptions of handlers to gRPC status
builder.Services.AddGrpc(options => options.Interceptors.Add<ExceptionInterceptor>());
builder.Services.AddGrpcReflection();

// Reuse the same Application and Persistence layers as REST api
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

app.MapGrpcService<CollectionTagsGrpcService>();
// Reflection lets Postman and grpcurl discover services, only in development
if (app.Environment.IsDevelopment())
    app.MapGrpcReflectionService();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

app.Run();

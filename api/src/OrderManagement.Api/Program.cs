using OrderManagement.Api.BackgroundServices;
using OrderManagement.Api.Filters;
using OrderManagement.Application;
using OrderManagement.Infra;
using OrderManagement.Infra.Migrations;
using OrderManagement.Infra.Services.Hubs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddInfra(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddHostedService<UpdateOrderStatusService>();

var allowedOrigins = builder.Configuration["AllowedOrigins"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins!)
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/health", () => Results.Ok());

await MigrateDatabaseAsync(app);

app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();
app.MapHub<OrderStatusHub>("/orderStatus");

app.Run();

async Task MigrateDatabaseAsync(IApplicationBuilder app)
{
    await using var scope = app.ApplicationServices.CreateAsyncScope();

    await DatabaseMigration.MigrateDatabase(scope.ServiceProvider);

}

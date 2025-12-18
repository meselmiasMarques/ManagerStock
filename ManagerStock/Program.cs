using ManagerStock.Data;
using ManagerStock.Repositories;
using ManagerStock.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Management Stock ",
        Version = "v1",
        Description = "Api de gerenciamento de produtos de um estoque, loja virtual"
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=app.db; Cache=Shared");
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ProductService>();


var app = builder.Build();

// Enable Swagger only in development (boa prática)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ManagerStock API v1");
        options.RoutePrefix = string.Empty; // Deixa o Swagger na raiz: http://localhost:5000
    });
}

app.MapControllers();
app.Run();

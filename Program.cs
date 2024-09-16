using ConnectData.Api.Data.Contexts;
using ConnectData.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var kafkaBootstrapServers = "localhost:9092"; // Endereço do seu broker Kafka
var kafkaTopic = "Clients"; // Nome do tópico que você deseja consumir
var kafkaGroupId = "1"; // ID do grupo de consumidores

// Adicionar o serviço de fundo Kafka
builder.Services.AddScoped<ClienteService>(); // Registrar o serviço
builder.Services.AddScoped<FibraService>(); // Registrar o serviço



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configurando logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


// Configurando Contexs
builder.Services.AddDbContext<ConnectDataContext>(options => options.UseInMemoryDatabase("InMemory"));
builder.Services.AddHostedService<KafkaConsumerService>();

    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

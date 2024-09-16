using Confluent.Kafka;
using ConnectData.Api.Resources;
using ConnectData.Api.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public class KafkaConsumerService : IHostedService
{
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly string _bootstrapServers;
    private readonly string _topic;
    private readonly string _groupId;
    private readonly IServiceScopeFactory _scopeFactory; // Adicione esta variável
    private CancellationTokenSource _cts;
    private Task _executingTask;

    public KafkaConsumerService(ILogger<KafkaConsumerService> logger, IConfiguration configuration, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _bootstrapServers = configuration["Kafka:BootstrapServers"];
        _topic = configuration["Kafka:Topic"];
        _groupId = configuration["Kafka:GroupId"];
        _scopeFactory = scopeFactory;
        Console.WriteLine(configuration["Kafka:BootstrapServers"]);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _executingTask = Task.Run(() => ExecuteAsync(_cts.Token), _cts.Token);
        return Task.CompletedTask;
    }

    private async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _bootstrapServers,
            GroupId = _groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using (var consumer = new ConsumerBuilder<string, string>(config).Build())
        {
            consumer.Subscribe(_topic);

            try
            {
                while (true)
                {
                    var cr = consumer.Consume(cancellationToken);
                    var key = cr.Message.Key; // Supondo que a chave esteja disponível aqui

                    // Seleciona o serviço e o recurso com base na chave da mensagem
                    using (var scope = _scopeFactory.CreateScope()){
                        var clienteService = scope.ServiceProvider.GetRequiredService<ClienteService>();
                        var fibraService = scope.ServiceProvider.GetRequiredService<FibraService>();

                    if (key.ToString() == "Cliente")
                    {
                        var clienteResource = JsonSerializer.Deserialize<ClienteResource>(cr.Message.Value);
                        if (clienteResource != null)
                        {
                            clienteService.CreateClient(clienteResource); // Processa a mensagem usando ClienteService
                        }
                    }
                    else if (key.ToString() == "Fibra")
                    {

                        var fibraResource = JsonSerializer.Deserialize<FibraResource>(cr.Message.Value);
                        if (fibraResource != null)
                        {
                            fibraService.CreateFibra(fibraResource); // Processa a mensagem usando FibraService
                        }
                    }
                    }
                    // Processar a mensagem recebida
                }
            }
            catch (ConsumeException e)
            {
                _logger.LogError($"Erro ao consumir mensagem: {e.Error.Reason}");
            }
            finally
            {
                consumer.Close();
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cts.Cancel();
        return Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
    }
}
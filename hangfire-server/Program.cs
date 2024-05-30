using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repo;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World! From Server");

IHostBuilder builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services => {
	services.AddHostedService<JobScheduler>();
	services.AddScoped<IJobRepo, JSONJobRepo>();
	services.AddScoped<IJobFactory, JobFactory>();
	services.AddTransient<IJob, CsvToLedger>();
});

IHost host = builder.Build();

await host.RunAsync();

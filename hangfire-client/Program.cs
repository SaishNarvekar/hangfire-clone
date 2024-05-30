using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<JobContext>(options => {
	options.UseSqlite("Data Source=../hangfire-db/jobs.db");
});
builder.Services.AddScoped<IJobRepo, JobRepo>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/", (IJobRepo repo, Job job) => {
	repo.AddJob(job);
	return "Hello, World";		
});

app.MapGet("/jobs",(IJobRepo repo) => repo.GetJobs());

// Ensure the database is created and apply any pending migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<JobContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();

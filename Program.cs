using DotNetEnv;
using twilio_messager.Models;
using twilio_messager.Services;

var builder = WebApplication.CreateBuilder(args);
Env.Load();
// Add services to the container.
builder.Services.Configure<TwilioSettings>(options =>
{
    options.AccountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID")
                         ?? throw new InvalidOperationException("TWILIO_ACCOUNT_SID is not set in .env");
    options.AuthToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN")
                        ?? throw new InvalidOperationException("TWILIO_AUTH_TOKEN is not set in .env");
});
builder.Services.AddSingleton<TwilioService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

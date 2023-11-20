using CurrencyExchangeApi;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models.Commands;
using CurrencyExchangeService.Core.Models.Queries;
using CurrencyExchangeService.Core.Services;
using CurrencyExchangeService.Infrastructure.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();

builder.Services.AddScoped<ICommandService<AddCurrencyCommand>, AddCurrencyService>();
builder.Services.AddScoped<ICommandService<CreateUserCommand>, CreateUserService>();
builder.Services.AddScoped<ICommandService<UpdateUserBalanceCommand>, UpdateUserBalanceService>();
builder.Services.AddScoped<IQueryService<string, CurrencyQuery>, GetCurrencyService>();
builder.Services.AddScoped<IQueryService<string, UserQuery>, GetUserService>();

builder.Services.AddDbContext<CurrencyExchangeDbContext>();
builder.Services.AddScoped<CurrencyExchangeDbContext>();

builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
{
    Title = "Currency Exchange Service API"
}));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
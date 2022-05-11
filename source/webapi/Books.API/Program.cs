using BooksStore.Bll;
using BooksStore.CacheDal;
using BooksStore.CacheDal.Persistence;
using BooksStore.Core.Configuration;
using BooksStore.Core.Interfaces;
using BooksStore.SqlDal;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DataStoreSettings>(builder.Configuration.GetSection(nameof(DataStoreSettings)));
builder.Services.AddSingleton<IDataStoreSettings>(sp => sp.GetRequiredService<IOptions<DataStoreSettings>>().Value);

builder.Services.AddSingleton<IRedisCacheDbContext, RedisCacheDbContext>();
builder.Services.AddScoped<IBooksBll, BooksBll>();
builder.Services.AddScoped<IBookCacheRepository, BookCacheRepository>();

builder.Services.AddScoped<IBookRepository, BookRepository>();

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

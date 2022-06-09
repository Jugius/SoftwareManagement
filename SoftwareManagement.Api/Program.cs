using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SoftwareManagement.Api;
using SoftwareManagement.Api.Database;
using SoftwareManagement.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Bind("ProjectConfiguration", new AppConfig());
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(AppConfig.ConnectionString));

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //кодирование кириллицы (иначе на выходе строка в UTF8)
        //options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic);

        //Сериализация типа Enum в строку
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        //запись форматированного JSON 
        //options.JsonSerializerOptions.WriteIndented = true;

        //сбрасываем в ноль формат имен свойств (по умолчанию CamelCase, вида "myData")
        options.JsonSerializerOptions.PropertyNamingPolicy = null;

        //игнорировать свойства с нулевыми значениями
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Добавляем сервисы api

builder.Services.AddScoped<ApplicationsService>();
builder.Services.AddScoped<ReleasesService>();
builder.Services.AddScoped<ReleaseDetailsService>();


builder.Services.AddSingleton<FileSystemService>();
builder.Services.AddSingleton<RequestValidationService>();

#if DEBUG
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

#if DEBUG
app.UseSwagger();
app.UseSwaggerUI();
#endif

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

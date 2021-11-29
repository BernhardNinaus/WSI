using Microsoft.EntityFrameworkCore;
using WSI.WortFilter;
using WSI.KommentarService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddWortFilter()
    .AddDefaultWortSuche()
    .AddDefautlWortAustasuch()
    .AddEinfacherWortFilter(builder.Configuration.GetSection("NichtErlaubteWorte").Get<string[]>());

builder.Services.AddDbContext<DataContext>(options => 
    options.UseInMemoryDatabase("InMem"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.Extensions.Options;
using RedLine.Services.Catalog.Filters;
using RedLine.Services.Catalog.Mapping;
using RedLine.Services.Catalog.Services;
using RedLine.Services.Catalog.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});


builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IGunService, GunService>();
builder.Services.AddAutoMapper(typeof(MapProfile));
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped(typeof(NotFoundFilter<>));

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.Run();



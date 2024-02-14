using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Redline.Services.Basket.Services;
using Redline.Services.Basket.Settings;
using RedLine.Shared.Services;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Basket API",
        Version = "v1",
        Description = "Basket API",
        Contact = new OpenApiContact
            {
                Name = "Mehmet Tekin ALTUN",
                Email = "mehmettekinaltun@gmail.com",
                Url = new Uri("https://github.com/mtaltn")
            }
    }));

//JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddControllers();

builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSetting = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
    RedisService redis = new(redisSetting.Host, redisSetting.Port);
    redis.Connect();
    return redis;
});

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "v1");
        c.RoutePrefix = "swagger";
    });
}

app.MapControllers();

app.Run();


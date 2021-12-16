using System.Reflection;
using DAL.Implementations;
using Lib;
using Microsoft.OpenApi.Models;
using WebApplication1.DeviceGuid;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
services.AddLib();
services.AddDAL();
services.AddHttpContextAccessor();
services.AddDistributedMemoryCache();
services.AddSession(options => {
    // Session 時間設定為最大
    options.IdleTimeout = TimeSpan.MaxValue;
});
// Client 裝置Guid Provider(存在session之中)
services.AddScoped<IClientDeviceGuidProvider,ClientDeviceGuidProvider>();
var app = builder.Build();
// 建立測試用DataBase
app.Services.MigrateDataBaseAndAddTestData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {

        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseSession();
app.MapControllers();

app.Run();

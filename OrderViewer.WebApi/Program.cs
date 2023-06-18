using Microsoft.AspNetCore.Authentication.Cookies;
using OrderViewer.DAL.Interfaces;
using OrderViewer.DAL.Repositories;
using OrderViewer.Service.Implementaitons;
using OrderViewer.Service.Interfaces;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //builder.Services.AddTransient<IUserOrderService, UserOrderService>();

        //builder.Services.AddSingleton<IUserOrderService, UserOrderService>();
        //builder.Services.AddSingleton<IUserOrderService, UserOrderService>();

        builder.Services.AddScoped<IUserOrderRepository, UserOrderRepository>();
        builder.Services.AddScoped<IUserOrderService, UserOrderService>();

        //builder.Services.AddDistributedMemoryCache();
        //builder.Services.AddSession();

        //Сервис аутификации
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    //options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Home/Login");
                });

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        //app.UseSession();

        

        app.UseHttpsRedirection();

        app.UseAuthentication();    // аутентификация
        app.UseAuthorization();     // авторизация

        //app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
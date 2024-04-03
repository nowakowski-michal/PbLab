using BLL_EF;
using BLL;
using BLL.ServiceInterfaces;
using DAL;
using BLL_DB;
namespace NowakowskiLab2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<WebshopContext>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<IBasketServiceDB, BasketServiceDB>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductServiceDB, ProductServiceDB>();
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
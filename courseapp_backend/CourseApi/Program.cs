using CourseApi.Context;
using Microsoft.EntityFrameworkCore;

namespace CourseApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();


            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(OptionsBuilderConfigurationExtensions =>
            //{
            //    options.Token
            //})

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularClient",
                    policy => policy
                        .WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                );
            });


            var app = builder.Build();


            app.UseCors("AllowAngularClient");
            app.UseHttpsRedirection();

            //app.UseCors();
            app.UseAuthorization();

            // ✅ Map controller routes
            app.MapControllers();

            app.Run();
        }
    }
}

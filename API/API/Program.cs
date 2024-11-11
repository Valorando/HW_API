using API.Services;
using API.Interfaces;
namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var requestsSection = configuration.GetSection("Requests");

            var sqlRequests = new Dictionary<string, string>();
            foreach (var request in requestsSection.GetChildren())
            {
                sqlRequests[request.Key] = request.Value;
            }


            // Add services to the container.
            builder.Services.AddScoped<IAPIService, APIService>(provider => new APIService(connectionString, sqlRequests));


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

            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

using Unity;
using EntityService;
using EntityService.Interfaces;
using AutoMapper;
using WebAPI.Mappings;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            //.net Core using this way 
            builder.Services.AddSingleton<IPostService, PostService>();
            builder.Services.AddSingleton<ICommentService, CommentService>();

            //builder.Services.AddScoped<ICommentService, CommentService>();

            // Add services to the containers.
            ConfigureServices(builder.Services);

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
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Create Unity container
            var container = new UnityContainer();

            // Register dependencies
            //container.RegisterType<IPostService, PostService>(); // Example registration
            //container.RegisterType<ICommentService, CommentService>();

            // Register Unity container as a service provider
            services.AddSingleton<IUnityContainer>(container);
        }

        private static IMapper ConfigureMapper()
        {
            // Configure AutoMapper profiles
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>(); // Add AutoMapper profile
                                                     
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}

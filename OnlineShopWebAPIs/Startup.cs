
using System.Linq;
using System.Text;
using BusinessLogic.Repository.BusinessRepository;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Interfaces.IBusinessRepository;
using Models.Interfaces.IServices;
using Models.Interfaces.IUnitOfWork;
using Models.Models;
using OnlineShopWebAPIs.BusinessLogic.DBContext;
using OnlineShopWebAPIs.BusinessLogic.UnitOfWork;
using OnlineShopWebAPIs.Helpers;
using OnlineShopWebAPIs.Models.SettingsModels;
using Serilog;

namespace OnlineShopWebAPIs
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
        }


        public void ConfigureServices(IServiceCollection services)
        {

            //CORS policy
            services.AddCors(opt => opt.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()) );


            //Bind appsettings Node properties of "WebAppSettings" with the Model "WebAppSettings"
            services.Configure<WebAppSettings>(Configuration.GetSection("WebAppSettings"));

            services.Configure<TokenSettings>(Configuration.GetSection("Jwt"));

            //Controllers Config + Self-Referencing loop config
            services.AddControllers().AddNewtonsoftJson(options=>options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            //identity
            services.AddIdentity<IdentityUserContext, IdentityRole>(opt=> { opt.User.RequireUniqueEmail = true;
                opt.Password.RequiredLength = 8; })
                .AddEntityFrameworkStores<OnlineShopDbContext>().AddDefaultTokenProviders();

            //JWT
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt => { opt.SaveToken = true;
            opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration["Jwt:ValidIssuer"],
                    ValidAudience = Configuration["Jwt:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
                
            services.AddAuthorization();




            //Services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            services.AddScoped<IProductsRepository, ProductsRepository>();


            //DbContext congig
            services.AddDbContext<OnlineShopDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OnlineShop_DB")));


            //IUnitOfWork Config
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //AutoMapper config
            services.AddAutoMapper(typeof(ApplicationMappingProfile));


            //ApiController overriding default modelstate behavior
            services.Configure<ApiBehaviorOptions>(opt => {
                opt.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                                 .SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();

                    return new BadRequestObjectResult(new{ Errors = errors });

                };

            });



            //Swagger config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnlineShopWebAPIs", Version = "v1" });
            });



        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineShopWebAPIs v1"));
            }

            app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

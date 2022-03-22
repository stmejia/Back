using Aguila.Core.CustomEntities;
using Aguila.Core.Interfaces;
using Aguila.Core.Interfaces.Repositories;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.Services;
using Aguila.Infrastructure.Data;
using Aguila.Infrastructure.Filters;
using Aguila.Infrastructure.Mappings;
using Aguila.Infrastructure.Options;
using Aguila.Infrastructure.Repositories;
using Aguila.Infrastructure.Services;
using Aguila.Interfaces.Trafico.Data;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;


namespace Aguila.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //aqui vamos a configurar el paquete AutoMapper para hacer los mapeos de Entity a DTo mas faciles y automatizados

            services.AddAutoMapper(System.AppDomain.CurrentDomain.GetAssemblies());

            //aqui vamos a indicar la cadena de conexion que se definio en appsettings.json para la base de datos Aguila
            services.AddDbContext<AguilaDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AguilaDB")).EnableSensitiveDataLogging()
                );

            //Database Trafico
            services.AddDbContext<TraficoDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TraficoDB")).EnableSensitiveDataLogging()
                );

            //Aqui vamos a administrar las dependencias (inyeccion de dependencias) (interfaces)

            services.AddScoped(typeof(IRepository<>), typeof(_BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();


            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Aguila API", Version = "v1" });

                // con esto creamos un xml con los comentarios que ponemos con summary , en cada metodo de los controladores
                // para que sean incluidos en la documentacion de nuestra api

                var xmlFile = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                doc.IncludeXmlComments(xmlFile);
            });

            // Uri Service
            services.AddSingleton<IUriService>(provider =>
            {
                // Resolviendo las urls para hateos
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(
                        request.Scheme, 
                        "://",
                        request.Host.ToUriComponent(),
                        request.PathBase.HasValue ? request.PathBase + "/" : ""
                    );
                return new UriService(absoluteUri);
            });

            // Repositorios
            services.AddScoped(typeof(IRepository<>), typeof(_BaseRepository<>));

            // servicios

            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddTransient<IEmpresaService, EmpresaService>();
            services.AddTransient<ISucursalService, SucursalService>();
            services.AddTransient<IEstacionesTrabajoService, EstacionesTrabajoService>();
            services.AddTransient<IUsuariosService, UsuariosService>();
            services.AddTransient<IModulosService, ModulosService>();
            services.AddTransient<IRecursosService, RecursosService>();
            services.AddTransient<IModulosMnuService, ModulosMnuService>();
            services.AddTransient<IAsigUsuariosModulosService, AsigUsuariosModulosService>();
            services.AddTransient<IAsigUsuariosEstacionesTrabajoService, AsigUsuariosEstacionesTrabajoService>();
            services.AddTransient<IRecursosAtributosService, RecursosAtributosService>();
            services.AddTransient<IAsigUsuariosRecursosAtributosService, AsigUsuariosRecursosAtributosService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<IUsuariosRolesService, UsuariosRolesService>();
            services.AddTransient<IUsuariosRecursosService, UsuariosRecursosService>();
            services.AddTransient<IImagenesRecursosService, ImagenesRecursosService>();
            services.AddTransient<IImagenesRecursosConfiguracionService, ImagenesRecursosConfiguracionService>();
            services.AddTransient<IpaisesService, paisesService>();
            services.AddTransient<ItiposListaService, tiposListaService>();
            services.AddTransient<IdepartamentosService, departamentosService>();
            services.AddTransient<ImunicipiosService, municipiosService>();
            services.AddTransient<IubicacionesService, ubicacionesService>();
            services.AddTransient<IdireccionesService, direccionesService>();
            services.AddTransient<IentidadComercialService, entidadComercialService>();
            services.AddTransient<IentidadesComercialesDireccionesService, entidadesComercialesDireccionesService>();
            services.AddTransient<ItipoClientesService, tipoClientesService>();
            services.AddTransient<IcorporacionesService, corporacionesService>();
            services.AddTransient<IlistasService, listasService>();
            services.AddTransient<ItipoReparacionesService, tipoReparacionesService>();
            services.AddTransient<IreparacionesService, reparacionesService>();
            services.AddTransient<ItipoActivosService, tipoActivosService>();
            services.AddTransient<IactivoGeneralesService, activoGeneralesService>();
            services.AddTransient<ItipoProveedoresService, tipoProveedoresService>();
            services.AddTransient<IpilotosTiposService, pilotosTiposService>();
            services.AddTransient<ItipoMecanicosService, tipoMecanicosService>();
            services.AddTransient<IllantaTiposService, llantaTiposService>();
            services.AddTransient<ItipoEquipoRemolqueService, tipoEquipoRemolqueService>();
            services.AddTransient<ItipoVehiculosService, tipoVehiculosService>();
            services.AddTransient<ItipoGeneradoresService, tipoGeneradoresService>();
            services.AddTransient<IclientesService,clientesService>();
            services.AddTransient<IproveedoresService, proveedoresService>();
            services.AddTransient<ItransportesService, transportesService>();
            services.AddTransient<IrutasService, rutasService>();
            services.AddTransient<IserviciosService, serviciosService>();
            services.AddTransient<IclienteServicioService, clienteServiciosService>();
            services.AddTransient<IempleadosService, empleadosService>();
            services.AddTransient<IasesoresService, asesoresService>();
            services.AddTransient<IpilotosService, pilotosService>();
            services.AddTransient<IpilotosDocumentosService, pilotosDocumentosService>();
            services.AddTransient<IactivoOperacionesService, activoOperacionesService>();
            services.AddTransient<IvehiculosService, vehiculosService>();
            services.AddTransient<ImedidasService, medidasService>();
            services.AddTransient<IinvCategoriaService, invCategoriaService>();
            services.AddTransient<IinvSubCategoriaService, invSubCategoriaService>();
            services.AddTransient<IproductosService, productosService>();
            services.AddTransient<IinvProductoBodegaService, invProductoBodegaService>();
            services.AddTransient<IequipoRemolqueService, equipoRemolqueService>();
            services.AddTransient<IgeneradoresService, generadoresService>();
            services.AddTransient<IinvUbicacionBodegaService, invUbicacionBodegaService>();
            services.AddTransient<IproductosBusquedaService, productosBusquedaService>();
            services.AddTransient<IestadosService, estadosService>();
            services.AddTransient<IactivoEstadosService, activoEstadosService>();
            services.AddTransient<IactivoUbicacionesService, activoUbicacionesService>();
            services.AddTransient<ImecanicosService, mecanicosService>();
            services.AddTransient<IllantasService, llantasService>();
            services.AddTransient<IllantaActualService, llantaActualService>();
            services.AddTransient<ItarifarioService, tarifarioService>();
            services.AddTransient<IclienteTarifasService, clienteTarifasService>();
            services.AddTransient<IactivoMovimientosService, activoMovimientosService>();
            services.AddTransient<IcondicionActivosService, condicionActivosService>();
            services.AddTransient<IactivoMovimientosActualService, activoMovimientosActualService>();
            services.AddTransient<IcondicionCabezalService, condicionCabezalService>();
            services.AddTransient<IcondicionEquipoService, condicionEquipoService>();
            services.AddTransient<IcondicionGenSetService, condicionGenSetService>();
            services.AddTransient<IcondicionTecnicaGenSetService, condicionTecnicaGenSetService>();
            services.AddTransient<IcondicionCisternaService, condicionCisternaService>();
            services.AddTransient<IcondicionFurgonService, condicionFurgonService>();
            services.AddTransient<IeventosControlEquipoService, eventosControlEquipoService>();
            services.AddTransient<IcontrolVisitasService, controlVisitasService>();
            services.AddTransient<IcontrolContratistasService, controlContratistasService>();
            services.AddTransient<IcontrolEquipoAjenoService, controlEquipoAjenoService>();
            services.AddTransient<IempleadosIngresosService, empleadosIngresosService>();
            services.AddTransient<IcondicionTallerVehiculoService, condicionTallerVehiculoService>();
            services.AddTransient<IdetalleCondicionService, detalleCondicionService>();
            services.AddTransient<IcondicionContenedorService, condicionContenedorService>();
            services.AddTransient<IcodigoPostalService, codigoPostalService>();

            services.AddTransient<IAguilaMap, AguilaMap>();

            services.Configure<PaginationOptions>(options => Configuration.GetSection("Pagination").Bind(options));
            services.Configure<PasswordOptions>(options => Configuration.GetSection("PasswordOptions").Bind(options));
            //para evitar error de loop en JSON y validacion standar de modelo invalido ya que vamos a personalizar este error
            //por medio de un filtro
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                )
                .ConfigureApiBehaviorOptions(options => {
                    options.SuppressModelStateInvalidFilter = true;
                });

            services.AddControllers(options => 
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:4300", "https://localhost:4300","https://localhost:4100","http://localhost:4100","http://localhost:4200", "https://localhost:4200", "http://localhost", "https://localhost", "http://192.168.10.190", "https://192.168.10.190", "http://192.168.10.190:4200", "https://192.168.10.190:4200", "http://192.168.10.190:4100", "https://192.168.10.190:4100", "http://192.168.1.17:4200", "https://192.168.1.17:4200", "http://192.168.1.17:4300", "https://192.168.1.17:4300")                   
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // autenticaciones con jwt, esta configuracion siempre tiene que ir antes de AddMvc
            // las configuraciones estan en appsegging y accedemos a ellas con configuraion
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:Secretkey"]))

                };
            });         

            //se agrega al API los filtros de validaciones personalizados de manera Global y por ultimo se agregan 
            // los validadores de los DTos por medio del paquete Fluent Validations
            services.AddMvc(options => {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options => {
                options.RegisterValidatorsFromAssemblies(System.AppDomain.CurrentDomain.GetAssemblies());
            });

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // TODO ,  en produccion unicamente aceptar un origen, esta confirguracion es para desarrollo
            app.UseCors("CorsPolicy");

            // app.UseCors("AguilaApp");

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../swagger/v1/swagger.json", "Aguila API V1");
                // Solo funciona desplegando en la raiz del sitio, oh iis expres
                //options.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

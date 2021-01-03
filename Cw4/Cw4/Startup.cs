
using Cw4.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cw4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        //wstrzykiwanie klas przydatnych w wielu miejscach w kodzie(np. logowanie, komunikacja z bd)
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDbService, MssqlDBService>();
            services.AddControllers();//zarejestrowanie kontroler�w z widokami i stronami
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //definiuje tzw. middlewary
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            //dodaje now� instancj� klasy EndpointRoutingMiddleware(rutowanie zada� u�ytkownik�w na poststawie adresu url, metody http)
            app.UseRouting();


            //m�j middleware: doklejanie do odpowiedzi  nag�owek http
            app.Use(async (context, c) =>
            {
                context.Response.Headers.Add("Secret", "1234");
                await c.Invoke();//przepuszczenie rz�dania do kolejnego middleware w kolejce
            });


            app.UseMiddleware<CustomMiddleware>();


            //dodaje middleware, kt�ry sprawdza czy kto� ma dost�p do czego�
            app.UseAuthorization();


            //definiuje endpointy
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

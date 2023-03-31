using Autofac;
using Autofac.Extensions.DependencyInjection;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Configure for Autofac 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllersWithViews();

//builder.Services.Add(new ServiceDescriptor(
//    typeof(ICitiesService),
//    typeof(CitiesService),
//    ServiceLifetime.Scoped
//));

//builder.Services.AddTransient<ICitiesService, CitiesService>(); 
//builder.Services.AddScoped<ICitiesService, CitiesService>();
//builder.Services.AddSingleton<ICitiesService, CitiesService>();


// Using Autofac
builder.Host.ConfigureContainer<ContainerBuilder>(
containerBuilder =>
{
    //containerBuilder.RegisterType<CitiesService>()
    //                .As<ICitiesService>()
    //                .InstancePerDependency(); // addTransient()

    containerBuilder.RegisterType<CitiesService>()
                    .As<ICitiesService>()
                    .InstancePerLifetimeScope(); // addScoped()
    
    //containerBuilder.RegisterType<CitiesService>()
    //                .As<ICitiesService>()
    //                .SingleInstance(); // addSingleton()

});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
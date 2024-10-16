using Microsoft.Extensions.Logging;
using ChangoMasApp.Services;
using ChangoMasApp.ViewModels;
namespace ChangoMasApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ILoginService, LoginService>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<ProductosService>();
            builder.Services.AddTransient<IProductosService, ProductosService>();
            builder.Services.AddSingleton<MainPage>(); // Agregar MainPage como singleton
#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();
        }
    }
}

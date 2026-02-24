using BoxingRoundApp.ViewModel;
using BoxingRoundApp.Views;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using BoxingRoundApp.Services.Data;

namespace BoxingRoundApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            // Pages
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<CreateWorkoutProfilePage>();
            builder.Services.AddTransient<ActivateWorkoutProfilePage>();

            // ViewModels
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<CreateWorkoutProfileViewModel>();
            builder.Services.AddTransient<ActivateWorkoutProfileViewModel>();

            // Services
            builder.Services.AddSingleton<BoxingDatabase>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#elif IOS || MACCATALYST
    handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });

            return builder.Build();
        }
    }
}

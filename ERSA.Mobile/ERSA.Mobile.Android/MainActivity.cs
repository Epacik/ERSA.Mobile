using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Serilog;

namespace ERSA.Mobile.Droid
{
    [Activity(Label = "ERSA Mobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private App app;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .WriteTo.AndroidLog()
                .CreateLogger();

            base.OnCreate(savedInstanceState);

            Xamarin.Forms.Device.SetFlags(new string[] { "AppTheme_Experimental" });
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            app = new App();
            LoadApplication(app);

            app.AddHardwareBackButtonFunc(new Func<System.Threading.Tasks.Task>(async () =>
            {
                await System.Threading.Tasks.Task.Yield();
                base.OnBackPressed();
            }));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            app.OnHardwareBackButtonPressed();
        }
    }
}
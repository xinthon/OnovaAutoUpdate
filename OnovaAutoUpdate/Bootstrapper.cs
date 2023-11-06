using OnovaAutoUpdate.Services;
using OnovaAutoUpdate.ViewModels;
using OnovaAutoUpdate.ViewModels.Framework;
using Stylet;
using StyletIoC;
using System.Net;

namespace OnovaAutoUpdate
{
    public class Bootstrapper : Bootstrapper<RootViewModel>
    {
        protected override void OnStart()
        {
            base.OnStart();
            ServicePointManager.DefaultConnectionLimit = 20;
        }

        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            base.ConfigureIoC(builder);

            builder.Bind<SettingsService>().ToSelf().InSingletonScope();
            builder.Bind<IViewModelFactory>().ToAbstractFactory();
        }

    }
}

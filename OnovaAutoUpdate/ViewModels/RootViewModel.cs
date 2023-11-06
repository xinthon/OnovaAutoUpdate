using MaterialDesignThemes.Wpf;
using OnovaAutoUpdate.Services;
using OnovaAutoUpdate.ViewModels.Components;
using OnovaAutoUpdate.ViewModels.Framework;
using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnovaAutoUpdate.ViewModels
{
    public class RootViewModel : Screen
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly SettingsService _settingsService;
        private readonly UpdateService _updateService;

        public SnackbarMessageQueue Notifications { get; } = new(TimeSpan.FromSeconds(5));

        public RootViewModel(
            IViewModelFactory viewModelFactory,
            SettingsService settingsService,
            UpdateService updateService)
        {
            _viewModelFactory = viewModelFactory;
            _settingsService = settingsService;
            _updateService = updateService;

            Dashboard = _viewModelFactory.CreateDashboardViewModel();

            DisplayName = App.Name;
        }

        public DashboardViewModel Dashboard { get; }

        public async void OnViewFullyLoaded() { await CheckForUpdatesAsync(); }

        private async Task CheckForUpdatesAsync()
        {
            try
            {
                var updateVersion = await _updateService.CheckForUpdatesAsync();
                if(updateVersion is null)
                    return;

                Notifications.Enqueue($"Downloading update to {App.Name} v{updateVersion}...");
                await _updateService.PrepareUpdateAsync(updateVersion);

                Notifications.Enqueue(
                    "Update has been downloaded and will be installed when you exit",
                    "INSTALL NOW",
                    () =>
                    {
                        _updateService.FinalizeUpdate(true);
                        RequestClose();
                    });
            } catch (Exception ex)
            {
                // Failure to update shouldn't crash the application
                Notifications.Enqueue("Failed to perform application update");
            }
        }
    }
}

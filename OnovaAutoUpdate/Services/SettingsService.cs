using Cogwheel;
using PropertyChanged;
using System;
using System.ComponentModel;
using System.IO;

namespace OnovaAutoUpdate.Services
{
    [AddINotifyPropertyChangedInterface]
    public partial class SettingsService : SettingsBase, INotifyPropertyChanged
    {
        public SettingsService(string filePath) : base(filePath)
        {
        }
        public SettingsService() : base(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.dat"))
        {
        }

        public bool IsAutoUpdateEnabled { get; set; } = true;
        public Version? LastAppVersion { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

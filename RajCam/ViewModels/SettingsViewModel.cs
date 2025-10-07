using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RajCam.Models;
using RajCam.Helpers;
using System.Threading.Tasks;

namespace RajCam.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        private CameraSettings settings;

        public SettingsViewModel()
        {
            Settings = new CameraSettings();
            LoadSettings();
        }

        [RelayCommand]
        private async Task SaveSettings()
        {
            await SettingsHelper.SaveSettingsAsync(Settings);
        }

        [RelayCommand]
        private async Task LoadSettings()
        {
            Settings = await SettingsHelper.LoadSettingsAsync();
        }

        [RelayCommand]
        private void ResetSettings()
        {
            Settings = new CameraSettings();
        }
    }
}
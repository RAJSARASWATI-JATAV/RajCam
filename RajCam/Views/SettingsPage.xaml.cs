using Microsoft.UI.Xaml.Controls;
using RajCam.Models;
using RajCam.Helpers;
using System.Threading.Tasks;

namespace RajCam.Views
{
    public sealed partial class SettingsPage : Page
    {
        private CameraSettings _settings;

        public SettingsPage()
        {
            this.InitializeComponent();
            _settings = new CameraSettings();
            LoadSettings();
        }

        private void LoadSettings()
        {
            BrightnessSlider.Value = _settings.Brightness;
            ContrastSlider.Value = _settings.Contrast;
            HdrToggle.IsOn = _settings.HdrEnabled;
            GridToggle.IsOn = _settings.GridEnabled;
        }

        private void SaveSettings()
        {
            _settings.Brightness = (int)BrightnessSlider.Value;
            _settings.Contrast = (int)ContrastSlider.Value;
            _settings.HdrEnabled = HdrToggle.IsOn;
            _settings.GridEnabled = GridToggle.IsOn;
        }
    }
}
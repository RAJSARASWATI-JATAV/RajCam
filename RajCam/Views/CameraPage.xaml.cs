using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using RajCam.Services;
using System;
using System.Threading.Tasks;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using Windows.Storage;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using Windows.Storage;

namespace RajCam.Views
{
    public sealed partial class CameraPage : Page
    {
        private readonly CameraService _cameraService;
        private readonly HdrService _hdrService;
        private readonly NightModeService _nightModeService;
        private readonly BurstModeService _burstModeService;
        private readonly FilterService _filterService;
        private bool _isRecording = false;
        private bool _gridEnabled = false;
        private bool _flashEnabled = false;
        private bool _hdrEnabled = false;
        private bool _nightModeEnabled = false;
        private int _timerSeconds = 0;
        private int _photoCount = 0;
        private int _videoCount = 0;
        private string _currentFilter = "None";

        public CameraPage()
        {
            this.InitializeComponent();
            _cameraService = new CameraService();
            _hdrService = new HdrService();
            _nightModeService = new NightModeService();
            _burstModeService = new BurstModeService();
            _filterService = new FilterService();
            InitializeCamera();
        }

        private async void InitializeCamera()
        {
            try
            {
                await _cameraService.InitializeAsync();
                PreviewControl.Source = _cameraService.GetMediaCapture();
                await _cameraService.GetMediaCapture().StartPreviewAsync();
            }
            catch (Exception ex)
            {
                await ShowDialog("Camera Error", $"Failed to initialize camera: {ex.Message}");
            }
        }

        private async void CaptureButton_Click(object sender, RoutedEventArgs e)
        {
            if (_timerSeconds > 0)
            {
                await StartTimerCapture();
            }
            else
            {
                await CapturePhoto();
            }
        }

        private async Task StartTimerCapture()
        {
            TimerCountdown.Visibility = Visibility.Visible;
            
            for (int i = _timerSeconds; i > 0; i--)
            {
                TimerCountdown.Text = i.ToString();
                await Task.Delay(1000);
            }
            
            TimerCountdown.Visibility = Visibility.Collapsed;
            await CapturePhoto();
        }

        private async Task CapturePhoto()
        {
            try
            {
                await ShowFlashEffect();
                
                StorageFile file = null;
                
                if (_hdrEnabled)
                {
                    file = await _hdrService.CaptureHdrPhotoAsync(_cameraService.GetMediaCapture());
                    ModeIndicator.Text = "HDR";
                }
                else if (_nightModeEnabled)
                {
                    file = await _nightModeService.CaptureNightPhotoAsync(_cameraService.GetMediaCapture());
                    ModeIndicator.Text = "NIGHT";
                }
                else
                {
                    file = await _cameraService.CapturePhotoAsync();
                    ModeIndicator.Text = "PHOTO";
                }
                
                if (file != null)
                {
                    _photoCount++;
                    UpdateMainWindowStats();
                    await ShowDialog("Success", "Photo captured successfully!");
                }
            }
            catch (Exception ex)
            {
                await ShowDialog("Error", $"Failed to capture photo: {ex.Message}");
            }
        }

        private async void RecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isRecording)
            {
                await StartRecording();
            }
            else
            {
                await StopRecording();
            }
        }

        private async Task StartRecording()
        {
            try
            {
                var file = await _cameraService.StartVideoRecordingAsync();
                if (file != null)
                {
                    _isRecording = true;
                    RecordButton.Content = "⏹";
                    RecordButton.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Red);
                    UpdateMainWindowRecording(true);
                }
            }
            catch (Exception ex)
            {
                await ShowDialog("Error", $"Failed to start recording: {ex.Message}");
            }
        }

        private async Task StopRecording()
        {
            try
            {
                await _cameraService.StopVideoRecordingAsync();
                _isRecording = false;
                RecordButton.Content = "⏺";
                RecordButton.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.ColorHelper.FromArgb(255, 255, 68, 68));
                _videoCount++;
                UpdateMainWindowStats();
                UpdateMainWindowRecording(false);
                await ShowDialog("Success", "Video saved successfully!");
            }
            catch (Exception ex)
            {
                await ShowDialog("Error", $"Failed to stop recording: {ex.Message}");
            }
        }

        private void GridButton_Click(object sender, RoutedEventArgs e)
        {
            _gridEnabled = !_gridEnabled;
            GridOverlay.Visibility = _gridEnabled ? Visibility.Visible : Visibility.Collapsed;
            GridButton.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                _gridEnabled ? Microsoft.UI.ColorHelper.FromArgb(255, 0, 212, 255) : Microsoft.UI.ColorHelper.FromArgb(255, 51, 51, 51));
        }

        private void FlashButton_Click(object sender, RoutedEventArgs e)
        {
            _flashEnabled = !_flashEnabled;
            FlashButton.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                _flashEnabled ? Microsoft.UI.ColorHelper.FromArgb(255, 255, 215, 0) : Microsoft.UI.ColorHelper.FromArgb(255, 51, 51, 51));
        }

        private void TimerButton_Click(object sender, RoutedEventArgs e)
        {
            _timerSeconds = _timerSeconds == 0 ? 3 : (_timerSeconds == 3 ? 10 : 0);
            TimerButton.Content = _timerSeconds == 0 ? "⏱️" : _timerSeconds.ToString();
            TimerButton.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                _timerSeconds > 0 ? Microsoft.UI.ColorHelper.FromArgb(255, 0, 212, 255) : Microsoft.UI.ColorHelper.FromArgb(255, 51, 51, 51));
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            // Zoom functionality
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            // Zoom functionality
        }

        private void GalleryButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GalleryPage));
        }

        private void HdrButton_Click(object sender, RoutedEventArgs e)
        {
            _hdrEnabled = !_hdrEnabled;
            HdrButton.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                _hdrEnabled ? Microsoft.UI.ColorHelper.FromArgb(255, 0, 212, 255) : Microsoft.UI.ColorHelper.FromArgb(255, 51, 51, 51));
            ModeIndicator.Text = _hdrEnabled ? "HDR" : "";
        }

        private void NightButton_Click(object sender, RoutedEventArgs e)
        {
            _nightModeEnabled = !_nightModeEnabled;
            NightButton.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                _nightModeEnabled ? Microsoft.UI.ColorHelper.FromArgb(255, 0, 212, 255) : Microsoft.UI.ColorHelper.FromArgb(255, 51, 51, 51));
            ModeIndicator.Text = _nightModeEnabled ? "NIGHT" : "";
        }

        private async void BurstButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await ShowFlashEffect();
                var files = await _burstModeService.CaptureBurstPhotosAsync(_cameraService.GetMediaCapture());
                _photoCount += files.Count;
                UpdateMainWindowStats();
                await ShowDialog("Success", $"Captured {files.Count} burst photos!");
            }
            catch (Exception ex)
            {
                await ShowDialog("Error", $"Burst capture failed: {ex.Message}");
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterPanel.Visibility = FilterPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string filter)
            {
                _currentFilter = filter;
                ModeIndicator.Text = filter == "None" ? "" : filter.ToUpper();
            }
        }

        private void CloseFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterPanel.Visibility = Visibility.Collapsed;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        private async Task ShowFlashEffect()
        {
            if (_flashEnabled)
            {
                FlashEffect.Visibility = Visibility.Visible;
                var animation = new DoubleAnimation
                {
                    From = 0.8,
                    To = 0,
                    Duration = TimeSpan.FromMilliseconds(200)
                };
                
                var storyboard = new Storyboard();
                storyboard.Children.Add(animation);
                Storyboard.SetTarget(animation, FlashEffect);
                Storyboard.SetTargetProperty(animation, "Opacity");
                
                storyboard.Begin();
                await Task.Delay(200);
                FlashEffect.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateMainWindowStats()
        {
            if (Frame.Parent is Grid parentGrid && parentGrid.Parent is MainWindow mainWindow)
            {
                mainWindow.UpdateStats(_photoCount, _videoCount);
            }
        }

        private void UpdateMainWindowRecording(bool isRecording)
        {
            if (Frame.Parent is Grid parentGrid && parentGrid.Parent is MainWindow mainWindow)
            {
                mainWindow.SetRecordingIndicator(isRecording);
            }
        }

        private async Task ShowDialog(string title, string message)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };
            await dialog.ShowAsync();
        }
    }
}
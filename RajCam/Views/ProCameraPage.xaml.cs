using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI.Xaml.Media;
using RajCam.Services;
using System.Threading.Tasks;
using System;
using Microsoft.UI;
using Windows.Storage;

namespace RajCam.Views
{
    public sealed partial class ProCameraPage : Page
    {
        private readonly CameraService _cameraService;
        private readonly FaceDetectionService _faceDetectionService;
        private readonly AdvancedImageService _advancedImageService;
        private readonly ProVideoService _proVideoService;
        private readonly LiveStreamService _liveStreamService;
        private readonly ProCameraControlService _proCameraControlService;
        private bool _isRecording = false;

        public ProCameraPage()
        {
            this.InitializeComponent();
            _cameraService = new CameraService();
            _faceDetectionService = new FaceDetectionService();
            _advancedImageService = new AdvancedImageService();
            _proVideoService = new ProVideoService();
            _liveStreamService = new LiveStreamService();
            _proCameraControlService = new ProCameraControlService();
            InitializeProCamera();
        }

        private async void InitializeProCamera()
        {
            await _cameraService.InitializeAsync();
            await _faceDetectionService.InitializeAsync();
            
            ProPreviewControl.Source = _cameraService.GetMediaCapture();
            await _cameraService.GetMediaCapture().StartPreviewAsync();
            
            _proCameraControlService.Initialize(_cameraService.GetMediaCapture());
        }

        private async void ExposureSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            await _proCameraControlService.SetManualExposureAsync(e.NewValue);
        }

        private async void FocusSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            await _proCameraControlService.SetManualFocusAsync(e.NewValue);
        }

        private async void ISOSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            await _proCameraControlService.SetISOAsync((uint)e.NewValue);
        }

        private async void WhiteBalanceSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            await _proCameraControlService.SetWhiteBalanceAsync((uint)e.NewValue);
        }

        private async void FaceDetectionToggle_Toggled(object sender, RoutedEventArgs e)
        {
            if (FaceDetectionToggle.IsOn)
            {
                // Start face detection
                await DetectFaces();
            }
            else
            {
                FaceOverlay.Children.Clear();
            }
        }

        private async Task DetectFaces()
        {
            try
            {
                var tempFile = await _cameraService.CapturePhotoAsync();
                var faces = await _faceDetectionService.DetectFacesAsync(tempFile);
                
                FaceOverlay.Children.Clear();
                foreach (var face in faces)
                {
                    var rect = new Rectangle
                    {
                        Width = face.FaceBox.Width,
                        Height = face.FaceBox.Height,
                        Stroke = new SolidColorBrush(Microsoft.UI.Colors.Red),
                        StrokeThickness = 2
                    };
                    
                    Canvas.SetLeft(rect, face.FaceBox.X);
                    Canvas.SetTop(rect, face.FaceBox.Y);
                    FaceOverlay.Children.Add(rect);
                }
            }
            catch { }
        }

        private async void ProPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var file = await _cameraService.CapturePhotoAsync();
                
                if (BeautifyToggle.IsOn)
                    file = await _advancedImageService.ApplyBeautifyAsync(file);
                
                if (PortraitModeToggle.IsOn)
                    file = await _advancedImageService.ApplyPortraitModeAsync(file);
                
                await ShowDialog("Success", "Professional photo captured!");
            }
            catch (Exception ex)
            {
                await ShowDialog("Error", ex.Message);
            }
        }

        private async void ProVideo_Click(object sender, RoutedEventArgs e)
        {
            if (!_isRecording)
            {
                await _proVideoService.Start4KRecordingAsync(_cameraService.GetMediaCapture());
                ProVideoButton.Content = "⏹ STOP";
                _isRecording = true;
            }
            else
            {
                await _cameraService.StopVideoRecordingAsync();
                ProVideoButton.Content = "🎥 PRO";
                _isRecording = false;
            }
        }

        private async void Start4K_Click(object sender, RoutedEventArgs e)
        {
            await _proVideoService.Start4KRecordingAsync(_cameraService.GetMediaCapture());
            await ShowDialog("4K Recording", "4K video recording started!");
        }

        private async void StartSlowMotion_Click(object sender, RoutedEventArgs e)
        {
            await _proVideoService.StartSlowMotionAsync(_cameraService.GetMediaCapture());
            await ShowDialog("Slow Motion", "240fps slow motion recording started!");
        }

        private async void StartTimelapse_Click(object sender, RoutedEventArgs e)
        {
            await _proVideoService.StartTimelapseAsync(_cameraService.GetMediaCapture());
            await ShowDialog("Timelapse", "Timelapse recording started!");
        }

        private async void StartStream_Click(object sender, RoutedEventArgs e)
        {
            if (!_liveStreamService.IsStreaming)
            {
                await _liveStreamService.StartStreamAsync(_cameraService.GetMediaCapture(), "rtmp://stream.url");
                StreamStatus.Text = "🔴 LIVE";
            }
            else
            {
                await _liveStreamService.StopStreamAsync();
                StreamStatus.Text = "";
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
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RajCam.Views;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;

namespace RajCam
{
    public sealed partial class MainWindow : Window
    {
        private int photoCount = 0;
        private int videoCount = 0;

        public MainWindow()
        {
            this.InitializeComponent();
            this.SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.BaseAlt };
            this.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            this.AppWindow.TitleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;
            
            // Navigate to Camera page by default
            ContentFrame.Navigate(typeof(CameraPage));
            UpdateNavigation("Camera");
        }

        private void NavigateToCamera_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(CameraPage));
            UpdateNavigation("Camera");
        }

        private void NavigateToGallery_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(GalleryPage));
            UpdateNavigation("Gallery");
        }

        private void NavigateToSettings_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(SettingsPage));
            UpdateNavigation("Settings");
        }

        private void NavigateToProCamera_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ProCameraPage));
            UpdateNavigation("ProCamera");
        }

        private void UpdateNavigation(string currentPage)
        {
            // Reset all buttons
            var grayBrush = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.ColorHelper.FromArgb(255, 102, 102, 102));
            var blueBrush = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.ColorHelper.FromArgb(255, 0, 212, 255));
            
            CameraNavButton.Background = grayBrush;
            ProCameraNavButton.Background = grayBrush;
            GalleryNavButton.Background = grayBrush;
            SettingsNavButton.Background = grayBrush;

            // Highlight current page
            switch (currentPage)
            {
                case "Camera":
                    CameraNavButton.Background = blueBrush;
                    break;
                case "ProCamera":
                    ProCameraNavButton.Background = blueBrush;
                    break;
                case "Gallery":
                    GalleryNavButton.Background = blueBrush;
                    break;
                case "Settings":
                    SettingsNavButton.Background = blueBrush;
                    break;
            }
        }

        public void UpdateStats(int photos, int videos)
        {
            photoCount = photos;
            videoCount = videos;
            StatsLabel.Text = $"📸 {photoCount}  🎥 {videoCount}";
        }

        public void SetRecordingIndicator(bool isRecording)
        {
            RecIndicator.Text = isRecording ? "● REC" : "";
        }
    }
}
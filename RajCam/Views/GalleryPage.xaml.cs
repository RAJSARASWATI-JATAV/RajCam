using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using RajCam.Services;
using RajCam.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Windows.Storage;

namespace RajCam.Views
{
    public sealed partial class GalleryPage : Page
    {
        private readonly StorageService _storageService;
        private readonly ExportService _exportService;
        private CaptureItem _selectedItem;

        public GalleryPage()
        {
            this.InitializeComponent();
            _storageService = new StorageService();
            _exportService = new ExportService();
            LoadGallery();
        }

        private async void LoadGallery()
        {
            var items = await _storageService.GetCapturedItemsAsync();
            GalleryGrid.ItemsSource = items;
        }

        private void GalleryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedItem = GalleryGrid.SelectedItem as CaptureItem;
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is CaptureItem item)
            {
                _selectedItem = item;
                ExportPanel.Visibility = Visibility.Visible;
            }
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is CaptureItem item)
            {
                var success = await _storageService.DeleteItemAsync(item);
                if (success)
                {
                    LoadGallery();
                    await ShowDialog("Success", "Item deleted successfully!");
                }
            }
        }

        private async void ExportToPictures_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem != null)
            {
                var success = await _exportService.ExportToGalleryAsync(_selectedItem.File);
                if (success)
                    await ShowDialog("Success", "Exported to Pictures library!");
                else
                    await ShowDialog("Error", "Export failed!");
            }
            ExportPanel.Visibility = Visibility.Collapsed;
        }

        private async void ExportToCustom_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem != null)
            {
                var success = await _exportService.ExportToCustomLocationAsync(_selectedItem.File);
                if (success)
                    await ShowDialog("Success", "Exported successfully!");
            }
            ExportPanel.Visibility = Visibility.Collapsed;
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            // Share functionality
            ExportPanel.Visibility = Visibility.Collapsed;
        }

        private void CloseExport_Click(object sender, RoutedEventArgs e)
        {
            ExportPanel.Visibility = Visibility.Collapsed;
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
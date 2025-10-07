using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RajCam.Services;
using System.Threading.Tasks;

namespace RajCam.ViewModels
{
    public partial class CameraViewModel : ObservableObject
    {
        private readonly CameraService _cameraService;

        [ObservableProperty]
        private bool isRecording;

        [ObservableProperty]
        private bool gridEnabled;

        [ObservableProperty]
        private int photoCount;

        [ObservableProperty]
        private int videoCount;

        public CameraViewModel()
        {
            _cameraService = new CameraService();
        }

        [RelayCommand]
        private async Task CapturePhoto()
        {
            var file = await _cameraService.CapturePhotoAsync();
            if (file != null) PhotoCount++;
        }

        [RelayCommand]
        private async Task ToggleRecording()
        {
            if (!IsRecording)
            {
                await _cameraService.StartVideoRecordingAsync();
                IsRecording = true;
            }
            else
            {
                await _cameraService.StopVideoRecordingAsync();
                IsRecording = false;
                VideoCount++;
            }
        }
    }
}
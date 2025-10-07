using System;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;

namespace RajCam.Services
{
    public class CameraService
    {
        private MediaCapture _mediaCapture;
        private bool _isInitialized = false;

        public async Task<bool> InitializeAsync()
        {
            try
            {
                _mediaCapture = new MediaCapture();
                await _mediaCapture.InitializeAsync();
                _isInitialized = true;
                return true;
            }
            catch
            {
                _isInitialized = false;
                return false;
            }
        }

        public MediaCapture GetMediaCapture() => _mediaCapture;

        public async Task<StorageFile> CapturePhotoAsync()
        {
            if (!_isInitialized) return null;

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                $"RAJ_CAM_Photo_{DateTime.Now:yyyyMMdd_HHmmss}.jpg",
                CreationCollisionOption.GenerateUniqueName);

            await _mediaCapture.CapturePhotoToStorageFileAsync(
                ImageEncodingProperties.CreateJpeg(), file);

            return file;
        }

        public async Task<StorageFile> StartVideoRecordingAsync()
        {
            if (!_isInitialized) return null;

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                $"RAJ_CAM_Video_{DateTime.Now:yyyyMMdd_HHmmss}.mp4",
                CreationCollisionOption.GenerateUniqueName);

            await _mediaCapture.StartRecordToStorageFileAsync(
                MediaEncodingProfile.CreateMp4(VideoEncodingQuality.HD1080p), file);

            return file;
        }

        public async Task StopVideoRecordingAsync()
        {
            if (_isInitialized)
                await _mediaCapture.StopRecordAsync();
        }

        public void Dispose()
        {
            _mediaCapture?.Dispose();
            _isInitialized = false;
        }
    }
}
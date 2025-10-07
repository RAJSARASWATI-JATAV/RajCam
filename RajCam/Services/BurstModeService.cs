using Windows.Storage;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RajCam.Services
{
    public class BurstModeService
    {
        public async Task<List<StorageFile>> CaptureBurstPhotosAsync(MediaCapture mediaCapture, int count = 5)
        {
            var files = new List<StorageFile>();
            
            for (int i = 0; i < count; i++)
            {
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                    $"Burst_{i + 1}_{System.DateTime.Now:yyyyMMdd_HHmmss}.jpg",
                    CreationCollisionOption.GenerateUniqueName);

                await mediaCapture.CapturePhotoToStorageFileAsync(
                    ImageEncodingProperties.CreateJpeg(), file);
                
                files.Add(file);
                await Task.Delay(200); // 200ms between shots
            }
            
            return files;
        }
    }
}
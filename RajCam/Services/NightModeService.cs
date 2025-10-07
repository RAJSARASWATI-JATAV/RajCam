using Windows.Storage;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using System.Threading.Tasks;

namespace RajCam.Services
{
    public class NightModeService
    {
        public async Task<StorageFile> CaptureNightPhotoAsync(MediaCapture mediaCapture)
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                $"Night_Photo_{System.DateTime.Now:yyyyMMdd_HHmmss}.jpg",
                CreationCollisionOption.GenerateUniqueName);

            var properties = ImageEncodingProperties.CreateJpeg();
            await mediaCapture.CapturePhotoToStorageFileAsync(properties, file);
            return file;
        }
    }
}
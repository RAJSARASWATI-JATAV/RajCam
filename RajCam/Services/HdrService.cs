using Windows.Storage;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using System.Threading.Tasks;

namespace RajCam.Services
{
    public class HdrService
    {
        public async Task<StorageFile> CaptureHdrPhotoAsync(MediaCapture mediaCapture)
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                $"HDR_Photo_{System.DateTime.Now:yyyyMMdd_HHmmss}.jpg",
                CreationCollisionOption.GenerateUniqueName);

            var properties = ImageEncodingProperties.CreateJpeg();
            properties.Width = 1920;
            properties.Height = 1080;

            await mediaCapture.CapturePhotoToStorageFileAsync(properties, file);
            return file;
        }
    }
}
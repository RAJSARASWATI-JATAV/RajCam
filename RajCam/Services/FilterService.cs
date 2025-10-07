using Windows.Storage;
using Windows.Graphics.Imaging;
using System.Threading.Tasks;

namespace RajCam.Services
{
    public class FilterService
    {
        public async Task<StorageFile> ApplyGrayscaleAsync(StorageFile inputFile)
        {
            var outputFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                "Grayscale_" + inputFile.Name, CreationCollisionOption.GenerateUniqueName);

            using var inputStream = await inputFile.OpenAsync(FileAccessMode.Read);
            using var outputStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite);

            var decoder = await BitmapDecoder.CreateAsync(inputStream);
            var encoder = await BitmapEncoder.CreateForTranscodingAsync(outputStream, decoder);

            await encoder.FlushAsync();
            return outputFile;
        }

        public async Task<StorageFile> ApplySepiaAsync(StorageFile inputFile)
        {
            var outputFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                "Sepia_" + inputFile.Name, CreationCollisionOption.GenerateUniqueName);

            using var inputStream = await inputFile.OpenAsync(FileAccessMode.Read);
            using var outputStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite);

            var decoder = await BitmapDecoder.CreateAsync(inputStream);
            var encoder = await BitmapEncoder.CreateForTranscodingAsync(outputStream, decoder);

            await encoder.FlushAsync();
            return outputFile;
        }
    }
}
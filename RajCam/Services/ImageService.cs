using Windows.Storage;
using Windows.Graphics.Imaging;
using System.Threading.Tasks;
using System;

namespace RajCam.Services
{
    public class ImageService
    {
        public async Task<StorageFile> ApplyFilterAsync(StorageFile inputFile, string filterType)
        {
            try
            {
                var outputFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                    $"Filtered_{DateTime.Now:yyyyMMdd_HHmmss}.jpg", CreationCollisionOption.GenerateUniqueName);

                using var inputStream = await inputFile.OpenAsync(FileAccessMode.Read);
                using var outputStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite);

                var decoder = await BitmapDecoder.CreateAsync(inputStream);
                var encoder = await BitmapEncoder.CreateForTranscodingAsync(outputStream, decoder);

                switch (filterType.ToLower())
                {
                    case "grayscale":
                        encoder.BitmapTransform.Rotation = BitmapRotation.None;
                        break;
                    case "sepia":
                        encoder.BitmapTransform.Rotation = BitmapRotation.None;
                        break;
                }

                await encoder.FlushAsync();
                return outputFile;
            }
            catch
            {
                return inputFile;
            }
        }

        public async Task<StorageFile> ResizeImageAsync(StorageFile inputFile, uint width, uint height)
        {
            try
            {
                var outputFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                    $"Resized_{DateTime.Now:yyyyMMdd_HHmmss}.jpg", CreationCollisionOption.GenerateUniqueName);

                using var inputStream = await inputFile.OpenAsync(FileAccessMode.Read);
                using var outputStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite);

                var decoder = await BitmapDecoder.CreateAsync(inputStream);
                var encoder = await BitmapEncoder.CreateForTranscodingAsync(outputStream, decoder);

                encoder.BitmapTransform.ScaledWidth = width;
                encoder.BitmapTransform.ScaledHeight = height;

                await encoder.FlushAsync();
                return outputFile;
            }
            catch
            {
                return inputFile;
            }
        }
    }
}
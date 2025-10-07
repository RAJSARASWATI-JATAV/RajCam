using Windows.Storage;
using Windows.Storage.Pickers;
using System.Threading.Tasks;
using System;

namespace RajCam.Services
{
    public class ExportService
    {
        public async Task<bool> ExportToGalleryAsync(StorageFile file)
        {
            try
            {
                var picturesLibrary = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Pictures);
                var saveFolder = picturesLibrary.SaveFolder;
                
                await file.CopyAsync(saveFolder, file.Name, NameCollisionOption.GenerateUniqueName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ExportToCustomLocationAsync(StorageFile file)
        {
            try
            {
                var saveFolder = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Pictures);
                await file.CopyAsync(saveFolder.SaveFolder, file.Name, NameCollisionOption.GenerateUniqueName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
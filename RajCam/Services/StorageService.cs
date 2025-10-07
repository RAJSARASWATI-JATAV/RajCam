using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using RajCam.Models;

namespace RajCam.Services
{
    public class StorageService
    {
        public async Task<List<CaptureItem>> GetCapturedItemsAsync()
        {
            var items = new List<CaptureItem>();
            var folder = ApplicationData.Current.LocalFolder;

            var files = await folder.GetFilesAsync();
            foreach (var file in files)
            {
                if (file.Name.StartsWith("RAJ_CAM_"))
                {
                    var properties = await file.GetBasicPropertiesAsync();
                    items.Add(new CaptureItem
                    {
                        Name = file.Name,
                        Path = file.Path,
                        DateCreated = properties.DateModified.DateTime,
                        Type = file.Name.Contains("Photo") ? CaptureType.Photo : CaptureType.Video,
                        Size = (long)properties.Size,
                        File = file
                    });
                }
            }

            return items;
        }

        public async Task<bool> DeleteItemAsync(CaptureItem item)
        {
            try
            {
                await item.File.DeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<StorageFile> SaveToGalleryAsync(StorageFile file)
        {
            var picturesFolder = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Pictures);
            var saveFolder = picturesFolder.SaveFolder;
            
            return await file.CopyAsync(saveFolder, file.Name, NameCollisionOption.GenerateUniqueName);
        }
    }
}
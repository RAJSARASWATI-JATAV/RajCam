using Windows.Storage;
using System.Text.Json;
using RajCam.Models;
using System.Threading.Tasks;
using System;

namespace RajCam.Helpers
{
    public static class SettingsHelper
    {
        private const string SettingsFileName = "settings.json";

        public static async Task SaveSettingsAsync(CameraSettings settings)
        {
            var json = JsonSerializer.Serialize(settings);
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(SettingsFileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, json);
        }

        public static async Task<CameraSettings> LoadSettingsAsync()
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(SettingsFileName);
                var json = await FileIO.ReadTextAsync(file);
                return JsonSerializer.Deserialize<CameraSettings>(json);
            }
            catch
            {
                return new CameraSettings();
            }
        }
    }
}
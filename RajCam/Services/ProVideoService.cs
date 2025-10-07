using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using System.Threading.Tasks;
using System;

namespace RajCam.Services
{
    public class ProVideoService
    {
        public async Task<StorageFile> Start4KRecordingAsync(MediaCapture mediaCapture)
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                $"4K_Video_{DateTime.Now:yyyyMMdd_HHmmss}.mp4", CreationCollisionOption.GenerateUniqueName);

            var profile = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Uhd2160p);
            profile.Video.Bitrate = 100000000; // 100 Mbps for 4K
            profile.Video.FrameRate.Numerator = 60;
            profile.Video.FrameRate.Denominator = 1;

            await mediaCapture.StartRecordToStorageFileAsync(profile, file);
            return file;
        }

        public async Task<StorageFile> StartSlowMotionAsync(MediaCapture mediaCapture)
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                $"SlowMotion_{DateTime.Now:yyyyMMdd_HHmmss}.mp4", CreationCollisionOption.GenerateUniqueName);

            var profile = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.HD1080p);
            profile.Video.FrameRate.Numerator = 240; // 240fps for slow motion
            profile.Video.FrameRate.Denominator = 1;

            await mediaCapture.StartRecordToStorageFileAsync(profile, file);
            return file;
        }

        public async Task<StorageFile> StartTimelapseAsync(MediaCapture mediaCapture)
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                $"Timelapse_{DateTime.Now:yyyyMMdd_HHmmss}.mp4", CreationCollisionOption.GenerateUniqueName);

            var profile = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.HD1080p);
            await mediaCapture.StartRecordToStorageFileAsync(profile, file);
            return file;
        }
    }
}
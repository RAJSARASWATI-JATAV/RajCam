using System;
using Windows.Storage;

namespace RajCam.Models
{
    public class CaptureItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime DateCreated { get; set; }
        public CaptureType Type { get; set; }
        public long Size { get; set; }
        public StorageFile File { get; set; }
    }

    public enum CaptureType
    {
        Photo,
        Video
    }
}
using Windows.Media.FaceAnalysis;
using Windows.Graphics.Imaging;
using Windows.Storage;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RajCam.Services
{
    public class FaceDetectionService
    {
        private FaceDetector _faceDetector;

        public async Task<bool> InitializeAsync()
        {
            _faceDetector = await FaceDetector.CreateAsync();
            return _faceDetector != null;
        }

        public async Task<IList<DetectedFace>> DetectFacesAsync(StorageFile imageFile)
        {
            using var stream = await imageFile.OpenAsync(FileAccessMode.Read);
            var decoder = await BitmapDecoder.CreateAsync(stream);
            var bitmap = await decoder.GetSoftwareBitmapAsync();
            
            return await _faceDetector.DetectFacesAsync(bitmap);
        }

        public bool HasFaces(IList<DetectedFace> faces) => faces.Count > 0;
    }
}
using Windows.Media.Capture;
using Windows.Media.Devices;
using System.Threading.Tasks;

namespace RajCam.Services
{
    public class ProCameraControlService
    {
        private MediaCapture _mediaCapture;

        public void Initialize(MediaCapture mediaCapture)
        {
            _mediaCapture = mediaCapture;
        }

        public async Task SetManualExposureAsync(double exposureTime)
        {
            if (_mediaCapture?.VideoDeviceController?.ExposureControl?.Supported == true)
            {
                await _mediaCapture.VideoDeviceController.ExposureControl.SetValueAsync(
                    System.TimeSpan.FromMilliseconds(exposureTime));
            }
        }

        public async Task SetManualFocusAsync(double focusValue)
        {
            if (_mediaCapture?.VideoDeviceController?.FocusControl?.Supported == true)
            {
                await _mediaCapture.VideoDeviceController.FocusControl.SetValueAsync((uint)focusValue);
            }
        }

        public async Task SetISOAsync(uint isoValue)
        {
            if (_mediaCapture?.VideoDeviceController?.IsoSpeedControl?.Supported == true)
            {
                await _mediaCapture.VideoDeviceController.IsoSpeedControl.SetValueAsync(isoValue);
            }
        }

        public async Task SetWhiteBalanceAsync(uint temperature)
        {
            if (_mediaCapture?.VideoDeviceController?.WhiteBalanceControl?.Supported == true)
            {
                await _mediaCapture.VideoDeviceController.WhiteBalanceControl.SetValueAsync(temperature);
            }
        }

        public void SetZoom(double zoomFactor)
        {
            if (_mediaCapture?.VideoDeviceController?.ZoomControl?.Supported == true)
            {
                _mediaCapture.VideoDeviceController.ZoomControl.Value = zoomFactor;
            }
        }
    }
}
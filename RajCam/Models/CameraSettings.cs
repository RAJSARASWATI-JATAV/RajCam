using System.ComponentModel;

namespace RajCam.Models
{
    public class CameraSettings : INotifyPropertyChanged
    {
        private string _resolution = "1920x1080";
        private string _filter = "None";
        private bool _hdrEnabled = false;
        private bool _gridEnabled = false;
        private int _brightness = 50;
        private int _contrast = 50;
        private int _saturation = 50;

        public string Resolution
        {
            get => _resolution;
            set { _resolution = value; OnPropertyChanged(nameof(Resolution)); }
        }

        public string Filter
        {
            get => _filter;
            set { _filter = value; OnPropertyChanged(nameof(Filter)); }
        }

        public bool HdrEnabled
        {
            get => _hdrEnabled;
            set { _hdrEnabled = value; OnPropertyChanged(nameof(HdrEnabled)); }
        }

        public bool GridEnabled
        {
            get => _gridEnabled;
            set { _gridEnabled = value; OnPropertyChanged(nameof(GridEnabled)); }
        }

        public int Brightness
        {
            get => _brightness;
            set { _brightness = value; OnPropertyChanged(nameof(Brightness)); }
        }

        public int Contrast
        {
            get => _contrast;
            set { _contrast = value; OnPropertyChanged(nameof(Contrast)); }
        }

        public int Saturation
        {
            get => _saturation;
            set { _saturation = value; OnPropertyChanged(nameof(Saturation)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
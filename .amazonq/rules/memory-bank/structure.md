# RAJ CAM - Project Structure

## Directory Organization

```
RajCam_WinUI3/
├── RajCam/
│   ├── Services/          # 13 Professional Services
│   ├── Views/             # 4 Pages (Camera, Pro, Gallery, Settings)
│   ├── ViewModels/        # MVVM ViewModels
│   ├── Models/            # Data Models
│   ├── Controls/          # Custom Controls
│   ├── Converters/        # XAML Converters
│   ├── Helpers/           # Utilities
│   ├── Constants/         # App Constants
│   └── Assets/            # Icons & Resources
├── docs/                  # Documentation
└── RajCam.sln             # Solution File
```

## Core Components

### Views (4 Pages)
- **CameraPage**: Standard camera mode
- **ProCameraPage**: Professional mode with AI features
- **GalleryPage**: Media gallery
- **SettingsPage**: Application settings

### Services (13 Services)
- **CameraService**: Camera operations
- **ProCameraControlService**: Manual controls
- **FaceDetectionService**: AI face detection
- **AdvancedImageService**: AI beautify
- **FilterService**: Creative filters
- **HdrService**: HDR processing
- **NightModeService**: Low-light optimization
- **BurstModeService**: Burst capture
- **ProVideoService**: 4K video recording
- **LiveStreamService**: Live streaming
- **StorageService**: File management
- **ExportService**: Export functionality
- **ImageService**: Image processing

## Architecture

- **MVVM Pattern**: Separation of UI and business logic
- **Service-Oriented**: 13 specialized services
- **Dependency Injection**: Loose coupling

## Capture Flow

1. User interaction → ViewModel
2. ViewModel → Services (Camera, AI, Filters)
3. Services → Storage
4. Gallery displays content

## Technology Stack

- **WinUI 3**: Modern Windows UI
- **Windows App SDK 1.5**: Camera & file access
- **CommunityToolkit**: MVVM helpers
- **.NET 8.0**: Runtime
- **x64 Platform**: Windows 10/11

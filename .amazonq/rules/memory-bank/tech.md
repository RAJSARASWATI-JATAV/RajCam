# RAJ CAM - Technology Stack

## Programming Languages
- **C# 12**: Primary language for application logic
- **XAML**: UI markup for WinUI 3 views
- **HTML/CSS**: Documentation landing page

## Framework & Runtime
- **.NET 8.0**: Latest .NET runtime
- **Target Framework**: net8.0-windows10.0.19041.0
- **Minimum Version**: Windows 10.0.17763.0
- **Platform**: x64 architecture
- **Runtime Identifier**: win-x64

## UI Framework
- **WinUI 3**: Modern Windows native UI framework
- **Windows App SDK 1.5.240311000**: Core Windows APIs
- **Fluent Design System**: Microsoft design language
- **Mica Material**: Modern window backdrop
- **Acrylic Effects**: Translucent UI elements

## NuGet Dependencies
```xml
<PackageReference Include="Microsoft.WindowsAppSDK" Version="1.5.240311000" />
<PackageReference Include="Microsoft.Windows.SDK.BuildTools" Version="10.0.22621.2428" />
<PackageReference Include="CommunityToolkit.WinUI.UI.Controls" Version="7.1.2" />
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.2.2" />
```

## Architecture Pattern
- **MVVM**: Model-View-ViewModel pattern
- **Service-Oriented**: 13 specialized services
- **Dependency Injection**: Constructor injection

## Windows APIs
- **MediaCapture API**: Camera hardware access
- **Windows.Storage**: File system operations
- **Windows.Graphics.Imaging**: Image processing
- **Windows.Media**: Video encoding/decoding
- **Windows.ApplicationModel.DataTransfer**: Sharing functionality

## Development Tools
- **Visual Studio 2022**: Primary IDE
- **Windows 11 SDK**: Build 22621+
- **.NET 8 SDK**: Required for compilation

## Build Commands
```cmd
# Quick build
build.cmd

# Build executable
build-exe.cmd

# Full build
BUILD_NOW.cmd

# Run application
run.cmd
```

## Project Configuration
- **Output Type**: WinExe (Windows executable)
- **Self-Contained**: false (framework-dependent)
- **Package Type**: None (unpackaged deployment)
- **Administrator Rights**: Enabled via app.manifest

## Build Output
- **Executable**: RajCam.exe
- **Location**: RajCam\bin\x64\Release\net8.0-windows10.0.19041.0\
- **Deployment**: Xcopy deployment (no installer required)

## System Requirements
- **OS**: Windows 11 (Build 22000+)
- **Privileges**: Administrator (automatic elevation)
- **Camera**: Compatible webcam or built-in camera
- **.NET**: .NET 8 Runtime (if not self-contained)

# RAJ CAM - Development Guidelines

## Code Quality Standards

### Naming Conventions
- **Private fields**: Prefix with underscore `_fieldName` (5/5 files)
- **Async methods**: Suffix with `Async` (5/5 files)
- **Boolean fields**: Prefix with `is` or verb (e.g., `_isRecording`, `_gridEnabled`)
- **Services**: Suffix with `Service` (e.g., `CameraService`, `HdrService`)
- **Event handlers**: Format as `ElementName_EventName` (e.g., `CaptureButton_Click`)

### File Organization
- **Using statements**: Group at top, no blank lines between groups
- **Namespace**: Single namespace per file matching folder structure
- **Class structure order**:
  1. Private fields
  2. Constructor
  3. Public methods
  4. Private methods
  5. Event handlers

### Code Formatting
- **Braces**: Opening brace on same line for methods and control structures
- **Indentation**: 4 spaces (no tabs)
- **Line length**: Keep reasonable, break long method chains
- **Blank lines**: Single blank line between methods

## MVVM Pattern Implementation

### ViewModels (CommunityToolkit.Mvvm)
```csharp
public partial class CameraViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isRecording;  // Auto-generates IsRecording property
    
    [RelayCommand]
    private async Task CapturePhoto()  // Auto-generates CapturePhotoCommand
    {
        // Implementation
    }
}
```
- Use `ObservableObject` base class for ViewModels
- Use `[ObservableProperty]` for bindable properties (auto-generates public property)
- Use `[RelayCommand]` for commands (auto-generates ICommand)
- Field names in camelCase, generated properties in PascalCase

### Models (INotifyPropertyChanged)
```csharp
public class CameraSettings : INotifyPropertyChanged
{
    private string _resolution = "1920x1080";
    
    public string Resolution
    {
        get => _resolution;
        set { _resolution = value; OnPropertyChanged(nameof(Resolution)); }
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
```
- Implement `INotifyPropertyChanged` for data models
- Use expression-bodied members for getters
- Call `OnPropertyChanged` in setters
- Use `nameof()` for property names

### Views (Code-Behind)
- Keep minimal logic in code-behind
- Use for UI-specific operations (animations, dialogs, navigation)
- Initialize services in constructor
- Use `sealed` keyword for page classes

## Service Layer Patterns

### Service Initialization
```csharp
public class CameraService
{
    private MediaCapture _mediaCapture;
    private bool _isInitialized = false;
    
    public async Task<bool> InitializeAsync()
    {
        try
        {
            _mediaCapture = new MediaCapture();
            await _mediaCapture.InitializeAsync();
            _isInitialized = true;
            return true;
        }
        catch
        {
            _isInitialized = false;
            return false;
        }
    }
}
```
- Track initialization state with boolean flag
- Return success/failure from initialization
- Handle exceptions gracefully

### Service Methods
```csharp
public async Task<StorageFile> CapturePhotoAsync()
{
    if (!_isInitialized) return null;
    
    var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
        $"RAJ_CAM_Photo_{DateTime.Now:yyyyMMdd_HHmmss}.jpg",
        CreationCollisionOption.GenerateUniqueName);
    
    await _mediaCapture.CapturePhotoToStorageFileAsync(
        ImageEncodingProperties.CreateJpeg(), file);
    
    return file;
}
```
- Check initialization state before operations
- Return null for failure cases
- Use descriptive file naming with timestamps
- Return created files for caller handling

### Service Disposal
```csharp
public void Dispose()
{
    _mediaCapture?.Dispose();
    _isInitialized = false;
}
```
- Implement disposal for resource cleanup
- Use null-conditional operator for safe disposal
- Reset state flags on disposal

## Error Handling

### Try-Catch Pattern
```csharp
private async void CaptureButton_Click(object sender, RoutedEventArgs e)
{
    try
    {
        await CapturePhoto();
    }
    catch (Exception ex)
    {
        await ShowDialog("Error", $"Failed to capture photo: {ex.Message}");
    }
}
```
- Wrap async operations in try-catch
- Show user-friendly error dialogs
- Include exception message in error display
- Use specific error titles ("Camera Error", "Error")

### Null Checking
```csharp
if (!_isInitialized) return null;
if (file != null) PhotoCount++;
```
- Check initialization states before operations
- Return early for invalid states
- Use null-conditional operators: `_mediaCapture?.Dispose()`

## Async/Await Patterns

### Async Event Handlers
```csharp
private async void CaptureButton_Click(object sender, RoutedEventArgs e)
{
    await CapturePhoto();
}
```
- Use `async void` for event handlers only
- Use `async Task` for all other async methods
- Always await async operations

### Task Delays
```csharp
for (int i = _timerSeconds; i > 0; i--)
{
    TimerCountdown.Text = i.ToString();
    await Task.Delay(1000);
}
```
- Use `Task.Delay()` for timed operations
- Await delays in loops for countdown timers

## UI State Management

### Toggle Pattern
```csharp
private void GridButton_Click(object sender, RoutedEventArgs e)
{
    _gridEnabled = !_gridEnabled;
    GridOverlay.Visibility = _gridEnabled ? Visibility.Visible : Visibility.Collapsed;
    GridButton.Background = new SolidColorBrush(
        _gridEnabled ? ColorHelper.FromArgb(255, 0, 212, 255) : 
                      ColorHelper.FromArgb(255, 51, 51, 51));
}
```
- Toggle boolean state first
- Update UI visibility based on state
- Update button background to indicate active state
- Active color: `#00D4FF` (cyan), Inactive: `#333333` (dark gray)

### Recording State
```csharp
_isRecording = true;
RecordButton.Content = "⏹";
RecordButton.Background = new SolidColorBrush(Colors.Red);
```
- Update state flag
- Change button content (emoji icons)
- Change button color to indicate state

## File Naming Conventions

### Captured Media
```csharp
$"RAJ_CAM_Photo_{DateTime.Now:yyyyMMdd_HHmmss}.jpg"
$"RAJ_CAM_Video_{DateTime.Now:yyyyMMdd_HHmmss}.mp4"
```
- Prefix: `RAJ_CAM_`
- Type: `Photo_` or `Video_`
- Timestamp: `yyyyMMdd_HHmmss` format
- Extension: `.jpg` for photos, `.mp4` for videos

## Windows API Usage

### MediaCapture Initialization
```csharp
_mediaCapture = new MediaCapture();
await _mediaCapture.InitializeAsync();
PreviewControl.Source = _mediaCapture;
await _mediaCapture.StartPreviewAsync();
```
- Create MediaCapture instance
- Initialize asynchronously
- Assign to preview control
- Start preview separately

### Camera Controls
```csharp
if (_mediaCapture?.VideoDeviceController?.ExposureControl?.Supported == true)
{
    await _mediaCapture.VideoDeviceController.ExposureControl.SetValueAsync(
        TimeSpan.FromMilliseconds(exposureTime));
}
```
- Check null safety with chained null-conditional operators
- Check `Supported` property before using controls
- Await control value changes

## Dialog Pattern

### ContentDialog Usage
```csharp
private async Task ShowDialog(string title, string message)
{
    ContentDialog dialog = new ContentDialog
    {
        Title = title,
        Content = message,
        CloseButtonText = "OK",
        XamlRoot = this.XamlRoot
    };
    await dialog.ShowAsync();
}
```
- Create reusable dialog helper method
- Set `XamlRoot` for proper rendering
- Use simple OK button for notifications
- Await dialog display

## Animation Patterns

### Flash Effect
```csharp
var animation = new DoubleAnimation
{
    From = 0.8,
    To = 0,
    Duration = TimeSpan.FromMilliseconds(200)
};

var storyboard = new Storyboard();
storyboard.Children.Add(animation);
Storyboard.SetTarget(animation, FlashEffect);
Storyboard.SetTargetProperty(animation, "Opacity");
storyboard.Begin();
```
- Use `DoubleAnimation` for opacity changes
- Wrap in `Storyboard` for execution
- Set target and property explicitly
- Use short durations (200ms) for flash effects

## Navigation Pattern

```csharp
Frame.Navigate(typeof(GalleryPage));
```
- Use `Frame.Navigate()` with page type
- No parameters needed for simple navigation
- Frame is available in Page code-behind

## Constants and Magic Numbers

### Color Values
- Active cyan: `ColorHelper.FromArgb(255, 0, 212, 255)`
- Inactive gray: `ColorHelper.FromArgb(255, 51, 51, 51)`
- Recording red: `Colors.Red` or `ColorHelper.FromArgb(255, 255, 68, 68)`
- Flash gold: `ColorHelper.FromArgb(255, 255, 215, 0)`

### Timer Values
- Short timer: 3 seconds
- Long timer: 10 seconds
- Flash duration: 200 milliseconds
- Countdown interval: 1000 milliseconds

### Burst Mode
- Burst count: 5 photos

## Best Practices Summary

1. **Always use async/await** for I/O operations
2. **Check initialization state** before service operations
3. **Handle exceptions** with user-friendly dialogs
4. **Use MVVM pattern** with CommunityToolkit attributes
5. **Implement INotifyPropertyChanged** for data models
6. **Prefix private fields** with underscore
7. **Suffix async methods** with Async
8. **Use null-conditional operators** for safe navigation
9. **Return early** for invalid states
10. **Keep code-behind minimal** - delegate to services

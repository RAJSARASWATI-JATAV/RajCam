@echo off
echo Building RAJ CAM...
dotnet restore RajCam\RajCam.csproj
dotnet build RajCam\RajCam.csproj -c Release
echo Build complete!
pause
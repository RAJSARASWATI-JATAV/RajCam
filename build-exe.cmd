@echo off
echo ========================================
echo RAJ CAM - Building EXE with Admin Rights
echo ========================================
echo.

cd RajCam

echo [1/4] Cleaning previous builds...
dotnet clean -c Release

echo.
echo [2/4] Restoring packages...
dotnet restore

echo.
echo [3/4] Building Release version...
dotnet build -c Release

echo.
echo [4/4] Publishing EXE...
dotnet publish -c Release -r win-x64 --self-contained false -p:PublishSingleFile=false

echo.
echo ========================================
echo BUILD COMPLETE!
echo ========================================
echo.
echo EXE Location:
echo bin\x64\Release\net8.0-windows10.0.19041.0\win-x64\publish\RajCam.exe
echo.
echo This EXE will run with Administrator rights!
echo.
pause
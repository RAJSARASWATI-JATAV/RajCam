@echo off
echo ========================================
echo RAJ CAM - FINAL BUILD SCRIPT
echo ========================================
echo.

cd RajCam

echo Cleaning previous builds...
dotnet clean --configuration Release --verbosity quiet

echo.
echo Building RAJ CAM in Release mode...
dotnet build --configuration Release --no-restore --verbosity minimal

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo ✅ BUILD SUCCESSFUL!
    echo ========================================
    echo.
    echo 📁 Executable Location:
    echo    RajCam\bin\x64\Release\net8.0-windows10.0.19041.0\win-x64\RajCam.exe
    echo.
    echo 🚀 To run the application:
    echo    Double-click RajCam.exe or run: .\RajCam\bin\x64\Release\net8.0-windows10.0.19041.0\win-x64\RajCam.exe
    echo.
    echo ========================================
    echo 🎉 RAJ CAM IS READY TO USE!
    echo ========================================
) else (
    echo.
    echo ❌ BUILD FAILED!
    echo Check the error messages above.
)

pause
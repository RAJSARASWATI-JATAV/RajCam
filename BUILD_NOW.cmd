@echo off
echo ========================================
echo RAJ CAM - Building with Visual Studio
echo ========================================
echo.

REM Find Visual Studio MSBuild
set "VSWHERE=%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"

if exist "%VSWHERE%" (
    for /f "usebackq tokens=*" %%i in (`"%VSWHERE%" -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe`) do (
        set MSBUILD=%%i
    )
)

if not defined MSBUILD (
    echo ERROR: Visual Studio 2022 not found!
    echo Please install Visual Studio 2022 with .NET Desktop Development workload
    pause
    exit /b 1
)

echo Found MSBuild: %MSBUILD%
echo.

echo [1/3] Restoring NuGet packages...
"%MSBUILD%" RajCam.sln /t:Restore /p:Configuration=Release /p:Platform=x64

echo.
echo [2/3] Building solution...
"%MSBUILD%" RajCam.sln /t:Build /p:Configuration=Release /p:Platform=x64 /m

echo.
echo [3/3] Checking output...
if exist "RajCam\bin\x64\Release\net8.0-windows10.0.19041.0\RajCam.exe" (
    echo.
    echo ========================================
    echo BUILD SUCCESS! 
    echo ========================================
    echo.
    echo EXE Location:
    echo RajCam\bin\x64\Release\net8.0-windows10.0.19041.0\RajCam.exe
    echo.
    echo File size:
    dir "RajCam\bin\x64\Release\net8.0-windows10.0.19041.0\RajCam.exe" | findstr "RajCam.exe"
    echo.
    echo Now upload to GitHub Releases!
    echo https://github.com/RAJSARASWATI-JATAV/RajCam/releases/new
    echo.
) else (
    echo.
    echo ========================================
    echo BUILD FAILED
    echo ========================================
    echo.
    echo Please check the error messages above.
    echo You may need to open Visual Studio 2022 and build manually.
    echo.
)

pause
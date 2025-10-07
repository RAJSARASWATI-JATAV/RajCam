@echo off
echo ========================================
echo RAJ CAM - GitHub Upload
echo ========================================
echo.

cd /d "%~dp0"

echo Initializing Git...
git init

echo.
echo Adding all files...
git add .

echo.
echo Creating commit...
git commit -m "Initial commit: RAJ CAM - Ultimate Professional Camera Suite"

echo.
echo Adding remote repository...
git remote add origin https://github.com/RAJSARASWATI-JATAV/RajCam.git

echo.
echo Pushing to GitHub...
git branch -M main
git push -u origin main

echo.
echo ========================================
echo ✅ Code Uploaded to GitHub!
echo ========================================
echo.
echo 📝 Next: Create Release with EXE
echo.
echo 1. Go to: https://github.com/RAJSARASWATI-JATAV/RajCam/releases/new
echo 2. Tag: v1.0.0
echo 3. Title: RAJ CAM v1.0.0 - Ultimate Professional Camera Suite
echo 4. Upload EXE from: RajCam\bin\x64\Release\net8.0-windows10.0.19041.0\win-x64\RajCam.exe
echo 5. Copy description from: RELEASE_NOTES_v1.0.0.md
echo 6. Click "Publish release"
echo.
echo ========================================

pause

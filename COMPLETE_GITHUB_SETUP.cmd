@echo off
echo ========================================
echo RAJ CAM - Complete GitHub Setup
echo ========================================
echo.

cd /d "%~dp0"

echo Step 1: Building Release Version...
cd RajCam
dotnet build --configuration Release --verbosity minimal
cd ..

echo.
echo Step 2: Initializing Git Repository...
git init

echo.
echo Step 3: Adding all files...
git add .

echo.
echo Step 4: Creating initial commit...
git commit -m "Initial commit: RAJ CAM - Ultimate Professional Camera Suite with all features"

echo.
echo ========================================
echo ✅ Local Git Setup Complete!
echo ========================================
echo.
echo 📝 NEXT STEPS (Manual):
echo.
echo 1. Go to: https://github.com/new
echo 2. Repository name: RajCam-WinUI3
echo 3. Description: Ultimate Professional Camera Suite for Windows 11
echo 4. Choose Public or Private
echo 5. Click "Create repository"
echo.
echo 6. Then run these commands (replace YOUR_USERNAME):
echo.
echo    git remote add origin https://github.com/YOUR_USERNAME/RajCam-WinUI3.git
echo    git branch -M main
echo    git push -u origin main
echo.
echo 7. For Release with EXE:
echo    - Go to your GitHub repository
echo    - Click "Releases" ^> "Create a new release"
echo    - Tag: v1.0.0
echo    - Title: RAJ CAM v1.0.0 - Ultimate Professional Camera Suite
echo    - Upload: RajCam\bin\x64\Release\net8.0-windows10.0.19041.0\win-x64\RajCam.exe
echo    - Click "Publish release"
echo.
echo ========================================

pause

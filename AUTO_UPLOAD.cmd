@echo off
echo ========================================
echo RAJ CAM - Automatic GitHub Upload
echo ========================================
echo.

cd /d "%~dp0"

echo [1/5] Initializing Git...
git init
if errorlevel 1 goto error

echo.
echo [2/5] Adding files...
git add .
if errorlevel 1 goto error

echo.
echo [3/5] Creating commit...
git commit -m "Initial commit: RAJ CAM - Ultimate Professional Camera Suite"
if errorlevel 1 goto error

echo.
echo [4/5] Adding remote...
git remote add origin https://github.com/RAJSARASWATI-JATAV/RajCam.git
if errorlevel 1 (
    echo Remote already exists, updating...
    git remote set-url origin https://github.com/RAJSARASWATI-JATAV/RajCam.git
)

echo.
echo [5/5] Pushing to GitHub...
echo.
echo ⚠️  GitHub will ask for credentials:
echo    Username: RAJSARASWATI-JATAV
echo    Password: Your Personal Access Token
echo.
echo 📝 Don't have a token? Get it from:
echo    https://github.com/settings/tokens/new
echo    (Select: repo, workflow)
echo.
pause

git branch -M main
git push -u origin main

if errorlevel 1 goto error

echo.
echo ========================================
echo ✅ SUCCESS! Code uploaded to GitHub!
echo ========================================
echo.
echo 🌐 View at: https://github.com/RAJSARASWATI-JATAV/RajCam
echo.
echo 📦 Next: Create Release
echo    1. Go to: https://github.com/RAJSARASWATI-JATAV/RajCam/releases/new
echo    2. Tag: v1.0.0
echo    3. Title: RAJ CAM v1.0.0
echo    4. Upload: RajCam\bin\x64\Release\net8.0-windows10.0.19041.0\win-x64\RajCam.exe
echo    5. Publish!
echo.
goto end

:error
echo.
echo ❌ ERROR occurred!
echo.
echo Common fixes:
echo 1. Install Git: https://git-scm.com/download/win
echo 2. Check internet connection
echo 3. Verify GitHub repository exists
echo.

:end
pause

@echo off
echo ========================================
echo RAJ CAM - GitHub Upload Script
echo ========================================
echo.

echo Step 1: Initializing Git...
git init

echo.
echo Step 2: Adding all files...
git add .

echo.
echo Step 3: Creating commit...
git commit -m "Initial commit: RAJ CAM - Ultimate Professional Camera Suite"

echo.
echo ========================================
echo ✅ Git Setup Complete!
echo ========================================
echo.
echo 📝 Next Steps:
echo.
echo 1. Go to GitHub.com and create a new repository
echo 2. Name it: RajCam-WinUI3
echo 3. Run these commands:
echo.
echo    git remote add origin https://github.com/YOUR_USERNAME/RajCam-WinUI3.git
echo    git branch -M main
echo    git push -u origin main
echo.
echo Replace YOUR_USERNAME with your GitHub username
echo.
echo ========================================

pause

@echo off
cd /d "%~dp0"

:: Open Chrome in debugging mode
start "" "C:\Program Files\Google\Chrome\Application\chrome.exe" --remote-debugging-port=9222 --user-data-dir="C:\ChromeDebug"

:: Wait for Chrome to start
ping -n 5 127.0.0.1 >nul

:: Run all tests sequentially
call :RunTest "LoginPage"
call :RunTest "ViewItem"
call :RunTest "AddToCart"
call :RunTest "Checkout"
call :RunTest "Checkout2"
call :RunTest "CheckoutOverview"
call :RunTest "ConfirmationPage"

:: Wait before closing Chrome
ping -n 5 127.0.0.1 >nul

:: Close Chrome Debugging instance (Port 9222)
echo Closing Chrome...
taskkill /F /IM chrome.exe >nul 2>&1
taskkill /F /IM chromedriver.exe >nul 2>&1

exit /b

:: Function Definition
:RunTest
echo Running TestCategory=%~1...
cmd /c "dotnet test --filter \"(TestCategory=%~1)\""
ping -n 10 127.0.0.1 >nul
exit /b

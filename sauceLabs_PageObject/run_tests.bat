@echo off
cd /d "C:\Users\shkar\source\repos\sauceLabs_PageObject\sauceLabs_PageObject"

:: Open Chrome in debugging mode using full path
start "" "C:\Program Files\Google\Chrome\Application\chrome.exe" --remote-debugging-port=9222 --user-data-dir="C:\ChromeDebug"

:: Wait for Chrome to start
timeout /t 5 >nul

:: Run all tests sequentially
call :RunTest "LoginPage"
call :RunTest "ViewItem"
call :RunTest "AddToCart"
call :RunTest "Checkout"
call :RunTest "Checkout2"
call :RunTest "CheckoutOverview"
call :RunTest "ConfirmationPage"

:: Wait before closing Chrome
timeout /t 5 >nul

:: Close Chrome Debugging instance (Port 9222)
echo Closing Chrome...
taskkill /F /IM chrome.exe >nul 2>&1
taskkill /F /IM chromedriver.exe >nul 2>&1

exit /b

:: Function Definition
:RunTest
echo Running TestCategory=%~1...
start cmd /c "dotnet test --filter \"(TestCategory=%~1)\" --logger:trx --results-directory TestResults && timeout /t 10 >nul && exit"
timeout /t 10 >nul
exit /b

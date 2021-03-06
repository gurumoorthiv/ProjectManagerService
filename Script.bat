@echo off
REM ** Be sure to install these nugets:
REM ** NUnit.ConsoleRunner
REM ** OpenCover
REM ** ReportGenerator
REM **
REM ** All paths should be entered without quotes

REM ** SET TestResultsFileProjectName=CalculatorResults
SET TestResultsFileProjectName=ProjectManagerTests

REM ** SET DLLToTestRelativePath=Calculator\bin\Debug\MyCalc.dll
SET DLLToTestRelativePath=ProjectManager.API.Tests\bin\Debug\ProjectManager.API.Tests.dll

REM ** Filters Wiki https://github.com/opencover/opencover/wiki/Usage
REM ** SET Filters=+[Calculator]* -[Calculator]CalculatorTests.*
SET Filters=+[ProjectManager.API]*Controller -[ProjectManager.API.Tests]*

SET OpenCoverFolderName=OpenCover.4.6.519
SET NUnitConsoleRunnerFolderName=NUnit.ConsoleRunner.3.9.0
SET ReportGeneratorFolderName=reportgenerator.3.1.2

REM *****************************************************************

REM Create a 'GeneratedReports' folder if it does not exist
if not exist "%~dp0GeneratedReports" mkdir "%~dp0GeneratedReports"

REM if not exist "%~dp0GeneratedReports\ReportGeneratorOutput" mkdir "%~dp0GeneratedReports\ReportGeneratorOutput"

REM Remove any previous test execution files to prevent issues overwriting
IF EXIST "%~dp0%TestResultsFileProjectName%.trx" del "%~dp0%TestResultsFileProjectName%.trx%"

REM Remove any previously created test output directories
CD %~dp0
FOR /D /R %%X IN (%USERNAME%*) DO RD /S /Q "%%X"

REM Run the tests against the targeted output
call :RunOpenCoverUnitTestMetrics

REM Generate the report output based on the test results
if %errorlevel% equ 0 (
 call :RunReportGeneratorOutput
)

REM Launch the report
if %errorlevel% equ 0 (
 call :RunLaunchReport
)
exit /b %errorlevel%

:RunOpenCoverUnitTestMetrics
"%~dp0packages\%OpenCoverFolderName%\tools\OpenCover.Console.exe" ^
-register:user ^
-target:"%~dp0packages\%NUnitConsoleRunnerFolderName%\tools\nunit3-console.exe" ^
-targetargs:"--noheader \"%~dp0%DLLToTestRelativePath%\"" ^
-filter:"%Filters%" ^
-mergebyhash ^
-skipautoprops ^
-excludebyattribute:"System.CodeDom.Compiler.GeneratedCodeAttribute" ^
-output:"%~dp0GeneratedReports\%TestResultsFileProjectName%.xml"
exit /b %errorlevel%

:RunReportGeneratorOutput
"%~dp0packages\%ReportGeneratorFolderName%\tools\ReportGenerator.exe" ^
-reports:"%~dp0GeneratedReports\%TestResultsFileProjectName%.xml" ^
-targetdir:"%~dp0GeneratedReports\ReportGeneratorOutput"
exit /b %errorlevel%

:RunLaunchReport
start "report" "%~dp0GeneratedReports\ReportGeneratorOutput\index.htm"
exit /b %errorlevel%

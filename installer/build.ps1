# Builds EduTrack-Setup-<version>.exe
#   1. compiles EduTrack (Release)
#   2. stages the app + a trimmed private MariaDB (the app starts it itself and stops it on exit)
#   3. compiles the installer with Inno Setup (winget install JRSoftware.InnoSetup)
#
# Usage:  powershell -ExecutionPolicy Bypass -File installer\build.ps1 [-Version 1.0.0] [-MariaDb "C:\Program Files\MariaDB 13.0"]
param(
    [string]$Version = "1.0.0",
    [string]$MariaDb = "C:\Program Files\MariaDB 13.0"
)
$ErrorActionPreference = "Stop"
$root  = Split-Path $PSScriptRoot -Parent
$stage = Join-Path $PSScriptRoot "stage"
$app   = Join-Path $stage "app"

$msbuild = "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe"
$iscc = @("$env:LOCALAPPDATA\Programs\Inno Setup 6\ISCC.exe", "C:\Program Files (x86)\Inno Setup 6\ISCC.exe") | Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not (Test-Path $msbuild)) { throw "MSBuild not found: $msbuild" }
if (-not $iscc) { throw "Inno Setup not found. Install it with: winget install JRSoftware.InnoSetup" }
if (-not (Test-Path "$MariaDb\bin\mariadbd.exe")) { throw "MariaDB not found at $MariaDb" }

Remove-Item $stage -Recurse -Force -ErrorAction SilentlyContinue
New-Item -ItemType Directory -Force $app | Out-Null

Write-Host "Building EduTrack (Release)..."
& $msbuild "$root\EduTrack.sln" -restore -v:m -nologo -p:Configuration=Release "-p:OutDir=$app\"
if ($LASTEXITCODE -ne 0) { throw "Build failed" }
Get-ChildItem $app -Recurse -Include *.pdb, *.xml | Remove-Item -Force

Write-Host "Staging the private database..."
$db = Join-Path $app "db"
New-Item -ItemType Directory -Force "$db\bin", "$db\lib" | Out-Null
Copy-Item "$MariaDb\bin\mariadbd.exe", "$MariaDb\bin\mysqld.exe", "$MariaDb\bin\mysql_install_db.exe" "$db\bin"
Copy-Item "$MariaDb\bin\*.dll" "$db\bin"
Copy-Item "$MariaDb\share" "$db\share" -Recurse
Copy-Item "$MariaDb\lib\plugin" "$db\lib\plugin" -Recurse
Get-ChildItem $db -Recurse -Include *.pdb, *.lib | Remove-Item -Force
foreach ($f in "COPYING", "THIRDPARTY", "CREDITS") { if (Test-Path "$MariaDb\$f") { Copy-Item "$MariaDb\$f" $db } }
Copy-Item "$PSScriptRoot\schema.sql" $db

Write-Host "Compiling the installer..."
& $iscc "/DAppVersion=$Version" "/DStageDir=$app" "/O$PSScriptRoot\output" "$PSScriptRoot\EduTrack.iss"
if ($LASTEXITCODE -ne 0) { throw "Installer compile failed" }
Write-Host "Done: $PSScriptRoot\output\EduTrack-Setup-$Version.exe"

<# 
.SYNOPSIS
    Installs the AdventPuzzleTest Visual Studio item template into all available
    Visual Studio "ItemTemplates" folders under the user's Documents directory.

.DESCRIPTION
    This script searches the user's Documents folder for all directories matching 
    "Visual Studio*". For each detected Visual Studio installation, it ensures
    the following folder exists:

        Templates\ItemTemplates\

    Then it copies the AdventPuzzleTest.zip template (stored in the repository at:
    
        %USERPROFILE%\source\repos\Advent\src\Templates\AdventPuzzleTest.zip

    into each Visual Studio ItemTemplates directory.

    Once installed, the template will appear inside Visual Studio under:
        Add → New Item → Advent Puzzle Test

    After running this script, you must restart Visual Studio for the template to load.

.NOTES
    Author: Your Name
    Requires: PowerShell 5+, Windows, Visual Studio item template support
    Template File: AdventPuzzleTest.zip

.EXAMPLE
    ./Install-AdventTemplate.ps1
    Installs the Advent Puzzle Test template for all Visual Studio versions.

#>

$documents = [Environment]::GetFolderPath("MyDocuments")

$sourceZip = Join-Path $env:USERPROFILE "source\repos\Advent\src\Templates\AdventPuzzleTest.zip"

Write-Host "Scanning Visual Studio folders in: $documents" -ForegroundColor Cyan

$vsFolders = Get-ChildItem -Path $documents -Directory |
    Where-Object { $_.Name -like "Visual Studio*" }

if (-not $vsFolders) {
    Write-Host "No Visual Studio folders found under Documents." -ForegroundColor Red
    exit 1
}

if (-not (Test-Path $sourceZip)) {
    Write-Host "Template ZIP not found at: $sourceZip" -ForegroundColor Red
    exit 1
}

foreach ($vsFolder in $vsFolders) {

    $itemTemplatesPath = Join-Path $vsFolder.FullName "Templates\ItemTemplates"

    Write-Host "`nFound Visual Studio folder:" -ForegroundColor Cyan
    Write-Host "   $($vsFolder.FullName)"

    if (-not (Test-Path $itemTemplatesPath)) {
        Write-Host "Creating ItemTemplates folder at:" -ForegroundColor Yellow
        Write-Host "   $itemTemplatesPath"
        New-Item -Path $itemTemplatesPath -ItemType Directory -Force | Out-Null
    }

    $destZip = Join-Path $itemTemplatesPath "AdventPuzzleTest.zip"

    Copy-Item -Path $sourceZip -Destination $destZip -Force

    Write-Host "Installed template into:" -ForegroundColor Green
    Write-Host "   $destZip"
}

Write-Host "`nInstallation complete!" -ForegroundColor Green
Write-Host "Restart Visual Studio to load the updated templates." -ForegroundColor Yellow

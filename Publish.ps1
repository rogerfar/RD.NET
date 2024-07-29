$scriptPath = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

$binDirectoryPath = ".\RDNET\bin"
if (Test-Path $binDirectoryPath) {
    Remove-Item -Path $binDirectoryPath -Recurse -Force
}

$csprojPath = Join-Path -Path $scriptPath -ChildPath "RDNET\RDNET.csproj"

if (Test-Path $csprojPath) {
    [xml]$csprojContent = Get-Content -Path $csprojPath
    
    $versionNode = $csprojContent.Project.PropertyGroup.Version
	    
    if ($versionNode -and $versionNode -match "2\.1\.(\d+)") {
        $newVersionNumber = [int]$matches[1] + 1
        $newVersion = "2.1.$newVersionNumber"
        
        $csprojContent.Project.PropertyGroup.Version = $newVersion
        
        $csprojContent.Save($csprojPath)
        
        Write-Output "Updated version to $newVersion."
    }
    else {
        Write-Error "Version format not recognized or not found."
		exit
    }
}
else {
    Write-Error "RDNET.csproj file not found."
	exit
}

dotnet publish ".\RDNET" -c Release

if ([string]::IsNullOrWhiteSpace($env:NUGET_API_KEY)) {
    $apiKey = Read-Host "Enter your NuGet API key"
    if (-not [string]::IsNullOrWhiteSpace($apiKey)) {
        [Environment]::SetEnvironmentVariable('NUGET_API_KEY', $apiKey, 'User')
		$env:NUGET_API_KEY = $apiKey
		Write-Host "Env variable NUGET_API_KEY set to $env:NUGET_API_KEY"
    } else {
        Write-Error "No API key provided. Exiting script."
        exit
    }
}

Write-Output "Commit the new version number now and press enter to publish to Nuget"
Read-Host

$packageFilePath = Get-ChildItem -Path ".\RDNET\bin\Release" -Filter "*.nupkg" | Select-Object -First 1
if ($packageFilePath -ne $null) {
    dotnet nuget push $packageFilePath.FullName --api-key $env:NUGET_API_KEY --source "https://api.nuget.org/v3/index.json"
}
else {
    Write-Error "No .nupkg file found in the publish directory."
	exit
}

Write-Output "Package published!"
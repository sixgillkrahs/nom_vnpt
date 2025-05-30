﻿. ".\common.ps1"

$apiKey = $args[0]

if(-Not $apiKey) {
$apiKey = "Csoft@123";
}

# Get the version
[xml]$commonPropsXml = Get-Content (Join-Path $rootFolder "aspnet-core/common.props")
$version = $commonPropsXml.Project.PropertyGroup.Version

# Publish all packages
$i = 0
$errorCount = 0
$totalProjectsCount = $projects.length
$nugetUrl = "https://10.0.10.80:3300/nuget"
Set-Location $packFolder

foreach($project in $projects) {
	$i += 1
	$projectFolder = Join-Path $rootFolder $project
	$projectName = ($project -split '/')[-1]
	$nugetPackageName = $projectName + "." + $version + ".nupkg"	
	$nugetPackageExists = Test-Path $nugetPackageName -PathType leaf
 
	Write-Info "[$i / $totalProjectsCount] - Pushing: $nugetPackageName"
	
	if ($nugetPackageExists)
	{
		dotnet nuget delete $projectName $version -s $nugetUrl --api-key "$apiKey"		
		#Write-Host ("Deleting package from local: " + $nugetPackageName)
		#Remove-Item $nugetPackageName -Force
	}
	else
	{
		Write-Host ("********** ERROR PACKAGE NOT FOUND: " + $nugetPackageName) -ForegroundColor red
		$errorCount += 1
		#Exit
	}
	
	Write-Host "--------------------------------------------------------------`r`n"
}

if ($errorCount > 0)
{
	Write-Host ("******* $errorCount error(s) occured *******") -ForegroundColor red
}

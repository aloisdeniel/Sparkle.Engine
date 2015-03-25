$msbuild = "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"

# Building a project and moving dll to binaries folder
function Build($platform, $project, $dll)
{
    & $msbuild $project /t:rebuild /p:Configuration=Release

    if($LASTEXITCODE -eq 0)
    {
        $file = (Get-ChildItem $dll).Name

        New-Item -ItemType Directory -Path "lib\$platform"
        Copy-Item  -Path $dll -Destination ".\lib\$platform\$file"
        Write-Host "[$platform][$project] Success" -ForegroundColor Green
    }
    else
    {
        Write-Host "[$platform][$project] Failed" -ForegroundColor Red
    }

}

# 1. Deleting old binaries

Remove-Item ".\lib" -Force -Recurse

# 2. Rebuilding projects

Build "net4" "..\Sparkle.Engine\Sparkle.Engine.Windows\Sparkle.Engine.Windows.csproj" "..\Sparkle.Engine\Sparkle.Engine.Windows\bin\Release\Sparkle.Engine.Windows.dll"
Build "wpa81" "..\Sparkle.Engine\Sparkle.Engine.WindowsPhone\Sparkle.Engine.WindowsPhone.csproj" "..\Sparkle.Engine\Sparkle.Engine.WindowsPhone\bin\Release\Sparkle.Engine.WindowsPhone.dll"
Build "win81" "..\Sparkle.Engine\Sparkle.Engine.WindowsStore\Sparkle.Engine.WindowsStore.csproj" "..\Sparkle.Engine\Sparkle.Engine.WindowsStore\bin\Release\Sparkle.Engine.WindowsStore.dll"
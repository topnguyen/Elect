param(
    [string]$SolutionRoot = (Get-Location)
)

$csprojFiles = Get-ChildItem -Path $SolutionRoot -Recurse -Filter *.csproj

foreach ($csproj in $csprojFiles) {
    $ProjectPath = Split-Path $csproj.FullName
    $globalUsingsPath = Join-Path $ProjectPath 'GlobalUsings.cs'
    $csFiles = Get-ChildItem -Path $ProjectPath -Recurse -Filter *.cs | Where-Object { $_.Name -ne 'GlobalUsings.cs' }

    $existingGlobalUsings = @()
    if (Test-Path $globalUsingsPath) {
        $existingGlobalUsings = Get-Content $globalUsingsPath | Where-Object { $_ -match '^global using\s+[^\=].*;' }
        $existingGlobalUsings = $existingGlobalUsings | ForEach-Object { $_ -replace '^global\s+', '' }
    }

    $usings = @()

    foreach ($file in $csFiles) {
        $lines = Get-Content $file.FullName
        $fileUsings = $lines | Where-Object { $_ -match '^using\s+[^\=].*;' }
        $usings += $fileUsings

        $inLicenseBlock = $false
        $inRegionLicense = $false
        $filteredLines = @()
        foreach ($line in $lines) {
            if ($line -match '^\s*#region\s+License') {
                $inRegionLicense = $true
                continue
            }
            if ($inRegionLicense -and $line -match '^\s*#endregion\s+License') {
                $inRegionLicense = $false
                continue
            }
            if ($inRegionLicense) { continue }

            if ($line -match '^\s*//\s*<License>') {
                $inLicenseBlock = $true
                continue
            }
            if ($inLicenseBlock -and $line -match '^\s*//\s*</License>') {
                $inLicenseBlock = $false
                continue
            }
            if ($inLicenseBlock) { continue }

            if ($fileUsings -contains $line) { continue }
            if ($line -match '^\s*$') { continue }
            $filteredLines += $line
        }

        Set-Content $file.FullName $filteredLines -Encoding UTF8
    }

    $allUsings = $usings + $existingGlobalUsings
    $allUsings = $allUsings | Sort-Object | Get-Unique
    $globalUsings = $allUsings | ForEach-Object { "global $_".Trim() }
    $globalUsings = $globalUsings | Where-Object { $_ -ne "" } | Sort-Object

    if ($globalUsings.Count -gt 1) {
        Set-Content $globalUsingsPath $globalUsings[0..($globalUsings.Count-2)] -Encoding UTF8
        $globalUsings[-1] | Out-File $globalUsingsPath -Encoding UTF8 -Append -NoNewline
    } elseif ($globalUsings.Count -eq 1) {
        $globalUsings | Out-File $globalUsingsPath -Encoding UTF8 -NoNewline
    } else {
        Clear-Content $globalUsingsPath
    }

    Write-Host "[$($csproj.Name)] GlobalUsings.cs created/updated, no trailing blank line."
}
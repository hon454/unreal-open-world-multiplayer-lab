# Read-only preflight. Run before committing/pushing assets or pulling LFS objects.
$ErrorActionPreference = 'Stop'
$repo = Split-Path -Parent $PSScriptRoot
$expected = 'https://gitea.jeonjihoon.dev/hon454/unreal-open-world-multiplayer-lab-lfs.git/info/lfs'

foreach ($key in @('lfs.url', 'lfs.pushurl')) {
    $value = git -C $repo config -f .lfsconfig --get $key
    if ($LASTEXITCODE -ne 0 -or $value -cne $expected) {
        throw "Repository .lfsconfig has a missing or unexpected $key."
    }
}

$overrides = @(git -C $repo config --get-regexp '^(lfs\.(url|pushurl)|remote\..*\.(lfsurl|lfspushurl))$')
if ($LASTEXITCODE -gt 1) { throw 'Unable to read Git LFS URL overrides.' }
foreach ($line in $overrides) {
    $parts = $line -split '\s+', 2
    if ($parts.Count -ne 2 -or $parts[1] -cne $expected) {
        throw 'A Git configuration override routes LFS to an unexpected destination.'
    }
}

$lfsEnv = @(git -C $repo lfs env)
if ($LASTEXITCODE -ne 0) { throw 'git lfs env failed; install Git LFS first.' }
$endpoints = @($lfsEnv | Where-Object { $_ -match '^Endpoint(?: \([^)]*\))?=' })
if ($endpoints.Count -eq 0) { throw 'No LFS endpoint reported.' }
foreach ($line in $endpoints) {
    if ($line -notmatch '^Endpoint(?: \([^)]*\))?=(\S+)' -or $Matches[1] -cne $expected) {
        throw 'The effective Git LFS endpoint is not the expected Gitea server.'
    }
}

Write-Output 'PASS: LFS download/upload configuration points to the NAS Gitea repository.'
Write-Output 'This read-only check does not upload files or verify server connectivity.'

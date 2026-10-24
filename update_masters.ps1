$basePath = 'c:\Users\admin\source\repos\Smard\Smard\Views'

$masters = @(
  [pscustomobject]@{folder='CeilingSystem';   infoTitle='Ceiling System Information';    sysBadge='CEILING SYSTEM';      sizeBadge='CEILING SIZE';           areaBadge='CEILING AREA' }
  [pscustomobject]@{folder='PeripheralLight'; infoTitle='Peripheral Light Information';  sysBadge='PERIPHERAL LIGHT';    sizeBadge='PERIPHERAL SIZE';        areaBadge='PERIPHERAL AREA' }
  [pscustomobject]@{folder='LaminarFlow';     infoTitle='Laminar Flow Information';      sysBadge='LAMINAR FLOW';        sizeBadge='LAMINAR FLOW SIZE';      areaBadge='LAMINAR FLOW AREA' }
  [pscustomobject]@{folder='AVSU';            infoTitle='AVSU Information';              sysBadge='AVSU SYSTEM';         sizeBadge='AVSU SIZE';              areaBadge='AVSU AREA' }
  [pscustomobject]@{folder='Doors';           infoTitle='Doors Information';             sysBadge='DOOR SYSTEM';         sizeBadge='DOOR SIZE';              areaBadge='DOOR AREA' }
  [pscustomobject]@{folder='StorageCabinet';  infoTitle='Storage Cabinet Information';   sysBadge='CABINET SYSTEM';      sizeBadge='CABINET SIZE';           areaBadge='CABINET AREA' }
  [pscustomobject]@{folder='Sink';            infoTitle='Sink Information';              sysBadge='SINK SYSTEM';         sizeBadge='SINK SIZE';              areaBadge='SINK AREA' }
  [pscustomobject]@{folder='WorkStation';     infoTitle='Work Station Information';      sysBadge='WORKSTATION SYSTEM';  sizeBadge='WORKSTATION SIZE';       areaBadge='WORKSTATION AREA' }
  [pscustomobject]@{folder='BHU';             infoTitle='BHU Information';               sysBadge='BHU SYSTEM';          sizeBadge='BHU SIZE';               areaBadge='BHU AREA' }
  [pscustomobject]@{folder='ReturnAirSystem'; infoTitle='Return Air System Information'; sysBadge='RETURN AIR SYSTEM';   sizeBadge='RETURN AIR SIZE';        areaBadge='RETURN AIR AREA' }
  [pscustomobject]@{folder='Windows';         infoTitle='Windows Information';           sysBadge='WINDOW SYSTEM';       sizeBadge='WINDOW SIZE';            areaBadge='WINDOW AREA' }
  [pscustomobject]@{folder='Counters';        infoTitle='Counters Information';          sysBadge='COUNTER SYSTEM';      sizeBadge='COUNTER SIZE';           areaBadge='COUNTER AREA' }
  [pscustomobject]@{folder='LGSPanelSystem';  infoTitle='LGS Panel System Information'; sysBadge='LGS PANEL SYSTEM';    sizeBadge='LGS PANEL SIZE';         areaBadge='LGS PANEL AREA' }
  [pscustomobject]@{folder='FixedFurnitures'; infoTitle='Fixed Furnitures Information'; sysBadge='FIXED FURNITURES';    sizeBadge='FIXED FURNITURE SIZE';   areaBadge='FIXED FURNITURE AREA' }
  [pscustomobject]@{folder='LooseFurnitures'; infoTitle='Loose Furnitures Information'; sysBadge='LOOSE FURNITURES';    sizeBadge='LOOSE FURNITURE SIZE';   areaBadge='LOOSE FURNITURE AREA' }
  [pscustomobject]@{folder='CSSDAFurnitures'; infoTitle='CSSD Furnitures Information';  sysBadge='CSSD FURNITURES';     sizeBadge='CSSD FURNITURE SIZE';    areaBadge='CSSD FURNITURE AREA' }
  [pscustomobject]@{folder='JobWorks';        infoTitle='Job Works Information';         sysBadge='JOB WORKS';           sizeBadge='JOB WORKS SIZE';         areaBadge='JOB WORKS AREA' }
)

foreach ($m in $masters) {
  $file = $basePath + '\' + $m.folder + '\Index.cshtml'
  if (-not (Test-Path $file)) { Write-Host ('SKIP: ' + $m.folder); continue }
  $c = [System.IO.File]::ReadAllText($file, [System.Text.Encoding]::UTF8)
  $orig = $c

  $c = $c.Replace('padding:24px 28px 40px', 'padding:72px 28px 40px')
  $c = [regex]::Replace($c, "wps-card-header-title'>[^<]+Information</span>", "wps-card-header-title'>" + $m.infoTitle + "</span>")
  $c = $c.Replace("badge-green`">SYSTEM</span>", "badge-green`">" + $m.sysBadge + "</span>")
  $c = $c.Replace("badge-orange`">SIZE</span>", "badge-orange`">" + $m.sizeBadge + "</span>")
  $c = $c.Replace("badge-red`">AREA</span>", "badge-red`">" + $m.areaBadge + "</span>")

  if ($c -ne $orig) {
    [System.IO.File]::WriteAllText($file, $c, [System.Text.Encoding]::UTF8)
    Write-Host ('UPDATED: ' + $m.folder)
  } else {
    Write-Host ('NO CHANGE: ' + $m.folder)
  }
}
Write-Host 'Done!'

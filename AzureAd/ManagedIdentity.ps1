New-AzUserAssignedIdentity -ResourceGroupName $ResourceGroupName -Name $IdentityName -Location $Location

$Identity=Get-AzResource -Name $IdentityName -ResourceGroupName $ResourceGroupName
$ResourceId=$Identity.Id


$vm = Get-AzVM -ResourceGroupName $ResourceGroupName -Name $VmName
Update-AzVM -ResourceGroupName $ResourceGroupName -VM $vm -IdentityType UserAssigned `
-IdentityID $ResourceId
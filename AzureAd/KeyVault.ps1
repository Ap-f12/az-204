
$Subcription=Get-AzSubscription -SubscriptionName $SubscriptionName
Set-AzContext -SubscriptionObject $Subcription


$StorageAccountKey=(Get-AzStorageAccountKey -ResourceGroupName $ResourceGroupName `
-AccountName $StorageAccountName) | Where-Object {$_.KeyName -eq "key1"}

$StorageAccountKeyValue=$StorageAccountKey.Value

$SecretValue = ConvertTo-SecureString $StorageAccountKeyValue -AsPlainText -Force


Set-AzKeyVaultSecret -VaultName $KeyVaultName -Name $StorageAccountName `
-SecretValue $SecretValue

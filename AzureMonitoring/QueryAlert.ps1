# Heartbeat | where TimeGenerated > ago(30m)



$LogQuery="Heartbeat | where TimeGenerated > ago(30m)"
$DataSourceId="workspace Id"

$RuleSource = New-AzScheduledQueryRuleSource -Query $LogQuery `
                  -DataSourceId $DataSourceId `
				  -QueryType "ResultCount"

$RuleSchedule=New-AzScheduledQueryRuleSchedule -FrequencyInMinutes 5 -TimeWindowInMinutes 5

$TriggerCondition=New-AzScheduledQueryRuleTriggerCondition -ThresholdOperator "GreaterThan" -Threshold 3

$ActionGroupId=""
$ActionGroup=New-AzScheduledQueryRuleAznsActionGroup -ActionGroup `
@($ActionGroupId) -EmailSubject "Log Alert"

$AlertAction = New-AzScheduledQueryRuleAlertingAction -AznsAction $ActionGroup -Severity "1" `
-Trigger $TriggerCondition


New-AzScheduledQueryRule -ResourceGroupName $ResourceGroupName -Location "Location" `
-Action $AlertAction -Enable $true -Description "Descritption" `
-Schedule $RuleSchedule -Source $RuleSource -Name "Log Analytics alert name"
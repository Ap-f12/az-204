### Create new Azure subscription programatically

[Link](https://docs.microsoft.com/en-us/azure/cost-management-billing/manage/programmatically-create-subscription-microsoft-customer-agreement?tabs=azure-powershell)

### Create New Subscription

```
New-AzManagementGroupDeployment -ManagementGroupId (Get-AzContext).Tenant.id -Location $location -TemplateFile azuredeploy.json -TemplateParameterFile myParameters.json
```

### Create alias for existing subscription or new subscription

```
az account alias create --name
                        [--billing-scope]
                        [--display-name]
                        [--no-wait]
                        [--reseller-id]
                        [--subscription-id]
                        [--workload {DevTest, Production}]
```

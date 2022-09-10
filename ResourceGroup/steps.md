### Create Resource Group

## Step1

```
New-AzResourceGroup -Name <resource-group-name> -Location <resource-group-location>
```

## Step2



```

New-AzResourceGroupDeployment -ResourceGroupName "ContosoEngineering" -TemplateFile "D:\Azure\Templates\EngineeringSite.json" -TemplateParameterFile "D:\Azure\Templates\EngSiteParms.json" -Tag @{"key1"="value1"; "key2"="value2";}

```

```

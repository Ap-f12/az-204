### Create Resource Group

```

New-AzDeployment `
  -Name demoSubDeployment `
  -Location centralus `
  -TemplateUri "https://raw.githubusercontent.com/Azure/azure-docs-json-samples/master/azure-resource-manager/emptyrg.json" `
  -rgName demoResourceGroup `
  -rgLocation centralus

```

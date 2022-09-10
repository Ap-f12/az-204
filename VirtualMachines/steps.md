### Deploye azure vm from template

```
new-azresourcegroupdeployment -resourcegroupname "rg-vm" -templatefile "C:\Users\user\Documents\GitHub\Azure\VirtualMachine\azuredeploy.json" -templateparameterfile "C:\Users\user\Documents\GitHub\Azure\VirtualMachine\azuredeploy.parameters.json"
```

### Deploy .net app on linux

- [Download putty](https://www.chiark.greenend.org.uk/~sgtatham/putty/latest.html)
- [winscp](https://winscp.net/eng/download.php)
- Login using putty and install .net 6.0 and nginx
- transfer publish folder using winscp to linux vm
  -stop nginx
- modify default file in /etc/nginx/sites-available
  - elevate permission to modify default `sudo chmod 777 default `
  - enter config
  ```
  	location / {
  	# First attempt to serve request as file, then
  	# as directory, then fall back to displaying a 404.
  	proxy_pass http://localhost:5000;
  	proxy_http_version 1.1;
  	proxy_set_header Upgrade $http_upgrade;
  	proxy_set_header Connection keep-alive;
  	proxy_set_header Host $host;
  	proxy_cache_bypass $http_upgrade;
  }
  ```
  - revert permissions `sudo chmod 644 default`
  - restart nginx `sudo systemctl restart nginx`

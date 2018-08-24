# AspNetCoreWithVSTSCiCdDemo

This is for 2018/08/25 Azure Tech Salon
http://study4.tw/Activity/Details/21

Azure App Service For Container 利用 VSTS 達成 CI/CD 整合



# Dotnet Build

```
dotnet build
```

## Docker Build

```bash
docker build .
```

## Docker Tag

```bash
docker tag demo0825:dev demo0825:082501
docker tag demo0825:latest abc12207/demo0825:latest
## For ACR
docker tag demo0825:latest demo0825.azurecr.io/demo0825:latest
```

## Remove All Container and Images

```bash
#!/bin/bash
# Delete all containers
docker rm $(docker ps -a -q)
# Delete all images
docker rmi $(docker images -q)
```

## Docker Push

```bash
docker push your-name/image-name:tag

## At ACR 必須使用 ACR 登入伺服器的完整名稱來標記映像
docker push demo0825.azurecr.io/demo0825:latest
```

## 登入 ACR

```bash
az login
az account set --subscription "Visual Studio Enterprise"
az acr login --name demo0825
## OR
docker login -u demo0825 -p yyChz3hEIA=O9tBYXuSAEiRpOido8usC demo0825.azurecr.io
```



# Demo Step (所需資源預先建立，避免浪費時間)

1. 建立新 ASP.NET Core 專案(不包含 Docker Support)
   1. 將範例程式碼 Copy 進去
   2. 直接執行網站，Show 環境
2. 加入 Docker Support
   1. 執行網站，Show 環境
3. 將網站手動發行到 Azure Container Registry
4. 建立 Azure Web App for Container
   1. 開啟網站
   2. 示範 SSL 支援
   3. 示範透過 app settings 直接改參數
5. 將程式碼 Commit 到 Github
6. 進入 VSTS 建立 Build and Release
   1. 設定 Trigger
   2. 設定 Build
   3. 設定 Release
7. 更改 Index Title 做為修改範例，並 Commit，觀看建置過程與結果
8. 示範 SSH 環境安裝與連線


FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY WebContainerDemo/WebContainerDemo.csproj WebContainerDemo/
RUN dotnet restore WebContainerDemo/WebContainerDemo.csproj
COPY . .
WORKDIR /src/WebContainerDemo
RUN dotnet build WebContainerDemo.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebContainerDemo.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebContainerDemo.dll"]

# set ssh
ENV SSH_PASSWD "root:Docker!"
RUN apt-get update \
        && apt-get install -y --no-install-recommends dialog \
        && apt-get update \
  && apt-get install -y --no-install-recommends openssh-server \
  && echo "$SSH_PASSWD" | chpasswd 
COPY startup /opt/startup
RUN chmod 755 /opt/startup/init_container.sh
ENTRYPOINT ["/opt/startup/init_container.sh"]
EXPOSE 2222 80
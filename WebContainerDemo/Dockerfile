FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 2222 80

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

RUN apt update \
  && apt install -y --no-install-recommends openssh-server \
  && mkdir -p /run/sshd \
  && echo "root:Docker!" | chpasswd

COPY sshd_config /etc/ssh/sshd_config

ENTRYPOINT ["/bin/bash", "-c", "/usr/sbin/sshd && dotnet WebContainerDemo.dll"]



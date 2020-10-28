# Author:KaneLeung(https://github.com/KaneLeung)
# Update:20201012
# NetCore SDK
# https://hub.docker.com/_/microsoft-dotnet-nightly-sdk/
FROM mcr.microsoft.com/dotnet/nightly/sdk:5.0-alpine AS build
WORKDIR /source

# Download Source
RUN git init
RUN git remote add -t main -m main origin https://gitee.com/monksoul/Fur.git
RUN git config core.sparseCheckout true
RUN echo framework >> .git/info/sparse-checkout
RUN git pull --depth 1 origin main

# Restore And Publish
WORKDIR /source/framework
RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

# Run Fur
# https://hub.docker.com/_/microsoft-dotnet-nightly-aspnet/
FROM mcr.microsoft.com/dotnet/nightly/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build /app ./
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Fur.Web.Entry.dll"]
# Author:KaneLeung(https://github.com/KaneLeung)
# Update:20201118
# NetCore SDK
# https://hub.docker.com/_/microsoft-dotnet-nightly-sdk/
FROM mcr.microsoft.com/dotnet/nightly/sdk:5.0-alpine AS build
WORKDIR /source

# Download Source
RUN git init
RUN git remote add -t master -m master origin https://gitee.com/dotnetchina/Furion.git
RUN git config core.sparseCheckout true
RUN echo samples >> .git/info/sparse-checkout
RUN git pull --depth 1 origin main

# Restore And Publish
WORKDIR /source/samples
RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

# Run Furion
# https://hub.docker.com/_/microsoft-dotnet-nightly-aspnet/
FROM mcr.microsoft.com/dotnet/nightly/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build /app ./
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Furion.Web.Entry.dll"]
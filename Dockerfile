# Author:KaneLeung(https://github.com/KaneLeung)
# Update:2023.02.28
# .NET7 SDK Docker
# https://hub.docker.com/_/microsoft-dotnet-sdk
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /source

# Download source
RUN git init
RUN git remote add origin https://gitee.com/dotnetchina/Furion.git
RUN git config core.sparseCheckout true
RUN echo samples >> .git/info/sparse-checkout
# Add Furion framework, if you use the NuGet package, you can delete it.
RUN echo framework >> .git/info/sparse-checkout
RUN git pull --depth 1 origin master

# Restore and Publish
WORKDIR /source/samples
RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

# Run Furion
# ASP.NET 7 Docker
# https://hub.docker.com/_/microsoft-dotnet-aspnet/
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=build /app ./
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Furion.Web.Entry.dll"]
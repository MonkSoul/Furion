#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src

RUN git init
RUN git remote add origin https://gitee.com/dotnetchina/Furion.git
RUN git config core.sparseCheckout true
RUN echo samples >> .git/info/sparse-checkout
RUN echo framework >> .git/info/sparse-checkout
RUN git pull --depth 1 origin v4

RUN dotnet restore "./samples/Furion.Web.Entry/Furion.Web.Entry.csproj"
RUN dotnet publish "./samples/Furion.Web.Entry/Furion.Web.Entry.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "Furion.Web.Entry.dll"]
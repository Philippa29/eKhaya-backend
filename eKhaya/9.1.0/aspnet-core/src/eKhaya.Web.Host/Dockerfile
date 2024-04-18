FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/eKhaya.Web.Host/eKhaya.Web.Host.csproj", "src/eKhaya.Web.Host/"]
COPY ["src/eKhaya.Web.Core/eKhaya.Web.Core.csproj", "src/eKhaya.Web.Core/"]
COPY ["src/eKhaya.Application/eKhaya.Application.csproj", "src/eKhaya.Application/"]
COPY ["src/eKhaya.Core/eKhaya.Core.csproj", "src/eKhaya.Core/"]
COPY ["src/eKhaya.EntityFrameworkCore/eKhaya.EntityFrameworkCore.csproj", "src/eKhaya.EntityFrameworkCore/"]
WORKDIR "/src/src/eKhaya.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/eKhaya.Web.Host", "src/eKhaya.Web.Host"]
COPY ["src/eKhaya.Web.Core", "src/eKhaya.Web.Core"]
COPY ["src/eKhaya.Application", "src/eKhaya.Application"]
COPY ["src/eKhaya.Core", "src/eKhaya.Core"]
COPY ["src/eKhaya.EntityFrameworkCore", "src/eKhaya.EntityFrameworkCore"]
WORKDIR "/src/src/eKhaya.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "eKhaya.Web.Host.dll"]

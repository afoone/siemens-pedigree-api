#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["siemens-pedigree-api/siemens-pedigree-api.csproj", "siemens-pedigree-api/"]
RUN dotnet restore "siemens-pedigree-api/siemens-pedigree-api.csproj"
COPY . .
WORKDIR "/src/siemens-pedigree-api"
RUN dotnet build "siemens-pedigree-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "siemens-pedigree-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "siemens-pedigree-api.dll"]

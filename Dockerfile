FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Capstone.Data.Entities/Capstone.Data.Entities.csproj", "Capstone.Data.Entities/"]
COPY ["Capstone.DTOs/Capstone.DTOs.csproj", "Capstone.DTOs/"]
COPY ["Capstone.Repositories/Capstone.Repositories.csproj", "Capstone.Repositories/"]
COPY ["Capstone.Api.Services/Capstone.Api.Services.csproj", "Capstone.Api.Services/"]

RUN dotnet restore "Capstone.Api.Services/Capstone.Api.Services.csproj"
COPY . .
WORKDIR "/src/Capstone.Api.Services"
RUN dotnet build "Capstone.Api.Services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Capstone.Api.Services.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Capstone.Api.Services.dll"]
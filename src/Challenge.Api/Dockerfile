FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/Challenge.Api/Challenge.Api.csproj", "src/Challenge.Api/"]
COPY ["src/Challenge.Application/Challenge.Application.csproj", "src/Challenge.Application/"]
COPY ["src/Challenge.Domain/Challenge.Domain.csproj", "src/Challenge.Domain/"]
COPY ["src/Challenge.Infrastructure/Challenge.Infrastructure.csproj", "src/Challenge.Infrastructure/"]
COPY ["src/Challenge.Infrastructure.Data/Challenge.Infrastructure.Data.csproj", "src/Challenge.Infrastructure.Data/"]

RUN dotnet restore "src/Challenge.Api/Challenge.Api.csproj"

COPY . .

RUN dotnet publish "src/Challenge.Api/Challenge.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Challenge.Api.dll"]
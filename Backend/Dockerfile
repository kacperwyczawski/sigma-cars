# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app SigmaCars.WebApi/*.csproj

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime

WORKDIR /app
COPY --from=build-env /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "SigmaCars.WebApi.dll"]
FROM mcr.microsoft.com/dotnet/sdk:7.0 as builder
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app SigmaCars.WebApi/*.csproj

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runner
WORKDIR /app
COPY --from=builder /app .
EXPOSE 5000
ENTRYPOINT ["dotnet", "SigmaCars.WebApi.dll", "--urls", "http://+:5000"]
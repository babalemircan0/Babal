# 1. Aşama: Derleme (Build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Projedeki tüm dosyaları kopyala
COPY . .

# Proje dosyasını arayıp bul ve paketleri geri yükle (Restore)
RUN find . -name "*.csproj" -exec dotnet restore {} \;

# Projeyi derle ve 'publish' klasörüne çıkar
RUN find . -name "*.csproj" -exec dotnet publish {} -c Release -o /app/publish \;

# 2. Aşama: Çalıştırma (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Port ve başlangıç komutu
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# 'Babal.dll' dosyasını bulup çalıştırır
ENTRYPOINT ["dotnet", "Babal.dll"]

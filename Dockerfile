 # 1. Aşama: Uygulamayı Derleme
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Proje dosyasını kopyala ve paketleri indir
COPY ["Babal.csproj", "./"]
RUN dotnet restore "Babal.csproj"

# Tüm dosyaları kopyala ve yayınla
COPY . .
RUN dotnet publish "Babal.csproj" -c Release -o /app/publish

# 2. Aşama: Uygulamayı Çalıştırma
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Render'ın portuyla uyumlu hale getir
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Babal.dll"]

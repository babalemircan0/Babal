# 1. Aşama: Uygulamayı Derleme
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Tüm dosyaları kopyala (Klasör yapısını korumak için)
COPY . .

# .csproj dosyasını bul ve restore et
RUN dotnet restore "Babal.csproj" || dotnet restore "Babal/Babal.csproj"

# Yayınla (Dosya nerede olursa olsun bulup derler)
RUN dotnet publish -c Release -o /app/publish

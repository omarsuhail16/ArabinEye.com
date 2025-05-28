# مرحلة البناء
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore "./ByArabianEye.csproj"
RUN dotnet publish "./ByArabianEye.csproj" -c Release -o out

# مرحلة التشغيل
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "ByArabianEye.dll"]
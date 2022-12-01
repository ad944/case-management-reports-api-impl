FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["case-management-reports/case-management-reports.csproj", "case-management-reports/"]
RUN dotnet restore "case-management-reports/case-management-reports.csproj"
COPY . .
WORKDIR "/src/case-management-reports"
RUN dotnet build "case-management-reports.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "case-management-reports.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet case-management-reports.dll
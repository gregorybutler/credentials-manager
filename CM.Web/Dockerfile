FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CM.Web/CM.Web.csproj", "CM.Web/"]
RUN dotnet restore "CM.Web/CM.Web.csproj"
COPY . .
WORKDIR "/src/CM.Web"
RUN dotnet build "CM.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CM.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CM.Web.dll"]

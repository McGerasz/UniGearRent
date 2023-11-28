FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

ENV ASPNETCORE_URLS=http://+:80
WORKDIR /UniGearRentAPI
COPY UniGearRentAPI.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /UniGearRentAPI
COPY --from=build-env /UniGearRentAPI/out .
ENTRYPOINT ["dotnet", "SolarWatch.dll"]
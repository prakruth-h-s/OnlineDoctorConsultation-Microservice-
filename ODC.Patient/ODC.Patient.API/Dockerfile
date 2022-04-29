#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ODC.Patient.API/ODC.Patient.API.csproj", "ODC.Patient.API/"]
RUN dotnet restore "ODC.Patient.API/ODC.Patient.API.csproj"
COPY . .
WORKDIR "/src/ODC.Patient.API"
RUN dotnet build "ODC.Patient.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ODC.Patient.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ODC.Patient.API.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ODC.Doctor.API/ODC.Doctor.API.csproj", "ODC.Doctor.API/"]
RUN dotnet restore "ODC.Doctor.API/ODC.Doctor.API.csproj"
COPY . .
WORKDIR "/src/ODC.Doctor.API"
RUN dotnet build "ODC.Doctor.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ODC.Doctor.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ODC.Doctor.API.dll"]
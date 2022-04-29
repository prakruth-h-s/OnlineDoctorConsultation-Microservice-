#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ODC.Appointment.API/ODC.Appointment.API.csproj", "ODC.Appointment.API/"]
RUN dotnet restore "ODC.Appointment.API/ODC.Appointment.API.csproj"
COPY . .
WORKDIR "/src/ODC.Appointment.API"
RUN dotnet build "ODC.Appointment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ODC.Appointment.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ODC.Appointment.API.dll"]
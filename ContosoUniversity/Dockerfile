FROM microsoft/aspnetcore:2.0-jessie AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-jessie AS build
WORKDIR /src
COPY ContosoUniversity/ContosoUniversity.csproj ContosoUniversity/
WORKDIR /src/ContosoUniversity
RUN dotnet restore
COPY ContosoUniversity/ .
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ContosoUniversity.dll"]

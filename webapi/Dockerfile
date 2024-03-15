FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

COPY ./*.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /
COPY --from=build-env /out .

EXPOSE 80

ENTRYPOINT ["dotnet", "webapi.dll"]


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
COPY src/QuizForms ./
RUN dotnet restore
RUN dotnet publish -c Release -o out
COPY data ./data

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
COPY data ./data
ENTRYPOINT ["dotnet", "QuizForms.Web.dll"]
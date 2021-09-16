FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AwsDemo.App/AwsDemo.App.csproj", "AwsDemo.App/"]
COPY ["AwsDemo.Bll.Contracts/AwsDemo.Bll.Contracts.csproj", "AwsDemo.Bll.Contracts/"]
COPY ["AwsDemo.Bll/AwsDemo.Bll.csproj", "AwsDemo.Bll/"]
COPY ["AwsDemo.Dal/AwsDemo.Dal.csproj", "AwsDemo.Dal/"]
RUN dotnet restore "AwsDemo.App/AwsDemo.App.csproj"
COPY . .
WORKDIR "/src/AwsDemo.App"
RUN dotnet build "AwsDemo.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AwsDemo.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AwsDemo.App.dll"]
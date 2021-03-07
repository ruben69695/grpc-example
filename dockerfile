ARG sdkTag=5.0-buster-slim
ARG runtimeTag=5.0-buster-slim
ARG image=mcr.microsoft.com/dotnet/aspnet
ARG sdkImage=mcr.microsoft.com/dotnet/sdk

FROM ${image}:${runtimeTag} AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM ${sdkImage}:${sdkTag} AS build

ARG DEPLOYMENT_ENVIRONMENT

WORKDIR /src
COPY ["Server/Server.csproj", "Server/"]

RUN dotnet restore "Server/Server.csproj"

COPY . .
WORKDIR "/src/Server"
RUN dotnet build "Server.csproj" -c $DEPLOYMENT_ENVIRONMENT -o /app/build

FROM build AS publish
WORKDIR /src/Server
ARG DEPLOYMENT_ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT=$DEPLOYMENT_ENVIRONMENT
RUN dotnet publish -c $DEPLOYMENT_ENVIRONMENT -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.dll"]
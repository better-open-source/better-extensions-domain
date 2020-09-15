ARG DotnetVersion=3.1
FROM mcr.microsoft.com/dotnet/core/sdk:$DotnetVersion-alpine AS build
WORKDIR /app

COPY ./BetterExtensions.Domain.sln ./
COPY ./src/BetterExtensions.Domain/BetterExtensions.Domain.csproj ./src/BetterExtensions.Domain/
COPY ./tests/BetterExtensions.Domain.Tests/BetterExtensions.Domain.Tests.csproj ./tests/BetterExtensions.Domain.Tests/

RUN dotnet restore ./src/BetterExtensions.Domain/BetterExtensions.Domain.csproj
RUN dotnet restore ./tests/BetterExtensions.Domain.Tests/BetterExtensions.Domain.Tests.csproj

COPY . ./

ARG CI_BUILDID
ARG CI_PRERELEASE

ENV CI_BUILDID ${CI_BUILDID}
ENV CI_PRERELEASE ${CI_PRERELEASE}

RUN dotnet build ./src/BetterExtensions.Domain/BetterExtensions.Domain.csproj -c Release --no-restore
RUN dotnet build ./tests/BetterExtensions.Domain.Tests/BetterExtensions.Domain.Tests.csproj -c Release --no-restore

RUN dotnet test ./tests/BetterExtensions.Domain.Tests/BetterExtensions.Domain.Tests.csproj -c Release --no-build --no-restore

RUN dotnet pack ./src/BetterExtensions.Domain/BetterExtensions.Domain.csproj -c Release --no-restore --no-build -o /app/out
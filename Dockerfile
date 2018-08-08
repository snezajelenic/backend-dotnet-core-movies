FROM microsoft/dotnet:2.1-sdk-alpine AS builder
ENV IS_DOCKER_ENV=true
WORKDIR /source
COPY . .
RUN dotnet restore
RUN dotnet publish --output /app/ --configuration Release

FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=builder /app .
COPY --from=node /app/build ./wwwroot
ENTRYPOINT ["dotnet", "MoviesBackend.dll"]
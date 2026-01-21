# Use .NET 10 runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["APIWEB.csproj", "."]
RUN dotnet restore

# Copy the rest of the files and build
COPY . .
WORKDIR "/src/."
RUN dotnet build "APIWEB.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "APIWEB.csproj" -c Release -o /app/publish

# Build the final image
FROM base AS final
WORKDIR /app

# Create non-root user for security
RUN addgroup --system app && adduser --system --group app

# Copy published files
COPY --from=publish /app/publish .

# Set ownership and permissions
RUN chown -R app:app /app
USER app

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Start the application
ENTRYPOINT ["dotnet", "APIWEB.dll"]
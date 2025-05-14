# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files
COPY ["Restaurantopia/*.csproj", "Restaurantopia/"]
RUN dotnet restore "Restaurantopia/Restaurantopia.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/Restaurantopia"

# Build the application
RUN dotnet build "Restaurantopia.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Restaurantopia.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy the published files from the publish stage
COPY --from=publish /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "Restaurantopia.dll"]
# Step 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application files
COPY . ./

# Publish the application in Release mode to the 'out' folder
RUN dotnet publish -c Release -o out

# Step 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory in the runtime container
WORKDIR /app

# Copy the build output from the previous step
COPY --from=build /app/out ./

# Copy the SQLite database file into the image
COPY leave.db ./leave.db

# Expose the port your app listens on (match this with your Program.cs or launch settings)
EXPOSE 5006

# Set the startup command to run the application
ENTRYPOINT ["dotnet", "LeaveManagement.dll"]

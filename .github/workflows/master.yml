name: C# Build and Syntax Check
on:
  push:
    branches: ["master"]
  pull_request:
    branches: ["master"]

jobs:
  build:
    runs-on: ubuntu-latest # Uses a VM runner

    steps:
      - name: Checkout Code #Clones the repo
        uses: actions/checkout@v4

      #Setup SDK.NET
      - name: Setup SDK
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '9.0' # Changes to framework used

 #Restore Dependancies
      - name: Restore Dependancies
        run: dotnet restore 
        
      #Run Unit Tests
      - name: Unit Tests
        run: dotnet test

      #Build the Project
      - name: Build
        run: dotnet build --configuration Release

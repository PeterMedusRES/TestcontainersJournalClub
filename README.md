# Testcontainers Software Journal Club code

This repository contains the example code I presented in the Testcontainers Software Journal Club session. It uses an [Azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite) Docker container created using the [Testcontainers](https://testcontainers.com/) library to emulate [Azure Blob Storage](https://azure.microsoft.com/en-gb/products/storage/blobs). This repository is a C# solution that can be opened using Visual Studio or any other C# IDE.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) or later
- A C# IDE, such as [Visual Studio](https://visualstudio.microsoft.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- (optional) [Azure Storage Explorer](https://azure.microsoft.com/en-gb/products/storage/storage-explorer) to inspect the contents of the Azurite container while debugging.

## Building and Running the Tests

The integration tests can be found the `AlbumCoverServiceTests` file in the `Albums.Test` project. You can use the test runner in your IDE, or run `dotnet test` from the command line. Docker must be running in the background for the tests to work.

## Git Branches

- `0-start` is the starting point for the Journal Club session.
- `1-containerbuilder` uses the `ContainerBuilder` class to create the Azurite container.
- `2-azurite-module` uses the Azurite Testcontainers module to create the Azurite container, as well as making the code debuggable.
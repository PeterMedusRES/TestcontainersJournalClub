using Azure.Storage.Blobs;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Albums.Test;

public class AzuriteContainerTestHelper
{
    private const string ConnectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;";

    private readonly IContainer _container = new ContainerBuilder()
        .WithImage("mcr.microsoft.com/azure-storage/azurite:latest")
        .WithEntrypoint("azurite")
        .WithCommand("--blobHost", "0.0.0.0")
        .WithPortBinding(10000, 10000)
        .Build();

    public async Task StartAsync()
    {
        await _container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }

    public BlobContainerClient GetBlobContainerClient()
    {
        var blobServiceClient = new BlobServiceClient(ConnectionString);
        return blobServiceClient.GetBlobContainerClient("albums");
    }

    public async Task CreateBlobContainer()
    {
        var blobContainerClient = GetBlobContainerClient();
        await blobContainerClient.CreateIfNotExistsAsync();
    }

    public async Task DeleteBlobContainer()
    {
        var blobContainerClient = GetBlobContainerClient();
        await blobContainerClient.DeleteAsync();
    }
}
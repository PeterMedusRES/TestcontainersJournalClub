using Azure.Storage.Blobs;
using Testcontainers.Azurite;

namespace Albums.Test;

public class AzuriteContainerTestHelper
{
    private readonly AzuriteContainer _container = BuildContainer();

    private static AzuriteContainer BuildContainer()
    {
        var builder = new AzuriteBuilder();

        if (System.Diagnostics.Debugger.IsAttached)
        {
            builder = builder.WithPortBinding(10000, 10000);
        }

        return builder.Build();
    }

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
        var blobServiceClient = new BlobServiceClient(_container.GetConnectionString());
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
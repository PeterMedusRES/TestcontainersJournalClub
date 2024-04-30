using Azure.Storage.Blobs;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Albums.Test;

public class AzuriteContainerTestHelper
{
    private const string ConnectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;";

    // private readonly IContainer _container = TODO;

    public async Task StartAsync()
    {
        throw new NotImplementedException();
    }

    public async Task DisposeAsync()
    {
        throw new NotImplementedException();
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
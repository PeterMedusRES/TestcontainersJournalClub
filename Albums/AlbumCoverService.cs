using Azure.Storage.Blobs;

namespace Albums;

public class AlbumCoverService
{
    private readonly BlobContainerClient _blobContainerClient;

    public AlbumCoverService(BlobContainerClient blobContainerClient)
    {
        _blobContainerClient = blobContainerClient;
    }

    public async Task<List<string>> GetAlbumCoversAsync()
    {
        var albumCovers = new List<string>();
        
        await foreach (var albumCover in _blobContainerClient.GetBlobsAsync())
        {
            albumCovers.Add(albumCover.Name);
        }
        
        return albumCovers;
    }
    
    public async Task UploadAlbumCoverAsync(string filename, Stream fileStream)
    {
        await _blobContainerClient.UploadBlobAsync(filename, fileStream);
    }
}
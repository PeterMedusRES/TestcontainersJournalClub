using FluentAssertions;
using NUnit.Framework;

namespace Albums.Test;

public class AlbumCoverServiceTests
{
    private readonly AzuriteContainerTestHelper _testHelper = new();

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await _testHelper.StartAsync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _testHelper.DisposeAsync();
    }

    [SetUp]
    public async Task SetUp()
    {
        await _testHelper.CreateBlobContainer();
    }

    [TearDown]
    public async Task TearDown()
    {
        await _testHelper.DeleteBlobContainer();
    }

    [Test]
    public async Task TestNoUploads()
    {
        var albumCoverService = new AlbumCoverService(_testHelper.GetBlobContainerClient());

        var albumCovers = await albumCoverService.GetAlbumCoversAsync();

        albumCovers.Should().BeEmpty();
    }

    [TestCase("DarkSideOfTheMoon.png")]
    [TestCase("TheNewAbnormal.png")]
    public async Task TestUploadSingleAlbumCover(string albumCover)
    {
        var albumCoverService = new AlbumCoverService(_testHelper.GetBlobContainerClient());

        var path = $"AlbumCovers/{albumCover}";
        var fileStream = new MemoryStream(await File.ReadAllBytesAsync(path));
        await albumCoverService.UploadAlbumCoverAsync(Path.GetFileName(path), fileStream);

        var albumCovers = await albumCoverService.GetAlbumCoversAsync();

        albumCovers.Should().ContainSingle().Which.Should().Be(albumCover);
    }

    [Test]
    public async Task TestUploadMultipleAlbumCovers()
    {
        var albumCoverService = new AlbumCoverService(_testHelper.GetBlobContainerClient());

        var paths = Directory.GetFiles("AlbumCovers", "*.png");
        foreach (var path in paths)
        {
            var fileStream = new MemoryStream(await File.ReadAllBytesAsync(path));
            await albumCoverService.UploadAlbumCoverAsync(Path.GetFileName(path), fileStream);
        }

        var albumCovers = await albumCoverService.GetAlbumCoversAsync();

        albumCovers.Should().BeEquivalentTo(["DarkSideOfTheMoon.png", "TheNewAbnormal.png", "TitanicRising.png"]);
    }
}
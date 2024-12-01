namespace SixLabors.ImageSharp.Dds.Tests;

// TODO: create comprehensive tests

public class Tests
{
    [Test]
    public void FullTest()
    {
        // Test loading
        using var file = new FileStream("TestImage.dds", FileMode.Open);
        var dds = DdsTexture.Load(file);

        // Test saving
        var stream = new FileStream("TestOutputImage.dds", FileMode.OpenOrCreate);
        dds.Save(stream);
    }
}

using System.Diagnostics;
using SixLabors.ImageSharp.PixelFormats;

namespace SixLabors.ImageSharp.Dds;

public class DdsTexture(DdsHeader header, DdsHeaderDxt10? dxt10Header)
{
    public DdsHeader Header = header;
    public DdsHeaderDxt10? Dxt10Header = dxt10Header;
    // TODO: add data

    public static DdsTexture Load(Stream stream)
    {
        var reader = new BinaryReader(stream);

        // Validate magic number
        if (new string(reader.ReadChars(4)) != "DDS ")
            throw new Exception("Not a DDS file");

        // Read header data
        var header = DdsHeader.Read(reader);
        bool hasDxt10Header = header.PixelFormat.Flags.HasFlag(DdsPixelFormatFlags.FourCC) && header.PixelFormat.FourCC == FourCCValues.Dxt10;
        DdsHeaderDxt10? dxt10Header = hasDxt10Header ? DdsHeaderDxt10.Read(reader) : null;

        // TODO: properly handle all data formats
        LoadData(reader, header);

        // Calculating data length (for testing)
        uint dataLength = header.Width * header.Height;
        // Mipmaps
        dataLength += (uint)(header.Width * header.Height / 4);
        dataLength += (uint)(header.Width * header.Height / 16);
        dataLength += (uint)(header.Width * header.Height / 64);
        dataLength += (uint)(header.Width * header.Height / 256);
        dataLength *= 4; // 4 bytes per pixel
        long realDataLength = stream.Length - stream.Position;

        return new DdsTexture(header, dxt10Header);
    }

    private static void LoadData(BinaryReader reader, DdsHeader header)
    {
        // Each byte is a component of a pixel in RGBA
        var image = new Image<Rgba32>((int)header.Width, (int)header.Height);

        for (int y = 0; y < header.Height; y++)
        {
            for (int x = 0; x < header.Width; x++)
            {
                uint pixel = reader.ReadUInt32();
                byte a = (byte)((pixel & header.PixelFormat.ABitMask) >> (8 * 3));
                byte r = (byte)((pixel & header.PixelFormat.RBitMask) >> (8 * 2));
                byte g = (byte)((pixel & header.PixelFormat.GBitMask) >> (8 * 1));
                byte b = (byte)((pixel & header.PixelFormat.BBitMask) >> (8 * 0));
                image[x, y] = new Rgba32(r, g, b, a);
            }
        }

        image.Save("TestOutput.png");
    }

    public void Save(Stream stream)
    {
        var writer = new BinaryWriter(stream);

        Header.Write(writer);
        Dxt10Header?.Write(writer);

        // TODO: finish the encoder
    }
}

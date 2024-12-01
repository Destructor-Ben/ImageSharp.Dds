// ReSharper disable PropertyCanBeMadeInitOnly.Global

using System.Diagnostics;

namespace SixLabors.ImageSharp.Dds;

public record struct DdsHeader
{
    public const int BytesSize = 124;

    public DdsHeaderFlags Flags { get; set; }
    public uint Height { get; set; }
    public uint Width { get; set; }
    public uint PitchOrLinearSize { get; set; }
    public uint Depth { get; set; }
    public uint MipMapCount { get; set; }
    public DdsPixelFormat PixelFormat { get; set; }
    // TODO: these 2 are enums
    public uint Caps { get; set; }
    public uint Caps2 { get; set; }

    public static DdsHeader Read(BinaryReader reader)
    {
        long startPos = reader.BaseStream.Position;

        Debug.Assert(reader.ReadUInt32() == BytesSize);
        var flags = (DdsHeaderFlags)reader.ReadUInt32();
        uint height = reader.ReadUInt32();
        uint width = reader.ReadUInt32();
        uint pitch = reader.ReadUInt32();
        uint depth = reader.ReadUInt32();
        uint mipMapCount = reader.ReadUInt32();
        reader.SkipBytes(UIntByteSize * 11); // dwReserved1
        var pixelFormat = DdsPixelFormat.Read(reader);
        uint caps = reader.ReadUInt32();
        uint caps2 = reader.ReadUInt32();
        reader.SkipBytes(UIntByteSize * 3); // dwCaps3, dwCaps4, dwReserved2

        // By now, we should be 124 bytes past the start
        long currentPos = reader.BaseStream.Position;
        Debug.Assert(currentPos - startPos == BytesSize);

        return new DdsHeader
        {
            Flags = flags,
            Height = height,
            Width = width,
            PitchOrLinearSize = pitch,
            Depth = depth,
            MipMapCount = mipMapCount,
            PixelFormat = pixelFormat,
            Caps = caps,
            Caps2 = caps2,
        };
    }

    public readonly void Write(BinaryWriter writer)
    {
        long startPos = writer.BaseStream.Position;

        writer.Write((uint)BytesSize);
        writer.Write((uint)Flags);
        writer.Write(Height);
        writer.Write(Width);
        writer.Write(PitchOrLinearSize);
        writer.Write(Depth);
        writer.Write(MipMapCount);
        writer.WriteEmptyBytes(UIntByteSize * 11); // dwReserved1
        PixelFormat.Write(writer);
        writer.Write(Caps);
        writer.Write(Caps2);
        writer.WriteEmptyBytes(UIntByteSize * 3); // dwCaps3, dwCaps4, dwReserved2

        // By now, we should be 124 bytes past the start
        long currentPos = writer.BaseStream.Position;
        Debug.Assert(currentPos - startPos == BytesSize);
    }
}

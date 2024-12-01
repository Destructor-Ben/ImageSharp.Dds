// ReSharper disable PropertyCanBeMadeInitOnly.Global

using System.Diagnostics;

namespace SixLabors.ImageSharp.Dds;

public record struct DdsPixelFormat
{
    public const int BytesSize = 32;

    public DdsPixelFormatFlags Flags { get; set; }
    public uint FourCC { get; set; } // 4 character code (FourCC) for custom pixel formats, e.g. DXT4
    public uint RgbBitCount { get; set; }
    public uint RBitMask { get; set; }
    public uint BBitMask { get; set; }
    public uint GBitMask { get; set; }
    public uint ABitMask { get; set; }

    public static DdsPixelFormat Read(BinaryReader reader)
    {
        long startPos = reader.BaseStream.Position;

        Debug.Assert(reader.ReadUInt32() == BytesSize);
        var flags = (DdsPixelFormatFlags)reader.ReadUInt32();
        uint fourCC = reader.ReadUInt32();
        uint rgbBitCount = reader.ReadUInt32();
        uint rBitMask = reader.ReadUInt32();
        uint gBitMask = reader.ReadUInt32();
        uint bBitMask = reader.ReadUInt32();
        uint aBitMask = reader.ReadUInt32();

        // By now, we should be 32 bytes past the start
        long currentPos = reader.BaseStream.Position;
        Debug.Assert(currentPos - startPos == BytesSize);

        return new DdsPixelFormat
        {
            Flags = flags,
            FourCC = fourCC,
            RgbBitCount = rgbBitCount,
            RBitMask = rBitMask,
            GBitMask = gBitMask,
            BBitMask = bBitMask,
            ABitMask = aBitMask,
        };
    }

    public readonly void Write(BinaryWriter writer)
    {
        long startPos = writer.BaseStream.Position;

        writer.Write((uint)BytesSize);
        writer.Write((uint)Flags);
        writer.Write(FourCC);
        writer.Write(RgbBitCount);
        writer.Write(RBitMask);
        writer.Write(GBitMask);
        writer.Write(BBitMask);
        writer.Write(ABitMask);

        // By now, we should be 32 bytes past the start
        long currentPos = writer.BaseStream.Position;
        Debug.Assert(currentPos - startPos == BytesSize);
    }
}

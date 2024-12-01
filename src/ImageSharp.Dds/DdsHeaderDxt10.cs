using System.Diagnostics;

namespace SixLabors.ImageSharp.Dds;

public record struct DdsHeaderDxt10
{
    public const int BytesSize = 20;

    public static DdsHeaderDxt10 Read(BinaryReader reader)
    {
        long startPos = reader.BaseStream.Position;

        // TODO: read

        // By now, we should be 20 bytes past the start
        long currentPos = reader.BaseStream.Position;
        Debug.Assert(currentPos - startPos == BytesSize);

        return new DdsHeaderDxt10();
    }

    public readonly void Write(BinaryWriter writer)
    {
        long startPos = writer.BaseStream.Position;

        // TODO: write

        // By now, we should be 20 bytes past the start
        long currentPos = writer.BaseStream.Position;
        Debug.Assert(currentPos - startPos == BytesSize);
    }
}

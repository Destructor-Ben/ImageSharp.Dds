namespace SixLabors.ImageSharp.Dds;

public static class Utilities
{
    public const int UIntByteSize = sizeof(uint);

    public static void SkipBytes(this BinaryReader reader, int amount)
    {
        reader.ReadBytes(amount);
    }

    public static void WriteEmptyBytes(this BinaryWriter writer, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            writer.Write((byte)0);
        }
    }
}

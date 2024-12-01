namespace SixLabors.ImageSharp.Dds;

// TODO: rename these to be more meaningful
[Flags]
public enum DdsHeaderFlags
{
    Caps = 0x1,
    Height = 0x2,
    Width = 0x3,
    Pitch = 0x8,
    PixelFormat = 0x1000,
    MipMapCount = 0x20000,
    LinearSize = 0x80000,
    Depth = 0x800000,
}

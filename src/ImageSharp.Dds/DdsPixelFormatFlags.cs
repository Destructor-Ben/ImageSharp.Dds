namespace SixLabors.ImageSharp.Dds;

// TODO: rename these to be more meaningful
[Flags]
public enum DdsPixelFormatFlags
{
    AlphaPixels = 0x1,
    Alpha = 0x2,
    FourCC = 0x4,
    Rgb = 0x40,
    Yuv = 0x200,
    Luminance = 0x20000,
}

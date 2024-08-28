using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace IntegrationTests.Helpers;

internal class PhotoGenerator
{
    public byte[] Generate(int width, int height)
    {
        using var image = new Image<Rgba32>(width, height);

        var color = new Rgba32(100, 100, 100, 255);

        image.Mutate(ctx => ctx.BackgroundColor(color));

        using var stream = new MemoryStream();

        image.SaveAsJpeg(stream);

        return stream.ToArray();
    }
}
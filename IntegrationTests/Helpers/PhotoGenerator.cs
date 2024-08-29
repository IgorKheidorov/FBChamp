using System.Drawing;
using System.Drawing.Imaging;
using Color = System.Drawing.Color;

namespace IntegrationTests.Helpers;

internal class PhotoGenerator
{
    public byte[] Generate(int width, int height)
    {
        using var bitmap = new Bitmap(width, height);
        using var graphics = Graphics.FromImage(bitmap);

        graphics.Clear(Color.White);

        using var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Jpeg);

        return stream.ToArray();
    }
}
using System.Drawing;

namespace FBChamp.Infrastructure.Validators;

internal class PhotoValidator
{
    public bool Validate(byte[] photo, int widthSize, int heightSize)
    {
        if (photo is null || photo.Length == 0)
        {
            return false;
        }

        try
        {
            using var photoStream = new MemoryStream(photo);

            // Suppressing CA1416 warning: Image.Size is a platform-specific API available only on Windows.
#pragma warning disable CA1416
            using var image = Image.FromStream(photoStream);

            return image.Size.Width == widthSize && image.Size.Height == heightSize;
#pragma warning restore CA1416
        }
        catch (Exception)
        {
            return false;
        }
    }
}
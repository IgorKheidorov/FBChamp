namespace FBChamp.Web.Common.Helpers;

public static class ImageHelper
{
    public static byte[] GetByteImage(this IFormFile photoFile) 
    {
        byte[] photo = null; 
        if (photoFile != null && photoFile.Length > 0)
        {
            var stream = new MemoryStream();
            photoFile.CopyToAsync(stream).Wait();
            photo = stream.ToArray(); ;
        }

        return photo;
    }
}

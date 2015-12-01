using System;
using System.IO;

namespace TradeYourPhone.Core.Utilities
{
    public static class ImageManager
    {
        public static byte[] CompressImage(object imageStream, string height)
        {
            Stream outputStream = new MemoryStream();
            ImageResizer.ImageJob j = new ImageResizer.ImageJob(imageStream, outputStream, new ImageResizer.ResizeSettings(
                        string.Format("height={0};format=jpg;mode=max", height)));
            j.Build();

            outputStream.Position = 0;
            using (var binaryReader = new BinaryReader(outputStream))
            {
                return binaryReader.ReadBytes(Convert.ToInt32(outputStream.Length));
            }
        }
    }
}

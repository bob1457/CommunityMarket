using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using Ninject.Activation;


namespace CommonMarket.Infrastructure.Utilities
{
    public static class ImageProcessor
    {
        public static void SaveResizedImage(string filePath, string fileName, string resizedFileName, int width, int height) //resize by fixed dimenstion
        {
            Image origImg = Image.FromFile(Path.Combine(filePath, fileName));

            Image resizedImg = FixedSize(origImg, width, height);


            string fileExt = FileProcessor.GetFileExtension(fileName);


            switch (fileExt)
            {
                case ".png":
                    resizedImg.Save(Path.Combine(filePath, resizedFileName), ImageFormat.Png);
                    break;
                case ".jpg":
                    resizedImg.Save(Path.Combine(filePath, resizedFileName), ImageFormat.Jpeg);
                    break;
                case ".jepg":
                    resizedImg.Save(Path.Combine(filePath, resizedFileName), ImageFormat.Jpeg);
                    break;
                case ".gif":
                    resizedImg.Save(Path.Combine(filePath, resizedFileName), ImageFormat.Gif);
                    break;
                default:
                    break;


            }
            
            resizedImg.Dispose();
            origImg.Dispose();

        }

        public static void SaveResizedImage(string filePath, string fileName, string resizedFileName, int width) //resize to keep aspect ratio by specifying width
        {
            Image origImg = Image.FromFile(Path.Combine(filePath, fileName));

            //Image resizedImg = FixedSize(origImg, widith, height);

            Image resizedImg = ScaleImageByWidth(origImg, width);

            //resizedImg.Save(Path.Combine(filePath, resizedFileName), ImageFormat.Jpeg);
            resizedImg.Save(Path.Combine(filePath, resizedFileName));
            resizedImg.Dispose();
            origImg.Dispose();

        }


        private static Image ScaleByPercent(Image imgPhoto, int percent)
        {
            float nPercent = ((float)percent / 100);
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }

        private static Image ScaleImage(Image imgPhoto, int maxWidth, int maxHeight)
        {
            double ratioX = maxWidth / imgPhoto.Width;
            double ratioY = maxHeight / imgPhoto.Height;
            var ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(imgPhoto.Width * ratio);
            int newHeith = (int)(imgPhoto.Height * ratio);

            Bitmap bmPhoto = new Bitmap(newWidth, newHeith);
            Graphics.FromImage(bmPhoto).DrawImage(imgPhoto, 0, 0, newWidth, newHeith);

            return bmPhoto;
        }

        private static Image ScaleImageByWidth(Image imgPhoto, int maxWidth)
        {
            double ratio = ((double)maxWidth / imgPhoto.Width);
            //double ratioY = maxHeight / imgPhoto.Height;
            //var ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(imgPhoto.Width * ratio);
            int newHeight = (int)(imgPhoto.Height * ratio);

            Bitmap bmPhoto = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(bmPhoto).DrawImage(imgPhoto, 0, 0, newWidth, newHeight);

            return bmPhoto;
        }

        private static Image FixedSize(Image imgPhoto, int width, int height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((height - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.CompositingMode = CompositingMode.SourceCopy;
            grPhoto.CompositingQuality = CompositingQuality.HighQuality;
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            grPhoto.Dispose();

            bmPhoto.MakeTransparent();

            return bmPhoto;
        }

        
    }
}

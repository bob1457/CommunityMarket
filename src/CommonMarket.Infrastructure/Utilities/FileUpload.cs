using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;



namespace CommonMarket.Infrastructure.Utilities
{
    public class FileUpload
    {
        public void Upload(HttpPostedFile myFile, string destinationPath)//, int s_fileWidth, int s_fileHeight, string destinationFileName)
        {
            if (myFile != null)
            {
                string fileName = myFile.FileName;
                string fileExtenstion = FileProcessor.GetFileExtension(fileName);

                if (myFile.ContentLength > 0 && myFile.ContentLength < 10 * 1024 * 1024)
                {
                    if (fileExtenstion == ".jpg" || fileExtenstion == ".jepg" || fileExtenstion == ".png" ||
                        fileExtenstion == ".gif")
                    {
                        //~/Content/Assets/Images/
                        //string path = Path.Combine(HttpContext.Current.Server.MapPath(destinationPath), Path.GetFileName(myFile.FileName));

                        //string picPath = Path.Combine(destinationPath, myFile.FileName);

                        ////string extension = FileProcessor.GetFileExtension(file.FileName);

                        //string purePath = HttpContext.Current.Server.MapPath(destinationPath);

                        myFile.SaveAs(destinationPath);

                        //Image img = Image.FromFile(path);

                        //string newFileName = user.UserProfile.FirstName + "_" + user.UserProfile.LastName +
                        //                     fileExtenstion;

                        //if (img.Width > s_fileWidth || img.Height > s_fileHeight)
                        //{
                        //    //resize image
                        //    //

                        //    ImageProcessor.SaveResizedImage(purePath, fileName, destinationFileName, s_fileWidth, s_fileHeight);
                        //}

                        //if (img.Width > l_fileWidth || img.Height > l_fileHeight)
                        //{
                        //    //resize image
                        //    //

                        //    ImageProcessor.SaveResizedImage(purePath, fileName, destinationFileName, l_fileWidth, l_fileHeight);
                        //}
                    }

                }
            }
        }
    }
}

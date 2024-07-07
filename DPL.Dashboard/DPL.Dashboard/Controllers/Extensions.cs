using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace DPL.DASHBOARD
{
    public static class Extensions
    {

        public static byte[] ToByteArray(this Image image)
        {
            if (image == null) return new byte[0];
            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static byte[] ToByteArray(this object image)
        {
            var imageByte = image as byte[];
            if (imageByte == null || imageByte.Length <= 0)
                return new byte[0];

            return imageByte;
        }

     
       
           

    }
}
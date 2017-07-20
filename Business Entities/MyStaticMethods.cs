using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public static class MyStaticMethods
    {
        public static byte[] ConvertImageBinary( string dataImage)
        {
            return Encoding.ASCII.GetBytes(dataImage);
        }
        public static string ConvertImageString(byte[] dataImage)
        {
           // byte[] imageByteData = dataImage;
           // string imageBase64Data = Convert.ToBase64String(imageByteData);
            //string imageDataURL = string.Format("data:image/jpeg;base64,{0}", imageBase64Data);
            return Convert.ToBase64String(dataImage);

        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SecuritasEngineering.Web.Utility
{
    public static class CommonHelper
    {
        public static string ToUniqueFileName(this HttpPostedFileBase ImageFile)
        {
            if (ImageFile is null) throw new ArgumentNullException(nameof(ImageFile));

            string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            return fileName + DateTime.Now.ToString("yymmssfff") + extension;
        }
    }
}
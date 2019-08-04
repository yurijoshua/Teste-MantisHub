﻿using System;
using System.IO;
using System.Reflection;

namespace TesteBase2
{
    class PathDriverGet
    {
        public static String PathDriver()
        {
            String strAppDir = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);

            var gparent = Directory.GetParent(Directory.GetParent(strAppDir).ToString());

            String aux = gparent.ToString();
 
            String strAppFolderData = String.Concat(aux, "\\Drivers");

            return strAppFolderData;
        }
    }
}
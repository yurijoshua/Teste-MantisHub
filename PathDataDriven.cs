using System;
using System.IO;
using System.Reflection;

namespace TesteBase2
{
    class PathDataDrivenGet
    {
        public static String PathDataDriven(string arqv)
        {
            String strAppDir = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);

            var gparent = Directory.GetParent(Directory.GetParent(strAppDir).ToString());

            String aux = gparent.ToString();

            String strAppFolderData = String.Concat(aux, "\\DataDriven\\" + arqv);

            return strAppFolderData;
        }
    }
}
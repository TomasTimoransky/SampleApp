using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Common
{
    public class Helper
    {
        public byte[] ReadFile(string path)
        {
            ////DirectoryInfo directoryInfo = new DirectoryInfo(path);
            ////List<FileInfo> files = directoryInfo.GetFiles().ToList();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            byte[] array = new byte[0];
            byte[] buffer = new byte[100];
            int index = 0;
            while (fileStream.Read(buffer, 0, 100) > 0)
            {
                Array.Resize(ref array, array.Length + 100);
                Array.Copy(buffer, 0, array, index, 100);
                index += 100;
            }

            return array;
        }
    }
}

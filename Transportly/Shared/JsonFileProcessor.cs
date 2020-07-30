using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Transportly.Common;

namespace Transportly.Shared
{
    /// <summary>
    /// Does everything for JSON file
    /// </summary>
    public class JsonFileProcessor : IFileProcessor
    {
        private string FilePath;

        public JsonFileProcessor(string filePath)
        {
            FilePath = filePath;
        }

        public bool Validate()
        {
            return File.Exists(FilePath);
        }

        public string GetFileContent()
        {
            if(Validate())
                return File.ReadAllText(FilePath);

            return null;
        }
    }
}

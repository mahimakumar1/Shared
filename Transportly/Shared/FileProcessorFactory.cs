using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Transportly.Shared;

namespace Transportly.Common
{
    /// <summary>
    /// The factory for File Processor. the current assumption is the files are in json format,
    /// if they can be multiple source then use this factory to get the correct file processor
    /// </summary>
    public static class FileProcessorFactory
    {
        public static IFileProcessor Processor;
        public static IFileProcessor GetFileProcessor(string filePath)
        {
            switch (Path.GetExtension(filePath))
            {
                case ".json":
                    Processor = new JsonFileProcessor(filePath);
                    break;
                case ".csv":
                    throw new NotImplementedException();
            }
            return Processor;
        }
    }
}

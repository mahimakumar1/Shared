using System;
using System.Collections.Generic;
using System.Text;

namespace Transportly.Shared
{
    public interface IFileProcessor
    {
        bool Validate();
        string GetFileContent();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Transportly.Manager
{
    public interface IManager<out T>
    {
        T Import(string content);
    }
}

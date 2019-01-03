using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cvtohtml.Models
{
    public interface IParseService
    {
        bool parse(string fileLocation);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatFacts.Responses
{
    public class CatFactApiResponse
    {
        public required string fact { get; set; }
        public int length { get; set; }
    }
}

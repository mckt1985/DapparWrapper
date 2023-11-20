using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperWrapper.Models
{
    public class ParameterInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public object DefaultValue { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goldentech.Models
{
    public class SpSqlParametersModel
    {
        public string Name { get; set; }
        public dynamic Value { get; set; }
        public bool IsNullable { get; set; }
    }
}

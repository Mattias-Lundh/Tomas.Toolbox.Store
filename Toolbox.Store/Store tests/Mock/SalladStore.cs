using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Store;

namespace Store_tests
{
    public class SalladStore
    {
        public SalladStore()
        {
            Tables = new List<TableEntity>();
        }
        public ICollection<TableEntity> Tables { get; set; }
        public TomatoEntity Tomato { get; set; }
        public BowlEntity Bowl { get; set; }

    }
}

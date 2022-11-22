using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Store;

namespace Store_tests
{
    public class TableEntity : Entity
    {
        public TableEntity()
        {
            Reserves = new List<BowlEntity>();
        }
        public string Name { get; set; }
        public OliveEntity Olive { get; set; }
        public BowlEntity Bowl { get; set; }
        
        public IList<BowlEntity> Reserves { get; set; }
    }
}

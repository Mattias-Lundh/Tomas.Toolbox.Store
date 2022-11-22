using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Store;

namespace Store_tests
{
    public class BowlEntity : Entity
    {        
        public TomatoEntity Tomato { get; set; }
        public OliveEntity Olive { get; set; }
    }
}

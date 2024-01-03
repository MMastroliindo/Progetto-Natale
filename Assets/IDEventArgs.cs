using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class IDEventArgs: EventArgs
    {
        public int ID;

        public IDEventArgs(int ID)
        {
            this.ID = ID;
        }
    }
}

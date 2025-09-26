using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzzzt_ {
    internal class MechanicCard : Card {
        public MechanicCard() : base(0, 0, 0) { }

        public override string ToString() {
            return "Mechanic".PadRight(20) + base.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzzzt_ {
    internal class FzzztCard : Card{
        public FzzztCard() : base(3, -1, 0) { }

        public override string ToString() {
            return "Fzzzt".PadRight(20) + base.ToString();
        }
    }
}

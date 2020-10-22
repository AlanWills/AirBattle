using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBattle.Level
{
    public interface IRoundManager
    {
        void BeginRound();
        void EndRound();
    }
}

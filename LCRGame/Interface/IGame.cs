using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCRGame.Interface
{
    public interface IGame
    {
        List<string> RollDice(int diceCount);
    }
}

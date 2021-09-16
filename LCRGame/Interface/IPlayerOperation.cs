using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCRGame.Interface
{
    internal interface IPlayerOperation
    {
        void ChangeChip(int number);

        void SetChip(int number);

        int GetChip();

        List<string> RollDice();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCRGame.Interface
{
    public interface IPlayer
    {
        void ChangeChip(int number);

        void SetChip(int number);

        int GetChip();

        IPlayer GetLeftPlayer();

        IPlayer GetRightPlayer();

        void SetLeftPlayer(IPlayer leftPlayer);

        void SetRightPlayer(IPlayer rightPlayer);
    }
}

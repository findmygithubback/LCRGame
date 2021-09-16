using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCRGame.Interface
{
    internal interface ITableOperation
    {
        Task StartTable();

        void ClearTable();

        void InitTable(int numPlayer, int totalGames);
    }
}

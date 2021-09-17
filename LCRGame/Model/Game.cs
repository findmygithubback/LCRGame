using LCRGame.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCRGame.Model
{
    public class Game : IGame
    {
        private List<string> dice = new List<string>() { "L", ".", "R", ".", "C", "." };

        public List<string> RollDice(int diceCount)
        {
            if (diceCount == 0)
            {
                return null;
            }
            else
            {
                List<string> result = new List<string>();
                Random random = new Random(DateTime.Now.Millisecond);
                for (int i = 0; i < diceCount; i++)
                {
                    result.Add(dice[random.Next(0, 6)]);
                }

                return result;
            }
        }
    }
}

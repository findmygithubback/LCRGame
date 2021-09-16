using LCRGame.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LCRGame.Model
{
    public class Player: INotifyPropertyChanged, IPlayerOperation
    {
        private List<string> dice = new List<string>() { "L", ".", "R", ".", "C", "." };

        public Player LeftPlayer;
        public Player RightPlayer;

        public Player(int chips, string name)
        {
            totalChips = chips;
            this.name = name;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        private int totalChips;
        public int TotalChips
        {
            get { return totalChips; }
            set
            {
                if (value != totalChips)
                {
                    totalChips = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void ChangeChip(int number)
        {
            if (number >= 0)
            {
                TotalChips += number;
            }
            else
            {
                TotalChips -= Math.Min(totalChips, Math.Abs(number));
            }
        }

        public void SetChip(int number)
        {
            TotalChips = number;
        }

        public int GetChip()
        {
            return totalChips;
        }

        public List<string> RollDice()
        {
            if (totalChips == 0)
            {
                return null;
            }
            else
            {
                int rolls = Math.Min(totalChips, 3);
                List<string> result = new List<string>();
                Random random = new Random(DateTime.Now.Millisecond);
                for (int i = 0; i < rolls; i++)
                {
                    result.Add(dice[random.Next(0, 6)]);
                }

                return result;
            }
        }
    }
}

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
    public class Player: INotifyPropertyChanged, IPlayer
    {
        private IPlayer leftPlayer;
        private IPlayer rightPlayer;

        public Player(string name)
        {
            this.name = name;
        }

        public Player(int chips, string name) : this(name)
        {
            totalChips = chips;
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

        public IPlayer GetLeftPlayer()
        {
            return leftPlayer;
        }

        public IPlayer GetRightPlayer()
        {
            return rightPlayer;
        }

        public void SetLeftPlayer(IPlayer leftPlayer)
        {
            this.leftPlayer = leftPlayer;
        }

        public void SetRightPlayer(IPlayer rightPlayer)
        {
            this.rightPlayer = rightPlayer;
        }
    }
}

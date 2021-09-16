using LCRGame.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LCRGame.Model
{
    public class Table : INotifyPropertyChanged, ITableOperation
    {
        private const int InitChipNumber = 3;

        #region Properties
        private ObservableCollection<Player> players = new ObservableCollection<Player>();
        public ObservableCollection<Player> Players
        {
            get { return players; }
            set
            {
                if (value != players)
                {
                    players = value;
                    OnPropertyChanged();
                }
            }
        }

        private int currentGames;
        public int CurrentGames
        {
            get { return currentGames; }
            set
            {
                if (value != currentGames)
                {
                    currentGames = value;
                    OnPropertyChanged();
                }
            }
        }

        private int totalGames;
        public int TotalGames
        {
            get { return totalGames; }
            set
            {
                if (value != totalGames)
                {
                    totalGames = value;
                    OnPropertyChanged();
                }
            }
        }

        private int chipsOnTable;
        public int ChipsOnTable
        {
            get { return chipsOnTable; }
            set
            {
                if (value != chipsOnTable)
                {
                    chipsOnTable = value;
                    OnPropertyChanged();
                }
            }
        }

        private int totalTurns;
        public int TotalTurns
        {
            get { return totalTurns; }
            set
            {
                if (value != totalTurns)
                {
                    totalTurns = value;
                    OnPropertyChanged();
                }
            }
        }

        private int shortestTurns;
        public int ShortestTurns
        {
            get { return shortestTurns; }
            set
            {
                if (value != shortestTurns)
                {
                    shortestTurns = value;
                    OnPropertyChanged();
                }
            }
        }

        private int longestTurns;
        public int LongestTurns
        {
            get { return longestTurns; }
            set
            {
                if (value != longestTurns)
                {
                    longestTurns = value;
                    OnPropertyChanged();
                }
            }
        }

        private int averageTurns;
        public int AverageTurns
        {
            get { return averageTurns; }
            set
            {
                if (value != averageTurns)
                {
                    averageTurns = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void ResetStatisticData()
        {
            LongestTurns = 0;
            ShortestTurns = 0;
            AverageTurns = 0;
            TotalTurns = 0;
            CurrentGames = 0;
        }

        private void ChangeChip(int number)
        {
            if (number < 0)
            {
                MessageBox.Show("Someone is stealing chip!!!");
            }
            else
            {
                ChipsOnTable += number;
            }
        }

        private void ResetChip()
        {
            ChipsOnTable = 0;
            foreach (var p in Players)
            {
                p.SetChip(InitChipNumber);
            }
        }

        private async Task PlayGame()
        {
            int turns = 0;
            int index = 0;
            int totalPlayers = Players.Count();
            while (true)
            {
                var currentIndex = index % totalPlayers;
                if (Players[currentIndex].TotalChips > 0)
                {
                    turns++;
                    var diceResult = Players[currentIndex].RollDice();

                    var totalLeft = diceResult.Where(x => x == "L").Count();
                    Players[currentIndex].LeftPlayer.ChangeChip(totalLeft);
                    Players[currentIndex].ChangeChip(-totalLeft);

                    var totalRight = diceResult.Where(x => x == "R").Count();
                    Players[currentIndex].RightPlayer.ChangeChip(totalRight);
                    Players[currentIndex].ChangeChip(-totalRight);

                    var totalCenter = diceResult.Where(x => x == "C").Count();
                    ChangeChip(totalCenter);
                    Players[currentIndex].ChangeChip(-totalCenter);
                }

                index++;
                await Task.Delay(2000);

                if (IsGameOver())
                {
                    break;
                }
            }

            TotalTurns += turns;
            if (turns > LongestTurns)
            {
                LongestTurns = turns;
            }

            if (ShortestTurns == 0)
            {
                ShortestTurns = turns;
            }
            else if (turns < ShortestTurns)
            {
                ShortestTurns = turns;
            }
        }

        private bool IsGameOver()
        {
            if (Players.Where(x => x.TotalChips != 0).Count() == 1)
            {
                return true;
            }

            return false;
        }

        public async Task StartTable()
        {
            ResetStatisticData();
            for (int i = 0; i < TotalGames; i++)
            {
                CurrentGames = i + 1;
                ResetChip();
                await PlayGame();

                AverageTurns = TotalTurns / (i + 1);
            }
        }

        public void ClearTable()
        {
            ResetChip();
            ResetStatisticData();
            Players.Clear();
        }

        public void InitTable(int numPlayer, int totalGames)
        {
            if (numPlayer >= 3)
            {
                for (int i = 0; i < numPlayer; i++)
                {
                    Players.Add(new Player(InitChipNumber, (i + 1).ToString()));
                }
            }

            for (int i = 0; i < numPlayer; i++)
            {
                Players[i].LeftPlayer = Players[(i - 1 + numPlayer) % numPlayer];
                Players[i].RightPlayer = Players[(i + 1) % numPlayer];
            }

            TotalGames = totalGames;
        }
    }
}

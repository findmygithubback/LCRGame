using LCRGame.Interface;
using LCRGame.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LCRGame.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            table = new Table(new Game());
            this.SimulateCommand = new DelegateCommand(OnSimulate);
        }

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
                    this.RaisePropertyChanged(nameof(Players));
                }
            }
        }

        private Table table;
        public Table GameTable
        {
            get { return table; }
            set
            {
                if (value != table)
                {
                    table = value;
                    this.RaisePropertyChanged(nameof(GameTable));
                }
            }
        }

        public int numberPlayer;
        public int NumberPlayer
        {
            get { return numberPlayer; }
            set
            {
                if (value != numberPlayer)
                {
                    numberPlayer = value;
                    this.RaisePropertyChanged(nameof(NumberPlayer));
                }
            }
        }

        public int totalGames;
        public int TotalGames
        {
            get { return totalGames; }
            set
            {
                if (value != totalGames)
                {
                    totalGames = value;
                    this.RaisePropertyChanged(nameof(TotalGames));
                }
            }
        }

        public bool isButtonEnabled = true;
        public bool IsButtonEnabled
        {
            get { return isButtonEnabled; }
            set
            {
                if (value != isButtonEnabled)
                {
                    isButtonEnabled = value;
                    this.RaisePropertyChanged(nameof(IsButtonEnabled));
                }
            }
        }
        #endregion

        #region Commands
        public DelegateCommand SimulateCommand { get; private set; }

        private async void OnSimulate()
        {
            if (NumberPlayer < 3 || totalGames <= 0)
            {
                MessageBox.Show("At least 3 players to play 1 game");
            }
            else
            {
                IsButtonEnabled = false;

                for (int i = 0; i < NumberPlayer; i++)
                {
                    Players.Add(new Player((i + 1).ToString()));
                }

                table.ClearTable();
                table.InitTable(players.Cast<IPlayer>().ToList(), TotalGames);
                await table.StartTable();
                IsButtonEnabled = true;
            }
        }
        #endregion
    }
}

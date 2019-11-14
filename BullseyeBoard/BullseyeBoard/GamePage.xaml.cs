using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BullseyeBoard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        private int teamAScore;
        private int teamBScore;

        public GamePage()
        {
            InitializeComponent();

            teamAScore = Preferences.Get("startingGameScore", 0);
            teamBScore = Preferences.Get("startingGameScore", 0);

            currentTeamLabel.Text = Preferences.Get("TeamOneName","Team 1");

        }
    }
}
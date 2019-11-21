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

        private string currentTeam = "Team 1";
        private int throwValue = 0;

        private string team1NameSetting = "TeamOneName";
        private string team2NameSetting = "TeamTwoName";

        private int currentRound = 1;
        private int counter = 1;


        public GamePage()
        {
            InitializeComponent();

            teamAScore = Preferences.Get("startingGameScore", 0);
            teamBScore = Preferences.Get("startingGameScore", 0);

            currentTeamLabel.Text = Preferences.Get(team1NameSetting,"Team 1");


        }

        public int computeScore(string color, int boardNumber)
        {
            if(color == "Red")
            {
                return 2 * boardNumber;
            }
            else if (color == "Green")
            {
                return 3 * boardNumber;
            }
            else if (color == "Purple")
            {
                return 25;
            }
            else if (color == "Yellow")
            {
                return 50;
            }
            else
            {
                return boardNumber;
            }
        }

        private void SubmitButton_Clicked(object sender, EventArgs e)
        {
            throwValue = computeScore(Convert.ToString(colorPicker.SelectedItem), Convert.ToInt32(numberPicker.SelectedItem));
            if (currentTeam == "Team 1")
            {
                if (teamAScore == Preferences.Get("startingGameScore", 0) && (Convert.ToString(colorPicker.SelectedItem)) != "Red")
                {
                    DisplayAlert("Invalid", "A double is required to begin scoring", "Ok");
                    throwValue = 0;
                }

                    teamAScore -= throwValue;
                    Preferences.Set("teamAScore", teamAScore);
                    currentTeamScore.Text = teamAScore.ToString();
                    counter++;

                    if (counter > 3)
                    {
                        DisplayAlert("Team Switch", "Your score is " + currentTeamScore.Text, "Continue");
                        currentTeam = "Team 2";
                        currentTeamLabel.Text = Preferences.Get(team2NameSetting, "Team 2");
                        currentTeamScore.Text = teamBScore.ToString();
                        counter = 1;
                    }
            }
            else 
            {

                if (teamBScore == Preferences.Get("startingGameScore", 0) && (Convert.ToString(colorPicker.SelectedItem)) != "Red")
                {
                    DisplayAlert("Invalid", "A double is required to begin scoring", "Ok");
                    throwValue = 0;
                }

                teamBScore -= throwValue;
                Preferences.Set("teamBScore", teamBScore);
                currentTeamScore.Text = teamBScore.ToString();
                counter++;
                if (counter > 3)
                {
                    DisplayAlert("Team Switch", "Your score is " + currentTeamScore.Text, "Continue");
                    currentTeam = "Team 1";
                    currentTeamLabel.Text = Preferences.Get(team1NameSetting, "Team 1");
                    currentTeamScore.Text = teamAScore.ToString();
                    counter = 1;
                }
            }


        }
    }
}
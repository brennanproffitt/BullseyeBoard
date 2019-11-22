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

        private string currentTeam = "Team 1"; //determines which team's turn it is. Team 1 by default, at the start of the game.
        private int throwValue = 0; //throwValue is the score the user gets when entering the number and color and hitting submit.

        private string team1NameSetting = "TeamOneName";
        private string team2NameSetting = "TeamTwoName";//Both of these strings are used to prevent typos when it comes to the currentTeamLabel.Text setting.

        private int currentRound = 1; //Not currently being used, but may be useful.
        private int counter = 1; //counter is used to determine how many times the submit button has been clicked, but gets reset after each team's turn (three throws).


        public GamePage()
        {
            InitializeComponent();

            teamAScore = Preferences.Get("startingGameScore", 0); //sets teamAScore to whatever the starting game score is (301 or 501)
            teamBScore = Preferences.Get("startingGameScore", 0); //sets teamBScore to whatever the starting game score is (301 or 501)

            currentTeamLabel.Text = Preferences.Get(team1NameSetting,"Team 1");


        }

        public int computeScore(string color, int boardNumber) //this is the method used to determine which values are associated with a color on the dart board.
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

        private void SubmitButton_Clicked(object sender, EventArgs e) //handles all the logic when the submit button is clicked
        {
            throwValue = computeScore(Convert.ToString(colorPicker.SelectedItem), Convert.ToInt32(numberPicker.SelectedItem)); //throwValue = whatever values were put into the pickers by the user.
            if (currentTeam == "Team 1") //currentTeam is set to "Team 1" by default. This is why whichever team goes first needs to be set to Team 1.
            {
                if (teamAScore == Preferences.Get("startingGameScore", 0) && (Convert.ToString(colorPicker.SelectedItem)) != "Red") //the logic that requires the user to begin scoring by hitting a double
                {
                    DisplayAlert("Invalid", "A double (the red section of the dart board) is required to begin scoring", "Ok");
                    throwValue = 0;
                }

                    if(counter <= 3 && (teamAScore <= 1)) //WORK IN PROGRESS: Logic that determines when a user busts.
                    {
                        DisplayAlert("Bust!","You scored an invalid amount of points. You must reach the score of 0 on a double (the red ring). Your score is now set back to the value it was before this turn.","Ok");
                        teamAScore += throwValue;
                        counter = 4;
                    }

                    teamAScore -= throwValue; //Team A score is equal to itself minus whatever the throwValue is
                    Preferences.Set("teamAScore", teamAScore); //store teamA's updated score
                    currentTeamScore.Text = teamAScore.ToString(); //updating the scoreLabel that displays at the top of the screen.
                    counter++; //incrementing the counter to determine if it is still this team's turn

                    if (counter > 3) //switches teams and displays an alert showing the current team's score.
                    {
                        DisplayAlert("Team Switch", currentTeamLabel.Text + ", your score is " + currentTeamScore.Text, "Continue");
                        currentTeam = "Team 2";
                        currentTeamLabel.Text = Preferences.Get(team2NameSetting, "Team 2");
                        currentTeamScore.Text = teamBScore.ToString();
                        counter = 1;
                    }
            }
            else //everything in this else statement is practically the same as the above, just for the opposite team.
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
                    DisplayAlert("Team Switch", currentTeamLabel.Text + ", your score is " + currentTeamScore.Text, "Continue");
                    currentTeam = "Team 1";
                    currentTeamLabel.Text = Preferences.Get(team1NameSetting, "Team 1");
                    currentTeamScore.Text = teamAScore.ToString();
                    counter = 1;
                }
            }


        }
    }
}
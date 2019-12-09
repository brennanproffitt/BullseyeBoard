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

        private void teamASwitch() //switches the turn to Team 2 and displays an alert showing Team 1's score.
        {
            DisplayAlert("Team Switch", currentTeamLabel.Text + ", your score is " + teamAScore.ToString(), "Continue");
            currentTeam = "Team 2";
            currentTeamLabel.Text = Preferences.Get(team2NameSetting, "Team 2");
            currentTeamScore.Text = teamBScore.ToString();
            Preferences.Set("teamARoundScore", teamAScore); //sets the round score for team 1 so we can call it incase they bust the next round
            counter = 1;
        }

        private void teamBSwitch() //switches the turn to Team 1 and displays an alert showing Team 2's score.
        {
            DisplayAlert("Team Switch", currentTeamLabel.Text + ", your score is " + teamBScore.ToString(), "Continue");
            currentTeam = "Team 1";
            currentTeamLabel.Text = Preferences.Get(team1NameSetting, "Team 1");
            currentTeamScore.Text = teamAScore.ToString();
            Preferences.Set("teamBRoundScore", teamBScore); //sets the round score for team 2 so we can call it incase they bust the next round
            counter = 1;
        }

        private void teamABustCheck()
        {
            if (teamAScore == 1 || teamAScore < 0) //This is now a bust check and a win check for team B
            {
                DisplayAlert("Bust!", "You scored an invalid amount of points. You must reach the score of 0 on a double (the red ring). Your score is now set back to the value it was before this turn.", "Ok");
                teamAScore = Preferences.Get("teamARoundScore", 0);
                teamASwitch();
                return;
            }
            else if (teamAScore == 0)
            {
                if (!(Convert.ToString(colorPicker.SelectedItem) == "Red" || Convert.ToString(colorPicker.SelectedItem) == "Yellow"))
                {
                    DisplayAlert("Bust!", "You scored an invalid amount of points. You must reach the score of 0 on a double (the red ring). Your score is now set back to the value it was before this turn.", "Ok");
                    teamAScore = Preferences.Get("teamARoundScore", 0);
                    teamASwitch();
                    return;
                }
                else if (Convert.ToString(colorPicker.SelectedItem) == "Red" || (Convert.ToString(colorPicker.SelectedItem)) == "Yellow")
                {
                    DisplayAlert("The winner is " + currentTeamLabel.Text, "Congratulations, " + currentTeamLabel.Text + ", you won! Continue to see the final score", "Continue");
                    Navigation.PushAsync(new FinalScorePage());
                }
            }
        }

        private void teamBBustCheck()
        {
            if (teamBScore == 1 || teamBScore <0) //This is now a bust check and a win check for team B
            {
                DisplayAlert("Bust!", "You scored an invalid amount of points. You must reach the score of 0 on a double (the red ring). Your score is now set back to the value it was before this turn.", "Ok");
                teamBScore = Preferences.Get("teamBRoundScore", 0);
                teamBSwitch();
                return;
            }
            else if(teamBScore == 0)
            {
                if (!(Convert.ToString(colorPicker.SelectedItem) == "Red" || Convert.ToString(colorPicker.SelectedItem) == "Yellow"))
                {
                    DisplayAlert("Bust!", "You scored an invalid amount of points. You must reach the score of 0 on a double (the red ring). Your score is now set back to the value it was before this turn.", "Ok");
                    teamBScore = Preferences.Get("teamBRoundScore", 0);
                    teamBSwitch();
                    return;
                }
                else if (Convert.ToString(colorPicker.SelectedItem) == "Red" || (Convert.ToString(colorPicker.SelectedItem)) == "Yellow")
                {
                    DisplayAlert("The winner is " + currentTeamLabel.Text, "Congratulations, " + currentTeamLabel.Text + ", you won! Continue to see the final score", "Continue");
                    Navigation.PushAsync(new FinalScorePage());
                }
            }
            
        }

        private async void SubmitButton_Clicked(object sender, EventArgs e) //handles all the logic when the submit button is clicked
        {
            await Dartboard.RotateTo(360,1000);
            Dartboard.Rotation = 0;
            throwValue = computeScore(Convert.ToString(colorPicker.SelectedItem), Convert.ToInt32(numberPicker.SelectedItem)); //throwValue = whatever values were put into the pickers by the user.
            if (currentTeam == "Team 1") //currentTeam is set to "Team 1" by default. This is why whichever team goes first needs to be set to Team 1.
            {
                if(!(Convert.ToString(colorPicker.SelectedItem) == "Red" || Convert.ToString(colorPicker.SelectedItem) == "Yellow"))
                {
                    if (teamAScore == Preferences.Get("startingGameScore", 0)) //the logic that requires the user to begin scoring by hitting a double
                    {
                        DisplayAlert("Invalid", "A double (the red section of the dart board) is required to begin scoring", "Ok");
                        throwValue = 0;
                    }
                }
               

                    teamAScore -= throwValue; //Team A score is equal to itself minus whatever the throwValue is
                    Preferences.Set("teamAScore", teamAScore); //store teamA's updated score
                    currentTeamScore.Text = teamAScore.ToString(); //updating the scoreLabel that displays at the top of the screen.
                    teamABustCheck(); //checks if team A busted after clicking the submit button
                    counter++; //incrementing the counter to determine if it is still this team's turn

                    if (counter > 3) //switches teams and displays an alert showing the current team's score.
                    {
                        teamASwitch();
                    }
            }
            else //everything in this else statement is practically the same as the above, just for the opposite team.
            {
                if (!(Convert.ToString(colorPicker.SelectedItem) == "Red" || Convert.ToString(colorPicker.SelectedItem) == "Yellow"))
                {
                    if (teamBScore == Preferences.Get("startingGameScore", 0))
                    {
                        DisplayAlert("Invalid", "A double (the red section of the dart board) is required to begin scoring", "Ok");
                        throwValue = 0;
                    }
                }
                

                teamBScore -= throwValue;
                Preferences.Set("teamBScore", teamBScore);
                currentTeamScore.Text = teamBScore.ToString();
                teamBBustCheck();
                counter++;

                if (counter > 3)
                {
                    teamBSwitch();
                }
            }


        }

        private void missButton_Clicked(object sender, EventArgs e)
        {
            counter++;
            if(counter > 3 && currentTeam == "Team 1")
            {
                teamASwitch();
            }
            else if (counter > 3 && currentTeam == "Team 2")
            {
                teamBSwitch();
            }
        }
    }
}
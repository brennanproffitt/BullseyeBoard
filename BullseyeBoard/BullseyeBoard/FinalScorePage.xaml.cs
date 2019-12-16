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
    public partial class FinalScorePage : ContentPage
    {
        public FinalScorePage()
        {
            InitializeComponent();
            Team1Name.Text = Preferences.Get("TeamOneName", "Team1");
            Team2Name.Text = Preferences.Get("TeamTwoName", "Team2");
            Team1FinalScore.Text = Convert.ToString(Preferences.Get("teamAScore", "Not Passed"));
            Team2FinalScore.Text = Convert.ToString(Preferences.Get("teamBScore", "Not Passed"));
        }

        private void NewGameButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GameTypePage());
        }
    }
}
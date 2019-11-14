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
    public partial class GameTypePage : ContentPage
    {
        public GameTypePage()
        {
            InitializeComponent();
        }

        private void RulesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RulesPage());
        }

        private void FiveButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TeamSelectionPage());

            int fiveStartingGameScore = 501;
            Preferences.Set("startingGameScore", fiveStartingGameScore);
        }

        private void ThreeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TeamSelectionPage());

            int threeStartingGameScore = 301;
            Preferences.Set("startingGameScore", threeStartingGameScore);
        }
    }
}
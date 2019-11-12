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
    public partial class TeamSelectionPage : ContentPage
    {
        public TeamSelectionPage()
        {
            InitializeComponent();
        }

        private void SubmitButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GamePage());

            string teamOneName = teamOneEntry.Text;
            string teamTwoName = teamTwoEntry.Text;

            Preferences.Set("TeamOneName", teamOneName);
            Preferences.Set("TeamTwoName", teamTwoName);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        private void ThreeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TeamSelectionPage());
        }
    }
}
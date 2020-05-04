using RiotSharp;
using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Riot_API_test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {

            this.InitializeComponent();


        }

        private void player_name_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == VirtualKey.Enter && API_key.Text.Count() > 0)
            {
                RiotApi api = RiotApi.GetInstance(API_key.Text, 3, 12); 

                try
                {
                    var summoner = api.Summoner.GetSummonerByNameAsync(Region.Eune, player_name.Text).Result;

                    var match = api.Match.GetMatchListAsync(summoner.Region, summoner.AccountId).Result;

                    specs.Text = $" Name: {summoner.Name}  \n Level: {summoner.Level} \n Region:  {summoner.Region} \n " +
                        $"Total Games: {match.TotalGames}";

                    

                  /*  foreach (var matchReference in matchListOfSummoner.Matches)
                    {
                        var match = riotApi.Match.GetMatchAsync(RiotSharp.Misc.Region.na, matchReference.GameId).Result;

                        // Get participant stats object of summoner (imaqtpie)
                        var particpantsId = match.ParticipantIdentities.Single(x => x.Player.AccountId == summoner.AccountId);
                        var participantsStats = match.Participants.Single(x => x.ParticipantId == particpantsId.ParticipantId);

                        // Do stuff with stats
                    }*/

                }
                catch (RiotSharpException ex)
                {
                    specs.Text = ex.Message;
                }
                catch (Exception myex)
                {
                    specs.Text = myex.Message;
                }
            }
        }
    }
}

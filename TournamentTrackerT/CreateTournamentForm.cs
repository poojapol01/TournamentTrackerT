using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;

namespace TournamentTrackerT
{
    public partial class CreateTournamentForm : Form, IPrizeRequester
    {
        List<TeamModel> availableTeams = GlobalConfig.connection.GetTeams_All();
        List<TeamModel> selectedTeams = new List<TeamModel> ();
        List<PrizeModel> selectedPrizes = new List<PrizeModel> ();

        public CreateTournamentForm()
        {
            InitializeComponent();
            WireUpLists();
        }
        private void createTournamentButton_Click(object sender, EventArgs e)
        {

        }

        private void WireUpLists()
        {
            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = selectTeamDropDown.SelectedItem as TeamModel;

            if(t != null)
            {
                selectedTeams.Add(t);
                availableTeams.Remove(t);

                WireUpLists();
            }
        }

        private void removeSelectedPlayersButton_Click(object sender, EventArgs e)
        {
            TeamModel t = tournamentTeamsListBox.SelectedItem as TeamModel;

            if (t != null)
            {
                availableTeams.Add(t);
                selectedTeams.Remove(t);

                WireUpLists();
            }
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            //call the Create Prize Form
            CreatePrizeForm createPrizeForm = new CreatePrizeForm(this);
            createPrizeForm.Show();
        }

        public void PrizeComplete(PrizeModel prize)
        {
            selectedPrizes.Add(prize);
            WireUpLists();
        }

        private void removeSelectedPrizesButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = prizesListBox.SelectedItem as PrizeModel;

            if(p != null) 
            {
                selectedPrizes.Remove(p);
                WireUpLists();
            }
        }
    }
}

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
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
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
            decimal fee = 0;

            bool acceptableFee = decimal.TryParse(entryFeeValue.Text, out fee);

            if (!acceptableFee)
            {
                MessageBox.Show("Enter a Valid Fee Value", "Invalid Fee", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Create Tournamnet Model 
            TournamentModel tm = new TournamentModel();
            tm.TournamentName = tournamentNameValue.Text;
            tm.EntryFee = fee;
            tm.Prizes = selectedPrizes;
            tm.EnteredTeam = selectedTeams;
            // Wire our Matchups

            // Create Tournament Entry
            // Create all of the Prizes Entry
            // Create all of Team Entries
            GlobalConfig.connection.CreateTournament(tm);
            this.Close();
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
                selectedTeams.Remove(t);
                availableTeams.Add(t);

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

        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm createTeamForm = new CreateTeamForm(this);
            createTeamForm.Show();
        }

        public void TeamComplete(TeamModel team)
        {
            availableTeams.Add(team);
            WireUpLists();
        }
    }
}

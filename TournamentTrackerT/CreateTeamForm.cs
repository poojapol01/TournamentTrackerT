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
    public partial class CreateTeamForm : Form
    {
        private List<PersonModel> availableTeamMembers = GlobalConfig.connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        public CreateTeamForm()
        {
            InitializeComponent();

            //CreateSampleData();

            WireUpLists();
        }

        void WireUpLists()
        {
            selectTeamMemberDropDown.DataSource = null;
            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;
            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";
        }

        public void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Tina", LastName = "Sethi" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Mina", LastName = "Rathi" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Pratibha", LastName = "Selati" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Prerna", LastName = "Gulati" });
        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel teamModel = new TeamModel();
            teamModel.TeamName = teamNameValue.Text;
            teamModel.TeamMembers = selectedTeamMembers;

            teamModel = GlobalConfig.connection.CreateTeam(teamModel);
            //TODO: If we aren't closing this form after creation then reset it.
        }

        private bool ValidateForm()
        {
            if(firstNameValue.Text.Length == 0)
            {
                return false;
            }
            if(lastNameValue.Text.Length == 0)
            {
                return false;
            }
            if(emailValue.Text.Length == 0) { return false; }

            if(cellphoneValue.Text.Length == 0) { return false; }

            return true;
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel personModel = new PersonModel(
                    firstNameValue.Text,
                    lastNameValue.Text,
                    emailValue.Text,
                    cellphoneValue.Text);

                GlobalConfig.connection.CreatePerson(personModel);

                selectedTeamMembers.Add(personModel);
                WireUpLists();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("This Form has invalid Information. Please fill in the correct details");
            }
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = selectTeamMemberDropDown.SelectedItem as PersonModel;
            
            if (p != null)
            {
                selectedTeamMembers.Add(p);
                availableTeamMembers.Remove(p);

                WireUpLists();
            }
        }

        private void removeSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = teamMembersListBox.SelectedItem as PersonModel;

            if(p!= null)
            {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpLists();
            } 
        }
    }
}

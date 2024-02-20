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
    public partial class CreatePrizeForm : Form
    {
        IPrizeRequester callingForm;
        public CreatePrizeForm(IPrizeRequester caller)
        {
            InitializeComponent();
            callingForm = caller;
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm() == true)
            {
                PrizeModel prizeModel = new PrizeModel(placeNumberValue.Text, 
                    placeNameValue.Text, 
                    prizeAmountValue.Text, 
                    prizePercentageValue.Text);

                    GlobalConfig.connection.CreatePrize(prizeModel);

                callingForm.PrizeComplete(prizeModel);

                this.Close();
                //GlobalConfig.connections.Add(prizeModel);
            }
            else
            {
                MessageBox.Show("This form has invalid information. Please check it and try again.");
            }
        }

        private bool ValidateForm()
        {
            bool output = true;
            //int placeNumberValidNumber = 0;
            bool placeNumberValid = int.TryParse(placeNumberValue.Text, out int placeNumber);

            if (!placeNumberValid)
            {
                return false;
            }
            if (placeNumber < 1 || placeNameValue.Text.Length == 0)
            {
                return false;
            }

            bool prizeAmountValid = decimal.TryParse(prizeAmountValue.Text, out decimal prizeAmount);
            bool prizePercentageValid = int.TryParse(prizePercentageValue.Text, out int prizePercentage);

            if (prizeAmountValid == false || prizePercentageValid == false)
            {
                return false;
            }
            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                return false;
            }
            if (prizePercentage < 0 || prizePercentage > 100)
            {
                output = false;
            }
            return output;
        }
    }
}

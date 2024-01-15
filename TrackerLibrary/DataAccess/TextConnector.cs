using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";
        public PrizeModel CreatePrize(PrizeModel prizeModel)
        {
            // Load the text file
            // Convert the text to a List<PrizeModel>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            // Find the Max ID
            int currentId = 1;
            if( prizes.Count > 0 )
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            
            prizeModel.Id = currentId;

            // Convert the Prizes to List<string>
            prizes.Add(prizeModel);

            // Save the list<string> to the text file
            prizes.SaveToPrizesFile(PrizesFile);
            return prizeModel;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName)
        {
            return $"{ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }
            return File.ReadAllLines(file).ToList();
        }

        #region PrizeData 
        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List <PrizeModel> output = new List<PrizeModel> ();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PrizeModel prizeModel = new PrizeModel();
                prizeModel.Id = int.Parse(cols[0]);
                prizeModel.PlaceNumber = int.Parse(cols[1]);
                prizeModel.PlaceName = cols[2];
                prizeModel.PrizeAmount = int.Parse(cols[3]);
                prizeModel.PrizePercentage = int.Parse(cols[4]);
                output.Add(prizeModel);
            }  
                return output;
        }

        public static void SaveToPrizesFile(this List<PrizeModel> models, string fileName)
        {
            List<string> output = new List<string>();

            foreach (PrizeModel prizeModel in models)
            {
                output.Add($"{prizeModel.Id},{prizeModel.PlaceNumber},{prizeModel.PlaceName}, {prizeModel.PrizeAmount}, {prizeModel.PrizePercentage}");
            }
            File.WriteAllLines(fileName.FullFilePath(), output);
        }
        #endregion

        #region PeopleData 
        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PersonModel personModel = new PersonModel();
                personModel.Id = int.Parse(cols[0]);
                personModel.FirstName = cols[1];
                personModel.LastName = cols[2];
                personModel.EmailAddress = cols[3];
                personModel.CellPhoneNumber = cols[4];

                output.Add(personModel);
            }
            return output;
        }

        public static void SaveToPeopleFile(this List<PersonModel> models, string fileName)
        {
            List<string> output = new List<string>();

            foreach(PersonModel personModel in models)
            {
                output.Add($"{personModel.Id},{personModel.FirstName},{personModel.LastName},{personModel.EmailAddress},{personModel.CellPhoneNumber}");
            }
            File.WriteAllLines(fileName.FullFilePath(), output);
        }
        #endregion
    }
}

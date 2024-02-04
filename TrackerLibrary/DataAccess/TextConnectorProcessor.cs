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

        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string peopleFileName)
        {
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> personModel = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModels();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TeamModel t = new TeamModel();
                t.Id = int.Parse(cols[0]);
                t.TeamName = cols[1];
                string[] personIds = cols[2].Split('|');

                foreach(string id in personIds)
                {
                    t.TeamMembers.Add(personModel.Where(x => x.Id == int.Parse(id)).First());
                }
                output.Add(t);
            }
            return output;
        }

        public static void SaveToTeamsFile(this List<TeamModel> teams, string fileName)
        {
            List<String> output = new List<string>();

            foreach (TeamModel team in teams)
            {
                output.Add($"{ team.Id }, { team.TeamName }, { ConvertPeopleListToString(team.TeamMembers) }");
            }
            File.WriteAllLines(fileName.FullFilePath(), output);
        }

        public static string ConvertPeopleListToString(this List<PersonModel> people)
        {
            String output =  string.Empty;
            if(people.Count == 0)
            {
                return "";
            }
            foreach(PersonModel p in people)
            {
                output += $"{p.Id}|";
            }
            output = output.Substring(0, output.Length - 1);

            return output;
        }
    }
}

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
        private const string PeopleFile = "PersonModels.csv";
        private const string TeamsFile = "TeamModels.csv";
        private const string TournamentsFile = "Tournaments.csv";

        public PersonModel CreatePerson(PersonModel personModel)
        {
            // Load the text file
            // Convert the text to a List<PersonModel>
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            // Find the Max ID
            int currentId = 1;
            if(people.Count > 0)
            {
                currentId = people.OrderByDescending(p => p.Id).First().Id + 1;
            }
            personModel.Id = currentId;

            // Convert the Prizes to List<string>
            people.Add(personModel);

            // Save the list<string> to the text file
            people.SaveToPeopleFile(PeopleFile);
            return personModel;
        }

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

        public TeamModel CreateTeam(TeamModel teamModel)
        {
            List<TeamModel> teams = TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);

            int currentId = 1;
            if( teams.Count > 0 )
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }
            teamModel.Id = currentId;

            teams.Add(teamModel);

            teams.SaveToTeamsFile(TeamsFile);

            return teamModel;
        }

        public TournamentModel CreateTournament(TournamentModel tournamentModel)
        {
            List<TournamentModel> tournaments = TournamentsFile.FullFilePath().LoadFile().ConvertToTournamentsModels(TeamsFile, PeopleFile, PrizesFile);

            int currentId = 1;
            if( tournaments.Count > 0 )
            {
                currentId = tournaments.OrderByDescending(x => x.Id).First().Id + 1;
            }
            tournamentModel.Id = currentId;
            tournaments.Add(tournamentModel);
            tournaments.SaveToTournamentsFile(TournamentsFile);

            return tournamentModel;
        }

        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public List<TeamModel> GetTeams_All()
        {
            return TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);
        }
    }
}

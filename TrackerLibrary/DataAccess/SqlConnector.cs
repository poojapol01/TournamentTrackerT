using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        /// <summary>
        /// Saves a new Person to the Database
        /// </summary>
        /// <param name="personModel">The Person information</param>
        /// <returns>The Person information, including unique identifier</returns>
        public PersonModel CreatePerson(PersonModel personModel)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(GlobalConfig.cnnstring("Tournaments")))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", personModel.FirstName);
                p.Add("@LastName", personModel.LastName);
                p.Add("@EmailAddress", personModel.EmailAddress);
                p.Add("@CellPhoneNumber", personModel.CellPhoneNumber);
                p.Add("@Id", 0, DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);
                personModel.Id = p.Get<int>("@Id");
            }
            return personModel;
        }

        /// <summary>
        /// Saves a new Prize to the Database
        /// </summary>
        /// <param name="prizeModel">The Prize information</param>
        /// <returns>The Prize information, including unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel prizeModel)
        {
            //prizeModel.Id = 1;

            //return prizeModel;

            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(GlobalConfig.cnnstring("Tournaments")))
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", prizeModel.PlaceNumber);
                p.Add("@PlaceName", prizeModel.PlaceName);
                p.Add("@PrizeAmount", prizeModel.PrizeAmount);
                p.Add("@PrizePercentage", prizeModel.PrizePercentage);
                p.Add("@Id", 0, DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute("dbo.spPrizes_Insert", p,commandType: CommandType.StoredProcedure);
                prizeModel.Id = p.Get<int>("@Id");

                return prizeModel;
            }
        }

        public TeamModel CreateTeam(TeamModel teamModel)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(GlobalConfig.cnnstring("Tournaments")))
            {
                var p = new DynamicParameters();
                p.Add("@TeamName", teamModel.TeamName);
                p.Add("@Id", 0, DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute("dbo.spTeams_Insert1", p, commandType: CommandType.StoredProcedure);

                teamModel.Id = p.Get<int>("@Id");

                foreach(PersonModel tm in teamModel.TeamMembers)
                {
                    p = new DynamicParameters();
                    p.Add("@TeamId", teamModel.Id);
                    p.Add("PersonId", tm.Id);
              
                    conn.Execute("dbo.spTeamMembers_Insert1", p, commandType: CommandType.StoredProcedure);
                }
                return teamModel;
            }
        }

        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> output;
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(GlobalConfig.cnnstring("Tournaments")))
            {
                output = conn.Query<PersonModel>("dbo.spPeople_GetALL").ToList();
            }
            return output;
        }

        public List<TeamModel> GetTeams_All()
        {
            List<TeamModel> output;

            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(GlobalConfig.cnnstring("Tournaments")))
            {
                output = conn.Query<TeamModel>("dbo.spTeams_GetAll").ToList();

                foreach(TeamModel tm in output)
                {
                    var p = new DynamicParameters();
                    p.Add("@TeamId", tm.Id);
                    tm.TeamMembers = conn.Query<PersonModel>("spTeamMembers_GetByTeam", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            return output;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        PrizeModel CreatePrize(PrizeModel prizeModel);

        PersonModel CreatePerson(PersonModel personModel);

        TeamModel CreateTeam(TeamModel teamModel);

        List<PersonModel> GetPerson_All();
    }
}

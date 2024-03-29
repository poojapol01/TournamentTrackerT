﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents what the prize is for the given place.
    /// </summary>
    public class PrizeModel
    {
        /// <summary>
		/// The unique identifier for the prize.
		/// </summary>
		public int Id { get; set; }
        /// <summary>
        /// The numeric identifier for the place (2 for the second place etc.)
        /// </summary>
        public int PlaceNumber { get; set; }

        /// <summary>
        /// The friendly name for the place (second place, first runner up, etc.)
        /// </summary>
        public string PlaceName { get; set; }

        /// <summary>
        /// The fixed amount this place earns or zero if it is not used.
        /// </summary>
        public decimal PrizeAmount { get; set; }

        /// <summary>
        /// The number that represent the percentage of the overall take or
		/// zero if it is not used. The percentage is a fraction of 1 (so 0.5 for 
		/// 50%)
        /// </summary>
        public double PrizePercentage { get; set; }

        public PrizeModel()
        {

        }
        public PrizeModel(string placeNumber, string placeName, string prizeAmount, string prizePercentage)
        {
            PlaceName = placeName;

            int.TryParse(placeNumber, out int placeNumberValue);
            PlaceNumber = placeNumberValue;

            int.TryParse(prizeAmount, out int prizeAmountValue);
            PrizeAmount = prizeAmountValue;

            int.TryParse(prizePercentage, out int prizePercentageValue);
            PrizePercentage = prizePercentageValue;
        }
    }
}

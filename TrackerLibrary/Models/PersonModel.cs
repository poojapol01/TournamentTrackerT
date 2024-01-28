using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents one person.
    /// </summary>
    public class PersonModel
    {
        /// <summary>
        /// Unique identity of the person
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The first name of the person. 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the person. 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The primary email address of the person.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The primary cell phone number of the person.
        /// </summary>
        public string CellPhoneNumber { get; set; }

        public PersonModel(string firstName, string lastName, string email, string cellphoneNumber ) 
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = email;
            this.CellPhoneNumber = cellphoneNumber;
        }
    }
}

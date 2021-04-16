using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kaizen.Controllers.Resources
{
    public class EmployeeResource
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Extension { get; set; }
        public string PhotoPath { get; set; }
    }
}
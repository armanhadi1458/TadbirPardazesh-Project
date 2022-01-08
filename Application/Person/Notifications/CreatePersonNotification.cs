using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Person.Notifications
{
    public class CreatePersonNotification: object
    {
        public CreatePersonNotification():base()
        {
        }

        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

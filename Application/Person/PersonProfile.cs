using Application.Person.Notifications;
using AutoMapper;

namespace Application.Person
{
    public class PersonProfile: Profile
    {
        public PersonProfile()
        {
            CreateMap<CreatePersonNotification, Core.Models.Person>().ReverseMap();
        }
    }
}

using Application.Person.Notifications;
using AutoMapper;

namespace Application.Person
{
    class PersonProfile: Profile
    {
        public PersonProfile()
        {
            CreateMap<CreatePersonNotification, Core.Models.Person>().ReverseMap();
        }
    }
}

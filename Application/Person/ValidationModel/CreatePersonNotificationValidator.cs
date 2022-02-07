using Application.Person.Notifications;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Person.ValidationModel
{
    public class CreatePersonNotificationValidator : AbstractValidator<CreatePersonNotification>
    {
        public CreatePersonNotificationValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Age).InclusiveBetween(18, 60);
        }
    }
}

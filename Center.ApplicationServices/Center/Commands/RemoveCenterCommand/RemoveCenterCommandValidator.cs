using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Center.Commands.RemoveCenterCommand
{
    public class RemoveCenterCommandValidator : AbstractValidator<RemoveCenterCommand>
    {
        public RemoveCenterCommandValidator()
        {

        }
    }
}

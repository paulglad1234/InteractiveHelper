﻿using AutoMapper;
using FluentValidation;
using InteractiveHelper.CharactetisricService.Models;

namespace InteractiveHelper.Api.Controllers.Characteristics.Models;

public class UpdateCharacteristicRequest
{
    public int? Id { get; set; }
    public string Name { get; set; }
}

public class UpdateCharacteristicRequestValidator : AbstractValidator<UpdateCharacteristicRequest>
{
    public UpdateCharacteristicRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(30).WithMessage("Name is too long.");
    }
}

public class UpdateCharacteristicRequestProfile : Profile
{
    public UpdateCharacteristicRequestProfile()
    {
        CreateMap<UpdateCharacteristicRequest, UpdateCharacteristicModel>();
    }
}

﻿using MediatR;
using SigmaCars.Infrastructure.Persistence;

namespace SigmaCars.Application.Features.CarModel.Commands;

public class CreateCarModelHandler : IRequestHandler<CreateCarModelCommand, CreateCarModelResponse>
{
    private readonly SigmaCarsDbContext _dbContext;

    public CreateCarModelHandler(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateCarModelResponse> Handle(CreateCarModelCommand command, CancellationToken cancellationToken)
    {
        var carModel = new Domain.Models.CarModel
        {
            Id = 0,
            Color = command.Color,
            Make = command.Make,
            Model = command.Model,
            ProductionYear = command.ProductionYear,
            PricePerDay = command.PricePerDay,
            SeatCount = command.SeatCount
        };
          
        _dbContext.Set<Domain.Models.CarModel>().Add(carModel);
        await _dbContext.SaveChangesAsync(cancellationToken);
            
        return CreateCarModelResponse.FromCarModel(carModel);
    }
}
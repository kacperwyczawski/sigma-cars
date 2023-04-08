using System.ComponentModel.DataAnnotations;

namespace SigmaCars.Application.Features.CarModel.Requests;

public record GetFilteredCarModelsRequest(
    int? MinYear, int? MaxYear,
    float? MinPrice, float? MaxPrice,
    int? MinSeats, int? MaxSeats,
    string? Make, string? Model
    );
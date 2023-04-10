namespace SigmaCars.Application.Features.CarModel.Requests;

public record GetCarModelRequest(
    int? MinYear, int? MaxYear,
    float? MinPrice, float? MaxPrice,
    int? MinSeats, int? MaxSeats,
    string? Make,
    string? Model,
    string? OrderByPropertyName,
    bool Ascending);
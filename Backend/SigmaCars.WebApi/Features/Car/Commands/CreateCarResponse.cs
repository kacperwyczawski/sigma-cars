namespace SigmaCars.WebApi.Features.Car.Commands;

public record CreateCarResponse(
    int Id,
    int CarModelId,
    int DepartmentId,
    string RegistrationNumber,
    string Vin)
{
    public static CreateCarResponse FromCar(Domain.Models.Car car) => new(
        car.Id,
        car.CarModelId,
        car.DepartmentId,
        car.RegistrationNumber,
        car.Vin);
}
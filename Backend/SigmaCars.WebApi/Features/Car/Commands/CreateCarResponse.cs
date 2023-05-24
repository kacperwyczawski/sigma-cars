namespace SigmaCars.WebApi.Features.Car.Commands;

public record CreateCarResponse(
    int Id,
    int CarTypeId,
    int DepartmentId,
    string RegistrationNumber,
    string Vin)
{
    public static CreateCarResponse FromCar(Domain.Models.Car car) => new(
        car.Id,
        car.CarTypeId,
        car.DepartmentId,
        car.RegistrationNumber,
        car.Vin);
}
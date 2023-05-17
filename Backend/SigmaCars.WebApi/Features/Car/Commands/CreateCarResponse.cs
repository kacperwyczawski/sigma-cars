namespace SigmaCars.WebApi.Features.Car.Commands;

public record CreateCarResponse(
    int Id,
    int carTypeId,
    int DepartmentId,
    string RegistrationNumber,
    string Vin)
{
    public static CreateCarResponse FromCar(Domain.Models.Car car) => new(
        car.Id,
        car.carTypeId,
        car.DepartmentId,
        car.RegistrationNumber,
        car.Vin);
}
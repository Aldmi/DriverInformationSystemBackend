namespace Application.Features.TrainLists.CreateCommand;

public class TrainDto
{
   public string? Name { get; init; }
   public LocomotiveDto LocomotiveOne { get; init; }
   public LocomotiveDto LocomotiveTwo { get; init; }
   public CarrigeDto[] Carriges { get; init; }
}

public class LocomotiveDto
{
   public string UniqCarrigeNumber { get; init; }
   public string[] CameraIpAddress { get; init; }
}

public class CarrigeDto
{
    public string UniqCarrigeNumber { get; init; }
    public string CameraFirstIpAddress { get; init; }
    public string CameraSecondIpAddress { get; init; }
}


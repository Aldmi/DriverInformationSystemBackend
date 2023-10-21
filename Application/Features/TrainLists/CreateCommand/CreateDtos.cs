namespace Application.Features.TrainLists.CreateCommand;


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


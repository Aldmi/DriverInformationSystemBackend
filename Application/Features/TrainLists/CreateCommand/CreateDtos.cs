namespace Application.Features.TrainLists.CreateCommand;

public class TrainDto
{
   public string? Name { get; set; }
   public LocomotiveDto LocomotiveOne { get; set; }
   public LocomotiveDto LocomotiveTwo { get; set; }
   public CarrigeDto[] Carriges { get; set; }
}

public class LocomotiveDto
{
   public string UniqCarrigeNumber { get; set; }
   public string CameraFirstIpAddress { get; set; }
   public string CameraSecondIpAddress { get; set; }
}

public class CarrigeDto
{
    public string UniqCarrigeNumber { get; set; }
    public string CameraFirstIpAddress { get; set; }
    public string CameraSecondIpAddress { get; set; }
}


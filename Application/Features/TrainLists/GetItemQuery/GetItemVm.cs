namespace Application.Features.TrainLists.GetItemQuery;

public class TrainItemVm
{ 
   public Guid Id { get; init; }
   public string? Name { get; init; }
   public LocomotiveItemVm LocomotiveOne { get; init; }
   public LocomotiveItemVm LocomotiveTwo { get; init; }
   public CarrigeItemVm[] Carriges { get; init; }
}

public class LocomotiveItemVm
{
   public string UniqCarrigeNumber { get; init; }
   public string[] CameraIpAddress { get; init; }
}

public class CarrigeItemVm
{
    public string UniqCarrigeNumber { get; init; }
    public string CameraFirstIpAddress { get; init; }
    public string CameraSecondIpAddress { get; init; }
}


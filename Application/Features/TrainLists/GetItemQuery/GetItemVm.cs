namespace Application.Features.TrainLists.GetItemQuery;

public class TrainItemVm
{ 
   public Guid Id { get; set; }
   public string? Name { get; set; }
   public LocomotiveItemVm LocomotiveOne { get; set; }
   public LocomotiveItemVm LocomotiveTwo { get; set; }
   public CarrigeItemVm[] Carriges { get; set; }
}

public class LocomotiveItemVm
{
   public string UniqCarrigeNumber { get; set; }
   public string CameraFirstIpAddress { get; set; }
   public string CameraSecondIpAddress { get; set; }
}

public class CarrigeItemVm
{
    public string UniqCarrigeNumber { get; set; }
    public string CameraFirstIpAddress { get; set; }
    public string CameraSecondIpAddress { get; set; }
}


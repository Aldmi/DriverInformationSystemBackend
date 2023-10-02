using System.Security.AccessControl;

namespace Application.Features.TrainLists.GetListQuery;

public class TrainVm
{ 
   public Guid Id { get; set; }
   public string? Name { get; set; }
   public LocomotiveVm LocomotiveOne { get; set; }
   public LocomotiveVm LocomotiveTwo { get; set; }
   public CarrigeVm[] Carriges { get; set; }
}

public class LocomotiveVm
{
   public string UniqCarrigeNumber { get; set; }
   public string CameraFirstIpAddress { get; set; }
   public string CameraSecondIpAddress { get; set; }
}

public class CarrigeVm
{
    public string UniqCarrigeNumber { get; set; }
    public string CameraFirstIpAddress { get; set; }
    public string CameraSecondIpAddress { get; set; }
}


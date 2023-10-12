using System.Security.AccessControl;

namespace Application.Features.TrainLists.GetListQuery;

public class TrainVm
{ 
   public Guid Id { get; init; }
   public string? Name { get; init; }
   public LocomotiveVm LocomotiveOne { get; init; }
   public LocomotiveVm LocomotiveTwo { get; init; }
   public CarrigeVm[] Carriges { get; init; }
}

public class LocomotiveVm
{
   public string UniqCarrigeNumber { get; init; }
   public string CameraFirstIpAddress { get; init; }
   public string CameraSecondIpAddress { get; init; }
}

public class CarrigeVm
{
    public string UniqCarrigeNumber { get; init; }
    public string CameraFirstIpAddress { get; init; }
    public string CameraSecondIpAddress { get; init; }
}


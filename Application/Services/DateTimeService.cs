using Application.Common.Interfaces;

namespace Application.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}

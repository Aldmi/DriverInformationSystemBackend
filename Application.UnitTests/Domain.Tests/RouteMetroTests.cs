using Application.Common.Models;
using Application.Domain.RouteMetroAgregat;
using Application.ValueObjects;
using FluentAssertions;

namespace Application.UnitTests.Domain.Tests;

public class RouteMetroTests
{
    private RouteMetro Route { get; } = RouteMetro.Create(
        "Заельцовская - площадь Маркса",
        Gender.Male,
        new[]
        {
            new UowAlert(
                "Заельцовская",
                "Посадка на Заельцовской",
                new SoundMessage[]
                {
                    new("сторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("Следующая станция Гагаринская")),

            new UowAlert(
                "Заельцовская",
                "Посадка на Гагаринской",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("Станция Гагаринская"))
        }
    ).Value;

    [Fact]
    public void ChangeUowsList_Empty_uowsNew_Expected_Failure()
    {
        List<UowAlert> uows = new List<UowAlert>();
        var result=Route.ChangeUowsList(uows.ToArray());
        result.IsFailure.Should().BeTrue();
    }
}
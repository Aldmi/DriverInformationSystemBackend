using Application.Common.Models;
using Application.Domain.RouteMetroAgregat;
using Application.ValueObjects;

namespace Application.UnitTests.Infrastructure.Dal.MongoDb.Tests.SeedDatas;

public static class RoutesMetroFactory
{
    public static List<RouteMetro> SeedList { get; } = new()
    {
        RouteMetro.Create(
            "Заельцовская - площадь Маркса",
            Gender.Mail,
            new []
            {
                new UowAlert(
                    new SoundMessage[]
                    {
                        new("сторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                        new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                        new("уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми", "./assets/sounds/001 Гагаринская 2.wav")
                    },
                    new Ticker("./assets/sounds/019 УПБВУМПЛИПСД 2.wav")),
                
                new UowAlert(
                    new SoundMessage[]
                    {
                        new("сторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                        new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                        new("уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми", "./assets/sounds/001 Гагаринская 2.wav")
                    },
                    new Ticker("./assets/sounds/019 УПБВУМПЛИПСД 2.wav"))
            }
            ).Value
    };
}
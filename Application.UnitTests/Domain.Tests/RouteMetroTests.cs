using Application.Common.Models;
using Application.Domain.RouteMetroAgregat;
using Application.ValueObjects;
using CSharpFunctionalExtensions;
using FluentAssertions;

namespace Application.UnitTests.Domain.Tests;

public class RouteMetroTests
{
    private RouteMetro Route { get; } = RouteMetro.Create(
        "Заельцовская - площадь Маркса",
        Gender.Male,
        new[]
        {
            //ЗАЕЛЬЦОВСКАЯ
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
                "Отправление на Гагаринскую",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("Станция Гагаринская")),
            
            //ГАГАРИНСКАЯ
            new UowAlert(
                "Гагаринская",
                "Посадка на Гагаринской",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("Следующая станция Красный проспект")),
            
            new UowAlert(
                "Гагаринская",
                "Отправление от Гагаринской",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("Станция Красный проспект")),
            
            //КР. ПРОСПЕКТ
            new UowAlert(
                "Кр проспект",
                "Посадка на Кр проспекте",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("Следующая станция площадь ленина")),
            
            new UowAlert(
                "Кр проспект",
                "Отправление от Кр. проспекта",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("Станция площадь ленина"))
        }
    ).Value;

    
    [Fact]
    public void ChangeUowsList_Empty_uowsNew_Expected_Failure()
    {
        List<UowAlert> uows = new List<UowAlert>();
        var result=Route.ChangeUowsList("Кр проспект", uows.ToArray());
        result.IsFailure.Should().BeTrue();
    }
    
    
    [Fact]
    public void ChangeUowsList_stationTag_bad_Expected_Failure()
    {
        List<UowAlert> uows = new List<UowAlert>
        {
            new UowAlert(
                "Гагаринская",
                "Отправление от Гагаринской",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("New ticker message 2"))
        };
        
        var result=Route.ChangeUowsList("Bad Tag", uows.ToArray());
        result.IsFailure.Should().BeTrue();
    }
    
    
    [Fact]
    public void ChangeUowsList_uowsNewCount_Equal_uowCount_Expected_Succsess()
    {
        List<UowAlert> uows = new List<UowAlert>
        {
            //ГАГАРИНСКАЯ
            new UowAlert(
                "Гагаринская",
                "Посадка на Гагаринской",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("New ticker message 1")),
            
            new UowAlert(
                "Гагаринская",
                "Отправление от Гагаринской",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("New ticker message 2"))
        };
        
        var result=Route.ChangeUowsList("Гагаринская", uows.ToArray());
        result.IsSuccess.Should().BeTrue();

        Route.Uows[2].Ticker.Message.Should().Be("New ticker message 1");
        Route.Uows[3].Ticker.Message.Should().Be("New ticker message 2");
    }
    
    
    [Fact]
    public void ChangeUowsList_uowsNewCount_Less_uowCount_Expected_Succsess()
    {
        List<UowAlert> uows = new List<UowAlert>
        {
            //ГАГАРИНСКАЯ
            new UowAlert(
                "Гагаринская",
                "Посадка на Гагаринской",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("New ticker message 1"))
        };
        
        var result= Route.ChangeUowsList("Гагаринская", uows.ToArray());
        result.IsSuccess.Should().BeTrue();

        Route.Uows[2].Ticker.Message.Should().Be("New ticker message 1");
        Route.Uows[3].StationTag.Should().Be("Кр проспект");
    }
    
    
    [Fact]
    public void ChangeUowsList_uowsNewCount_More_uowCount_Expected_Succsess()
    {
        List<UowAlert> uows = new List<UowAlert>
        {
            //ГАГАРИНСКАЯ
            new UowAlert(
                "Гагаринская",
                "Посадка на Гагаринской",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("New ticker message 1")),
            
            new UowAlert(
                "Гагаринская",
                "Отправление от Гагаринской",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("New ticker message 2")),
            
            new UowAlert(
                "Гагаринская",
                "New item",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("New ticker message 3"))
        };
        
        var result= Route.ChangeUowsList("Гагаринская", uows.ToArray());
        result.IsSuccess.Should().BeTrue();

        Route.Uows[2].Ticker.Message.Should().Be("New ticker message 1");
        Route.Uows[3].Ticker.Message.Should().Be("New ticker message 2");
        Route.Uows[4].StationTag.Should().Be("Гагаринская");
        Route.Uows[4].Ticker.Message.Should().Be("New ticker message 3");
    }
    
    
    [Fact]
    public void ChangeUowsList_uowsNewCount_More_uowCount_Inseart_In_End_ListExpected_Succsess()
    {
        List<UowAlert> uows = new List<UowAlert>
        {
            //КР. ПРОСПЕКТ
            new UowAlert(
                "Кр проспект",
                "Посадка на Кр проспекте",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("New ticker message 1")),
            
            new UowAlert(
                "Кр проспект",
                "Отправление от Кр. проспекта",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("New ticker message 2")),
            
            new UowAlert(
                "Кр проспект",
                "New item",
                new SoundMessage[]
                {
                    new("осторожно двери закрываются след. станция", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new("Гагаринская", "./assets/sounds/013 ОДЗСС 2.wav"),
                    new(
                        "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
                        "./assets/sounds/001 Гагаринская 2.wav")
                },
                new Ticker("New ticker message 3"))
        };
        
        var result= Route.ChangeUowsList("Кр проспект", uows.ToArray());
        result.IsSuccess.Should().BeTrue();

        Route.Uows[4].Ticker.Message.Should().Be("New ticker message 1");
        Route.Uows[5].Ticker.Message.Should().Be("New ticker message 2");
        Route.Uows[6].StationTag.Should().Be("Кр проспект");
        Route.Uows[6].Ticker.Message.Should().Be("New ticker message 3");
    }
}
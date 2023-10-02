# DriverInformationSystem Backend

#### Серверная часть системы информирования машиниста метро позда.

## REST wep api:

<font size=6 style="color:red"> **Поезда:** </font>



<font size=4 style="color:lightGreen"> Получить все поезда </font>
### `GET: http://ip:port/api/trains/`

Ответ:

    [
        {
            "id": "bf4e8299-6fc6-413e-82a0-d615b6aaef67",
            "name": "Train 1",
            "locomotiveOne": {
                "uniqCarrigeNumber": "1111",
                "cameraFirstIpAddress": "192.168.1.1",
                "cameraSecondIpAddress": "192.168.1.2"
            },
            "locomotiveTwo": {
                "uniqCarrigeNumber": "2222",
                "cameraFirstIpAddress": "192.168.1.3",
                "cameraSecondIpAddress": "192.168.1.4"
            },
            "carriges": [
                {
                    "uniqCarrigeNumber": "3333",
                    "cameraFirstIpAddress": "192.168.1.10",
                    "cameraSecondIpAddress": "192.168.1.11"
                },
                {
                    "uniqCarrigeNumber": "4444",
                    "cameraFirstIpAddress": "192.168.1.12",
                    "cameraSecondIpAddress": "192.168.1.13"
                },
                {
                    "uniqCarrigeNumber": "5555",
                    "cameraFirstIpAddress": "192.168.1.14",
                    "cameraSecondIpAddress": "192.168.1.15"
                },
                {
                    "uniqCarrigeNumber": "6666",
                    "cameraFirstIpAddress": "192.168.1.16",
                    "cameraSecondIpAddress": "192.168.1.17"
                }
            ]
        },
        {
            "id": "8901972e-ce0e-4999-b687-f88d11b228f9",
            "name": "Train 1",
            "locomotiveOne": {
                "uniqCarrigeNumber": "1111",
                "cameraFirstIpAddress": "192.168.1.1",
                "cameraSecondIpAddress": "192.168.1.2"
            },
            "locomotiveTwo": {
                "uniqCarrigeNumber": "2222",
                "cameraFirstIpAddress": "192.168.1.3",
                "cameraSecondIpAddress": "192.168.1.4"
            },
            "carriges": [
                {
                    "uniqCarrigeNumber": "3333",
                    "cameraFirstIpAddress": "192.168.1.10",
                    "cameraSecondIpAddress": "192.168.1.11"
                },
                {
                    "uniqCarrigeNumber": "4444",
                    "cameraFirstIpAddress": "192.168.1.12",
                    "cameraSecondIpAddress": "192.168.1.13"
                },
                {
                    "uniqCarrigeNumber": "5555",
                    "cameraFirstIpAddress": "192.168.1.14",
                    "cameraSecondIpAddress": "192.168.1.15"
                },
                {
                    "uniqCarrigeNumber": "6666",
                    "cameraFirstIpAddress": "192.168.1.16",
                    "cameraSecondIpAddress": "192.168.1.17"
                }
            ]
        }
    ]

### Status: 200 Ok

---
<font size=4 style="color:lightGreen"> Получить поезд по Id </font>

### `GET: http://ip:port/api/trains/{id}`

Ответ:

    {
        "id": "bf4e8299-6fc6-413e-82a0-d615b6aaef67",
        "name": "Train 1",
        "locomotiveOne": {
            "uniqCarrigeNumber": "1111",
            "cameraFirstIpAddress": "192.168.1.1",
            "cameraSecondIpAddress": "192.168.1.2"
        },
        "locomotiveTwo": {
            "uniqCarrigeNumber": "2222",
            "cameraFirstIpAddress": "192.168.1.3",
            "cameraSecondIpAddress": "192.168.1.4"
        },
        "carriges": [
            {
                "uniqCarrigeNumber": "3333",
                "cameraFirstIpAddress": "192.168.1.10",
                "cameraSecondIpAddress": "192.168.1.11"
            },
            {
                "uniqCarrigeNumber": "4444",
                "cameraFirstIpAddress": "192.168.1.12",
                "cameraSecondIpAddress": "192.168.1.13"
            },
            {
                "uniqCarrigeNumber": "5555",
                "cameraFirstIpAddress": "192.168.1.14",
                "cameraSecondIpAddress": "192.168.1.15"
            },
            {
                "uniqCarrigeNumber": "6666",
                "cameraFirstIpAddress": "192.168.1.16",
                "cameraSecondIpAddress": "192.168.1.17"
            }
        ]
    }

### Status: 200 Ok

---
<font size=4 style="color:lightGreen"> Создать новый поезд </font>

### `POST: http://ip:port/api/trains`

Тело запроса:

    {
        "name": "Train 1",
        "locomotiveOne": {
            "uniqCarrigeNumber": "1111",
            "cameraFirstIpAddress": "192.168.1.1",
            "cameraSecondIpAddress": "192.168.1.2"
        },
        "locomotiveTwo": {
            "uniqCarrigeNumber": "2222",
            "cameraFirstIpAddress": "192.168.1.3",
            "cameraSecondIpAddress": "192.168.1.4"
        },
        "carriges": [
            {
                "uniqCarrigeNumber": "3333",
                "cameraFirstIpAddress": "192.168.1.10",
                "cameraSecondIpAddress": "192.168.1.11"
            },
            {
                "uniqCarrigeNumber": "4444",
                "cameraFirstIpAddress": "192.168.1.12",
                "cameraSecondIpAddress": "192.168.1.13"
            },
            {
                "uniqCarrigeNumber": "5555",
                "cameraFirstIpAddress": "192.168.1.14",
                "cameraSecondIpAddress": "192.168.1.15"
            },
            {
                "uniqCarrigeNumber": "6666",
                "cameraFirstIpAddress": "192.168.1.16",
                "cameraSecondIpAddress": "192.168.1.17"
            }
        ]
    }

### Status: 200 Ok

---

<font size=4 style="color:lightGreen"> Удалить поезд по id </font>

### `DELETE: http://ip:port/api/trains{id}`


### Status: 204 NoContent

---


<font size=6 style="color:red"> **Маршруты:** </font>

<font size=4 style="color:lightGreen"> Получить все маршруты </font>
### `GET: http://ip:port/api/routes/`

Ответ:


    ????

### Status: 200 Ok

---



<font size=4 style="color:lightGreen"> Получить маршрут по Id </font>

### `GET: http://ip:port/api/routes/{id}`

Ответ:

    {
        "id": "6361b9dc-a46b-40c6-9be2-cc1d1008a1b2",
        "name": "Заельцовская - площадь Маркса",
        "Uows": [
            {
			    "Gender": "male",
				"SoundMessage": [
					{
					  "name": "Осторожно двери закрываются след. станция",
					  "url:"  "./assets/sounds/013 ОДЗСС 2.wav"
					},
					{
					  "name": "Гагаринская",
					  "url:"  "./assets/sounds/001 Гагаринская 2.wav"
					},
					{
					  "name": "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
					  "url:"  "./assets/sounds/019 УПБВУМПЛИПСД 2.wav"
					},
				],
				"Ticker":{
					"message": "следующая станция Гагаринская...",
				}
            },
			
            {
			    "Gender": "male",
				"SoundMessage": [
					{
					  "name": "Станция",
					  "url:"  "./assets/sounds/014 Станция 2.wav"
					},
					{
					  "name": "Гагаринская",
					  "url:"  "./assets/sounds/001 Гагаринская 2.wav"
					}
				],
				"Ticker":{
					"message": "станция Гагаринская...",
				}
            },
			
            {
			    "Gender": "male",
				"SoundMessage": [
					{
					  "name": "Осторожно двери закрываются след. станция",
					  "url:"  "./assets/sounds/013 ОДЗСС 2.wav"
					},
					{
					  "name": "Красный проспект",
					  "url:"  "./assets/sounds/001 Гагаринская 2.wav"
					},
					{
					  "name": "уважаемые пассажиры будте взимовежливы уступайте места пожилым людям, женщинам и пассажирам с детьми",
					  "url:"  "./assets/sounds/019 УПБВУМПЛИПСД 2.wav"
					}
				],
				"Ticker":{
					"message": "Следующая станция Красный проспект...",
				}
            },
			
            {
			    "Gender": "male",
				"SoundMessage": [
					{
					  "name": "СЕРВИСНОЕ СООБЩЕНИЕ Следите за сумками",
					  "url:"  "./assets/sounds/013 ОДЗСС 2.wav"
					},
				]
            },
        ]
    }

### Status: 200 Ok

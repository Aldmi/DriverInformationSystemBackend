# DriverInformationSystem Backend

#### Серверная часть системы информирования машиниста метро позда.

## REST wep api:

## **Поезда:**


### Получить все поезда 
### `GET: http://ip:port/api/trains/`

Ответ:

    [
        {
            "id": "487e916d-e936-46a7-aeb8-fe0fa74a6f21",
            "name": "Train 100",
            "locomotiveOne": {
                "uniqCarrigeNumber": "1111",
                "cameraIpAddress": [
                    "192.168.1.1",
                    "192.168.1.2",
                    "192.168.1.3",
                    "192.168.1.4"
                ]
            },
            "locomotiveTwo": {
                "uniqCarrigeNumber": "2222",
                "cameraIpAddress": [
                    "192.168.1.10",
                    "192.168.1.20",
                    "192.168.1.30",
                    "192.168.1.40"
                ]
            },
            "carriges": [
                {
                    "uniqCarrigeNumber": "6666",
                    "cameraFirstIpAddress": "192.168.1.16",
                    "cameraSecondIpAddress": "192.168.1.17"
                },
                {
                    "uniqCarrigeNumber": "5555",
                    "cameraFirstIpAddress": "192.168.1.14",
                    "cameraSecondIpAddress": "192.168.1.15"
                },
                {
                    "uniqCarrigeNumber": "3333",
                    "cameraFirstIpAddress": "192.168.1.10",
                    "cameraSecondIpAddress": "192.168.1.11"
                },
                {
                    "uniqCarrigeNumber": "4444",
                    "cameraFirstIpAddress": "192.168.1.12",
                    "cameraSecondIpAddress": "192.168.1.13"
                }
            ]
        }   
    ]



### Status: 200 Ok
---


### Получить поезд по Id
### `GET: http://ip:port/api/trains/{id}`

Ответ:

    {
        "id": "1d122a3e-4428-4a6f-a55b-11cdef4c50c6",
        "name": "Train 100",
        "locomotiveOne": {
            "uniqCarrigeNumber": "1111",
            "cameraIpAddress": [
                "192.168.1.1",
                "192.168.1.2",
                "192.168.1.3",
                "192.168.1.4"
            ]
        },
        "locomotiveTwo": {
            "uniqCarrigeNumber": "2222",
            "cameraIpAddress": [
                "192.168.1.10",
                "192.168.1.20",
                "192.168.1.30",
                "192.168.1.40"
            ]
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


### Создать новый поезд 
### `POST: http://ip:port/api/trains`

Тело запроса:

    {
        "name": "Train 100",
        "locomotiveOne": {
            "uniqCarrigeNumber": "1111",
            "CameraIpAddress": [
                "192.168.1.1",
                "192.168.1.2",
                "192.168.1.3",
                "192.168.1.4"
                ]
        },
        "locomotiveTwo": {
            "uniqCarrigeNumber": "2222",
            "CameraIpAddress": [
                "192.168.1.10",
                "192.168.1.20",
                "192.168.1.30",
                "192.168.1.40"
                ]
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


### Удалить поезд по id

### `DELETE: http://ip:port/api/trains{id}`


### Status: 204 NO content
---


### Поменять порядок вагонов
CarrigesNumberSeq - перечислить уникальные номера вагонов в нужном порядке.
### `PUT: http://ip:port/api/trains/updateCarrigesListSeq`

    {
        "IdTrain": "487e916d-e936-46a7-aeb8-fe0fa74a6f21",
        "CarrigesNumberSeq":[
            "6666",
            "5555",
            "4444",
            "3333"
        ]
    }

### Status: 200 Ok
---
---

## **Маршруты:**

### Получить все маршруты
### `GET: http://ip:port/api/routes/`

Ответ:


    ????

### Status: 200 Ok
---


### Получить маршрут по Id
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
---
---


## **Пользователи:**

### Добавить нового пользователя
### `POST: http://ip:port/persones`

    {
        "Name": "Евгений Викторович Иванов",
        "Password": "123456",
        "RoleName": "enginer"
    }


Ответ:
    GUID нового пользователя

### Status: 200 Ok
---

### Получить всех пользователей
### `GET: http://ip:port/persones`

    [
        {
            "id": "543d9a49-aab9-42fd-9321-04dd21fd3845",
            "name": "root",
            "password": "123456",
            "roleName": "admin"
        }
    ]


### Status: 200 Ok
---


### Удалить пользователя
### `DELETE: http://ip:port/persones/{id}`


### Status: 204 NO content
---
---


## **Версия:**

### Получить описание программы
### `POST: http://ip:port/_version`


Ответ:
    Ver: '1.0.0'	Date: '13 октября 2023 г.'	Description: 'Add CORS with default Policy'	 Git: 'b0faecfd702c6dd80e32463741924dcc541b8c83 [b0faecf]'	

### Status: 200 Ok
---
---


## **Login**

### Получить JWT.
### `POST: http://ip:port/login`

    {
        "Login":"root",
        "Password": "123456"
    }

Ответ:

    {
        "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicm9vdCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6ImFkbWluIiwibmJmIjoxNjk3MTk3MzgxLCJleHAiOjE2OTcyMDA5ODEsImlzcyI6IkRyaXZlckluZm9ybWF0aW9uU3lzdGVtQmFja2VuZCIsImF1ZCI6IkRyaXZlckluZm9ybWF0aW9uU3lzdGVtQ2xpZW50In0.nQGDON-UnQc3Qqv8gjjQmCpT43OmIYD6QKop5pIhA1M",
        "user_name": "root",
        "role_name": "admin",
        "generate_time": "2023-10-13T11:43:01Z",
        "expired_time": "2023-10-13T12:43:01Z"
    }
### Status: 200 Ok
---


## **Тест аутентификации**
### `GET: http://ip:port/authTest`

 
 `Вставить в Header запроса полученный JWT`
    
    Authorization:Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicm9vdCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6ImFkbWluIiwibmJmIjoxNjk3MTk3MzgxLCJleHAiOjE2OTcyMDA5ODEsImlzcyI6IkRyaXZlckluZm9ybWF0aW9uU3lzdGVtQmFja2VuZCIsImF1ZCI6IkRyaXZlckluZm9ybWF0aW9uU3lzdGVtQ2xpZW50In0.nQGDON-UnQc3Qqv8gjjQmCpT43OmIYD6QKop5pIhA1M


Ответ:
    401 Unauthorized - Если пользователь не авторизован
    

    Если авторизован то данные пользователя из JWT.
    {
        "auth": true,
        "login": "root",
        "role": "admin"
    }

### Status: 200 Ok
---

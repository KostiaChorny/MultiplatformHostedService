# MultiplatformHostedService

Установка windows сервиса

Настоятельно рекомендую паблишить решение перед установкой

```
sc create Service binpath="\Service\bin\Release\net471\publish\Service.exe"
sc start Service
sc stop Service
sc delete Service
```

Для установки в продакшене рекомендуют пользоваться InstallUtil

Установка демона linux (проверял на CentOS 7)

Добавить файл. `/etc/systemd/system/coreservice.service`

`coreservice` - имя сервиса и может быть произвольным


```
systemctl daemon-reload - обновляет конфигурацию
systemctl start coreservice - запускает сервис
systemctl stop coreservice - останавливает сервис
systemctl status coreservice -l - выводит статус и последние сообщения с консоли
```


Содержимое файла
```
[Unit]
Description=.NET Core Example Daemon

[Service]
User=root
WorkingDirectory=/usr/myservice/
Type=oneshotmc
RemainAfterExit=yes
ExecStart=/usr/bin/dotnet Service.dll --name Daemon
Restart=always
RestartSec=10
KillSignal=SIGINT

[Install]
WantedBy=multi-user.target 
```


Полезные ссылки и источники
1) https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-2.2
2) https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.2
3) https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.2
4) https://www.stevejgordon.co.uk/running-net-core-generic-host-applications-as-a-windows-service
5) https://www.stevejgordon.co.uk/asp-net-core-2-ihostedservice

Список пакетов, которые использованы в проекте (детальнее в .csproj файле)
1) Microsoft.Extensions.Configuration.CommandLine
2) Microsoft.Extensions.Configuration.Json
3) Microsoft.Extensions.Hosting
4) Microsoft.Extensions.Logging
5) Microsoft.Extensions.Logging.Console
6) Microsoft.Extensions.Logging.Debug
7) System.ServiceProcess.ServiceController

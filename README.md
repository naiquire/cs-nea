# Handwriting Analysis and Feedback Platform
Technical Solution for my Computer Science NEA
* AQA A-level Computer Science 7517
* Entry year 2026
* Mark: 74/75
* Language: C#
* Libraries: SignalR, Windows Forms

## Installation Guide
I do not intend on releasing this project as a full application, so installation requires building from source. The client application must be run on Windows since it uses Windows Forms. The server application is guaranteed to work on Windows and Arch Linux - other Linux distributions and MacOS are likely to work but are untested.

### Prerequisites
* [dotnet 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
* [dotnet framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)
* git

### Installation

Recommended to change working directory to a suitable folder: `Downloads`, `Documents`, etc
```
git clone https://github.com/naiquire/cs-nea
cd cs-nea
```
Set the environment variable `cs-nea-server = $(pwd)/server-app/server-app`


__IMPORTANT__ ~ the client can only connect to the server if they are running on the **same** device. (If you know what you are doing, installing NGINX can expose the server to the internet by configuring port forwarding on your router. I **highly** advise against exposing the server without NGINX for security reasons. To set up the server to use NGINX with autostart, **uncomment line 18 in Program.cs** and add the directory location of where `nginx.exe` has been installed to **line 29 of Program.cs**

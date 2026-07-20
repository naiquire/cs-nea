# Handwriting Analysis and Feedback Platform
Technical Solution for my Computer Science NEA
* AQA A-level Computer Science 7517
* Entry year 2026
* Mark: 74/75
* Language: C#
* Libraries: SignalR, Windows Forms

## Installation Guide
I do not intend on releasing this project as a full application, so installation requires building from source. The client application must be run on Windows since it uses Windows Forms. The server application is guaranteed to work on Windows and Linux - other operating systems are likely to work but are untested.

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

__SKIP TO [Execution](https://github.com/naiquire/cs-nea#execution) IF YOU DO NOT INTEND TO SETUP NGINX__
NGINX is not required for this project to work, however allows the server to be securely exposed to the internet and accessed from a device on a different LAN. First download NGINX and place it in a suitable directory - this does not have to be the same as the cloned repository. Use the following configuration:
```
listen 5252;
location /cs-nea {
  proxy_pass http://localhost:3900;
  proxy_set_header Upgrade $http_upgrade;
  proxy_set_header Connection $connection_upgrade;
}
```
Consult the [NGINX documentation](https://nginx.org/en/docs/) if you are unsure on how to configure NGINX.

* Change the socket on **line 84 of Program.cs** to use localhost instead of 0.0.0.0
* Uncomment **line 18 of Program.cs**
* Adjust **line 29 of Program.cs** to point at the directory which NGINX is installed to

### Execution

First, open both the server and client solutions in Visual Studio. Navigate to **client-app/menus/main.cs** and adjust the `address` property to match the non-routable IP of the device the server is running on. Examples of what this may look like are shown below:
```
public const string address = "http://localhost:3900/cs-nea";
public const string address = "http://192.168.0.24:3900/cs-nea";
```
If you have setup NGINX, then the IP should be changed to the routable IP of the server device, and the port number changed to 5252:
```
public const string address = "http://123.45.67.89:5252/cs-nea";
```



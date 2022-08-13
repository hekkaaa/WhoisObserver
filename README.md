# WhoisObserver
The program aggregates a number of whois servers. Creates the necessary request and interprets the response into a common object.

## Servers used:
- **[ip-api](https://ip-api.com/)**
- **[Ru-Center](https://www.nic.ru/whois/?searchWord=)**
- **[whois.ru](https://whois.ru/8.8.8.8)**

## Example
### Async version
```csharp
using WhoisObserver;
using WhoisObserver.Services.Model;
...

async void Test1()
{
    var st0 = new WhoisClient();
    var st1 = st0.GetResponceJsonAsync("ya.ru", ServersClientFamily.IpApi).Result;
    Console.WriteLine(st1.CountryCode);
}

Test1();
```

![image](https://user-images.githubusercontent.com/46771781/183240324-a46d1e0a-1fbc-4422-906c-02360a754186.png)


```csharp
using WhoisObserver;
using WhoisObserver.Services.Model;
...

async void Test2()
{
    var st0 = new WhoisClient();
    var st1 = st0.GetResponceModelAsync("13.228.116.142", ServersClientFamily.IpApi).Result;
    Console.WriteLine(st1.CountryCode);
}

Test2());
```

![image](https://user-images.githubusercontent.com/46771781/183470488-c0448b79-6e18-48d9-9506-98cf927d0901.png)


## Download
**[Nuget reference](https://www.nuget.org/packages/WhoisObserver)**


## Dependencies
- .NETStandard 2.0
- AutoMapper (>= 10.1.1)
- Newtonsoft.Json (>= 13.0.1)
- System.Net.Http.Json (>= 6.0.0)
- System.Text.Json (>= 6.0.5)

## Feedback and bug reports

Telegram: https://t.me/Hekkaaa

Email: silencemyalise@gmail.com

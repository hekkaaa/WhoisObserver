# WhoisObserver
The program aggregates a number of whois servers. Creates the necessary request and interprets the response into a common object.


### Async version
```csharp
using Whois.NET;
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


## Download
**[Nuget reference](#)**
**[Download it now](#)**

## Feedback and bug reports

Telegram: https://t.me/Hekkaaa

Email: silencemyalise@gmail.com

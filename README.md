# Weather demo

This is a very simple ASP.NET MVC application that makes use of the MetaWeather API found at https://www.metaweather.com/.

The program sticks to one location (Belfast) and shows the 5-day forecast from the current day onwards. It doesn't include detailed weather forecast for a particular day, it just copies from the website's example with the first page showing all of the weeks' forecast at a particular location (which is hardcoded in the code). 

It uses a very simple user authentication mode (standard Entity Framework) to allow only registered users to view the page with the weather forecast. The demo account is metaweatherdemo@metaweatherdemo.com with password Password1$ and is included in the source files (one of the .mdf files in App_Data folder).

The project was created with Visual Studio 2019.

![Screenshot example](https://raw.githubusercontent.com/GitHubGeorge/WeatherDemo/main/screenshot.jpg)

**Note**

In case you try to run it and it complains about bin/roslyn/csc.exe try and download a NuGet package called **Microsoft.CodeDom.Providers.DotNetCompilerPlatform.BinFix**

![Screenshot roslyn nuget package](https://raw.githubusercontent.com/GitHubGeorge/WeatherDemo/main/nuget_roslyn.jpg)

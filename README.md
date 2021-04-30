<img src="assets/banner.png" alt="Daily Desktop" height="300">

---

[![Version](https://img.shields.io/github/v/release/goodtrailer/daily-desktop.svg?color=green&style=flat-square)](https://github.com/goodtrailer/daily-desktop/releases/latest)
[![NuGet](https://img.shields.io/nuget/v/goodtrailer.DailyDesktop.Core.svg?color=steelblue&style=flat-square)](https://www.nuget.org/packages/goodtrailer.DailyDesktop.Core/)
[![CodeFactor](https://www.codefactor.io/repository/github/goodtrailer/daily-desktop/badge/main?style=flat-square)](https://www.codefactor.io/repository/github/goodtrailer/daily-desktop/overview/main)
[![License](https://img.shields.io/github/license/goodtrailer/soyokaze.svg?color=goldenrod&style=flat-square)](https://github.com/goodtrailer/soyokaze/blob/master/LICENSE)
[![Downloads](https://img.shields.io/github/downloads/goodtrailer/daily-desktop/total.svg?color=orange&style=flat-square)](https://somsubhra.github.io/github-release-stats/?username=goodtrailer&repository=daily-desktop&page=1&per_page=0)

Modular Windows desktop wallpaper updater that works daily at a set time. Wallpapers are provided by [IProvider](/DailyDesktop.Core/Providers/IProvider.cs) classes that are implemented in DLL modules. For example, look at [/DailyDesktop.Core.Providers.MTG/](/DailyDesktop.Core.Providers.MTG/), which takes wallpaper from the official Wizards of the Coast [website for Magic: The Gathering wallpaper](https://magic.wizards.com/en/articles/media/wallpapers).

This program is based on [a previous C# program I wrote](https://github.com/goodtrailer/MTG-Wallpaper-OTW) that only worked specifically for Magic: The Gathering, and was not remotely user-friendly. The original idea is based off of KDE Plasma's daily wallpaper addon/plugin where options included [Bing](https://www.bing.com), [Wikimedia Commons](https://commons.wikimedia.org/wiki/Commons:Picture_of_the_day), and [National Geographic](https://www.nationalgeographic.com/photo-of-the-day).

## Developing a Provider Module
To develop your own Daily Desktop provider modules, use the [NuGet package](https://www.nuget.org/packages/goodtrailer.DailyDesktop.Core/):
* PackageManager: `Install-Package goodtrailer.DailyDesktop.Core`
* dotnet: `dotnet add package goodtrailer.DailyDesktop.Core`

Then, implement the [IProvider](/DailyDesktop.Core/Providers/IProvider.cs) interface in a *public* class. For examples, check these [providers I already implemented](#implemented).

## Providers
#### Implemented
* Magic: The Gathering @ [/DailyDesktop.Core.Providers.MTG/](/DailyDesktop.Core.Providers.MTG/)

#### Planned
* Pixiv
* Bing
* Wikimedia Commons
* DeviantArt
* National Geographic
* Unsplash

## Blurred-fit Mode
Daily Desktop has an optional "blurred-fit" mode that fits the entire image within the wallpaper, then fills in the background with a blurred and enlarged version of the image itself, instead of using a solid color like Windows does. This is useful if a provider's source contains images that are of a vastly different aspect ratio than your primary display.

*Image credit:* [真夏に花が咲く](https://www.pixiv.net/en/artworks/88058753)*, illustrated by* [流感中](https://www.pixiv.net/en/users/2712686)
| Original | Blurred-Fit |
| -------- | ----------- |
| ![Original](assets/comparison-original.png) | ![Blurred-Fit](assets/comparison-blurredfit.png) |

| Windows 10 Fill | Windows 10 Fit |
| --------------- | -------------- |
| ![temp](assets/comparison-fill.png) | ![Blurred-Fit](assets/comparison-fit.png) |

## Third-Party Libraries
* **[MIT]** [mdymel/superfastblur](https://github.com/mdymel/superfastblur) used @ [/DailyDesktop.Task/](/DailyDesktop.Task/)

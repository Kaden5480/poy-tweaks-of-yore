# poy-tweaks-of-yore
![Code size](https://img.shields.io/github/languages/code-size/Kaden5480/poy-tweaks-of-yore?color=5c85d6)
![Open issues](https://img.shields.io/github/issues/Kaden5480/poy-tweaks-of-yore?color=d65c5c)
![License](https://img.shields.io/github/license/Kaden5480/poy-tweaks-of-yore?color=a35cd6)

A quality of life mod for
[Peaks of Yore](https://store.steampowered.com/app/2236070/).

# Overview
- [Features](#features)
- [Installing](#installing)
    - [BepInEx](#bepinex)
    - [MelonLoader](#melonloader)
- [Building from source](#building-from-source)
    - [Dotnet](#dotnet-build)
    - [Visual Studio](#visual-studio-build)
    - [Custom game locations](#custom-game-locations)

# Features
- Skip cleaning items
- Always allows skipping to the next peak
- No longer detach rope when looking at it
- No longer interact with the pocket watch when looking at it

# Installing
## BepInEx
If you haven't installed BepInEx yet, follow the install instructions
[here](https://github.com/Kaden5480/modloader-instructions#bepinex).

### Tweaks of Yore
- Download the latest BepInEx release
[here](https://github.com/Kaden5480/poy-tweaks-of-yore/releases).
- The compressed zip will contain a `plugins` directory.
- Copy the files in `plugins` to `BepInEx/plugins` in your game directory.

## MelonLoader
If you haven't installed MelonLoader yet, follow the install instructions here:
- [Windows](https://github.com/Kaden5480/modloader-instructions#melonloader-windows)
- [Linux](https://github.com/Kaden5480/modloader-instructions#melonloader-linux)

### Tweaks of Yore
- Download the latest release
[here](https://github.com/Kaden5480/poy-tweaks-of-yore/releases).
- The compressed zip file will contain a `Mods` directory.
- Copy the files from `Mods` to `Mods` in your game directory.

# Building from source
Whichever approach you use for building from source, the resulting
plugin/mod can be found in `bin/`.

The following configurations are supported:
- Debug-BepInEx
- Release-BepInEx
- Debug-MelonLoader
- Release-MelonLoader

## Dotnet build
To build with dotnet, run the following command, replacing
<configuration> with the desired value:
```sh
dotnet build -c <configuration>
```

## Visual Studio build
To build with Visual Studio, open TweaksOfYore.sln and build by pressing ctrl + shift + b,
or by selecting Build -> Build Solution.

## Custom game locations
If you installed Peaks of Yore in a custom game location, you may require
an extra file to configure the build so it knows where to find the Peaks of Yore game
libraries.

The file must be in the root of this repository and must be called "GamePath.props".

Below gives an example where Peaks of Yore is installed on the F drive:
```xml
<Project>
  <PropertyGroup>
    <GamePath>F:\Games\Peaks of Yore</GamePath>
  </PropertyGroup>
</Project>
```

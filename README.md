# InfiniteAxis Utility System for .Net

The InfiniteAxis Utility System is a system designed by Dave Mark and Mike Lewis as part of work at [Intrinsic Algorithm](https://www.intrinsicalgorithm.com/). This repository provides a set of libraries as well as a WIP behavior editor.

## Design

TODO

## Libraries

Multiple versions of the library are provided to support usage by multiple game engines such as Unity and Godot. The primary codebase is the .NetStandard version which the . NetFramework version replaces some files due to C# language incompatibilities.

A single package is published on the [Github Nuget Feed](https://github.com/SonarBeserk/iaus-net/pkgs/nuget/IAUS-Net) that will install the .NetFramework or .NetStandard version as needed automatically. You can setup the [Nuget Feed by following the Github Docs](https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry).

### Core Library

The Core library targets .Net 8.0. This version is supported by both [Godot's .Net Version](https://godotengine.org/article/platform-state-in-csharp-for-godot-4-2/) and [Unity projects targeting newer api versions](https://learn.microsoft.com/en-us/visualstudio/gamedev/unity/unity-scripting-upgrade).

### Framework Library

The Framework library targets .NetFramework 4.8 which is the most recent major version available. This version is intended to be used by [Unity projects targeting older api versions](https://learn.microsoft.com/en-us/visualstudio/gamedev/unity/unity-scripting-upgrade). Due to the Framework dependency a [lower C# version is supported](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning) which can limit some features available when developing the library.

## Editor

The editor provided is based on [Curvature by Mike Lewis](https://github.com/apoch/curvature). It is written in C# using [Avalonia](https://github.com/AvaloniaUI/Avalonia) to make it a cross platform desktop app.

## Reference Material

- [Building a Better Centaur AI](https://gdcvault.com/play/1021848/Building-a-Better-Centaur-AI)
- [Curvature Wiki](https://github.com/apoch/curvature/wiki/Utility-Theory-Crash-Course)
- [Behavior Mathematics for Game AI](https://www.amazon.com/Behavioral-Mathematics-Game-AI-Applied/dp/1584506849)
# OpenXBL.NET
An asynchronous wrapper library for the https://xbl.io API written in C# for .NET Framework 4.7.1+.

Assembly: Kalakoi.Xbox.OpenXBL.dll

Namespace: Kalakoi.Xbox.OpenXBL

Please check src/XboxConnection.cs for outward facing methods.

It is required to obtain an API key from https://xbl.io before running the provided methods.
After obtaining your API key you can use it by running the XboxConnection.SetApiKey(key) method inside your program before running the other methods.

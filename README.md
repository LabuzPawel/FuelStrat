# FuelStrat - Fuel and strategy for Assetto Corsa Competizione

This is an application made in Visual Studio 2022 using C# Windows Form for calculating fuel and pit strategy in Assetto Corsa Competizione. 

Originally project early version was created in python using tkinter GUI. Old script can be found in 'FCalcACC\python_old_script'.

Release version requieres .NET 8.0 Framework to work (https://dotnet.microsoft.com/en-us/download/dotnet/8.0). Larger size exe release that is independent from .NET version can be found in 'FuelStrat/self_contained'.

Application is creating a 'FuelStrat_data.json' at first start up and 'FuelStrat_saved_strats.json' after clicking on 'Save / Load strategy' button. Those files stores user data.

Plans for 1.0 version:
- Improved recent stint list with more options and access to each lap
- Move all files that are being created and stores data to documents
- Create another file or solution that will store last state of the app

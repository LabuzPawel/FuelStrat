# FuelStrat - Fuel and strategy for Assetto Corsa Competizione

This is an application made in Visual Studio 2022 using C# Windows Form for calculating fuel and pit strategy in Assetto Corsa Competizione (ACC). 

Originally project early version was created in python using tkinter GUI. Old script can be found in 'FCalcACC\python_old_script'.

Help section at the top of the app shows detailed information how it works.

![Alt text](/FuelStrat/other_forms/help_pics/full_app.png)
***
## Key features:
- **'AUTO'** button that will be enabled whenever ACC session is 'Race'. Clicking this button will fill all the information about the car, track and race and calculate fuel. If there will be a mandatory pit stop, additional window will show up asking for desired pit stop option. **TL:DR** maximum of 2 clicks to get all the info about fuel and pit stops.
- Recent stints list that shows stints that were recorded when app was running. Stints can be edited and used to accurately calculate fuel.
- Save / Load strategy for future usage
- In 'Pit Stop Strategy' panel user can move pit stop timing earlier/later and see how much fuel is needed for each stint
- FuelStart is very flexible and works also with limited amount of information like car and track. 

***
## Future plans:
Current 1.0 version completed most of the features that were planned for this application. 
There are two things missing that I would want to add and improve. Right now app detects invalid laps only in practice session. 
There is a possibility to read a pit window but couldnt make it work. Most of those problems probably lies in SharedMemory/RecentSessions that needs to be completely change.

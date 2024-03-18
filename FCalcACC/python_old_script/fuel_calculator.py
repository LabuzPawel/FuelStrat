import tkinter as tk
from tkinter import messagebox
import time
from os import __file__, path, getcwd, remove

fuel_calc = tk.Tk()
fuel_calc.title('FUEL CALCULATOR FOR ACC BY FINOOR')
fuel_calc.minsize(420, 585)

# ERROR CLASSES

class RaceLenghtError (Exception):
    "Raised when race duration is less or equal 0"
    pass

class FuelError (Exception):
    "Raised when fuel is less or equal 0"
    pass

class DriverLapTimeError (Exception):
    "Raised when driver lap time is less or equal 0"
    pass

# HELP

help_text = 'This is fuel calculator for Assetto Corsa Competizione and it calculates fuel needed for the race.\n\n\
1. For basic calculation it requiers only: Lap time, Fuel Per Lap, Race Lenght\n\
2. Car and track selection gives option to load and save data\n\
3. Right now refuel in pit stop strategy have only two options: with tyre change or fixed refuel'


### CAR TRACK DEFAULT DATA ###

default_car_track_fuel = {
    'Ferrari 296 GT3 2023': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Porsche 992 GT3 R 2023': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Lamborghini Huracan Evo2 GT3 2023': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'McLaren 720S GT3 Evo 2023': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Aston Martin V8 Vantage GT3 2019': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Audi R8 LMS Evo GT3 2019': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Audi R8 LMS Evo II GT3 2022': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Bentley Continental GT3 2018': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'BMW M4 GT3 2022': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'BMW M6 GT3 2017': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Ferrari 488 GT3 Evo 2020': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Lamborghini Huracan Evo GT3 2019': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'McLaren 720S GT3 2019': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Mercedes AMG GT3 2020': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Porsche 911 II GT3 R 2019': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Aston Martin V12 Vantage GT3 2013': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Audi R8 LMS GT3 2015': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Bentley Continental GT3 2015': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Emil Frey Jaguar GT3 2012': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Ferrari 488 GT3 2018': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Honda NSX GT3 2017': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Honda NSX Evo GT3 2019': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Lamborghini Huracan GT3 2015': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Lexus RC F GT3 2016': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'McLaren 650S GT3 2015': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Mercedes AMG GT3 2015': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Nissan GTR Nismo GT3 2015': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Nissan GTR Nismo GT3 2018': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Porsche 911 GT3 R 2018': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Reiter Engineering R-EX GT3 2017': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Alpine A110 GT4 2018': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Aston Martin V8 Vantage GT4 2018': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Audi R8 LMS GT4 2018': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'BMW M4 GT4 2018': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Chevrolet Camaro R GT4 2017': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Ginetta G55 GT4 2012': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'KTM X-Bow GT4 2016': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Maserati Granturismo MC GT4 2016': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Mclaren 570S GT4 2016': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Mercedes ANG GT4 2016': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Porsche 718 Cayman GT4 Clubsport 2019': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Audi R8 LMS GT2': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'KTM X-Bow GT2': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Maserati MC20 GT2': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Mercedes AMG GT2': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Porsche 991 II GT2 RS CS Evo': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Porsche 935 GT2': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Ferrari 488 Challenge Evo 2020': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Lamborghini Huracan Super Trofeo 2015': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Lamborghini Huracan Super Trofeo Evo 2 2021': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Porsche 911 II GT3 Cup 2017': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'Porsche 911 GT3 Cup (992) 2021': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}, 
    'BMW M2 CS 2020': {'Imola': 0.0, 'Barcelona': 0.0, 'Brands Hatch': 0.0, 'COTA': 0.0, 'Doningto Park': 0.0, 'Hungaroring': 0.0, 'Indianapolis': 0.0, 'Kyalami': 0.0, 'Laguna Seca': 0.0, 'Misano': 0.0, 'Monza': 0.0, 'Mount Panorama': 0.0, 'Nurburgring': 0.0, 'Oulton Park': 0.0, 'Paul Ricard': 0.0, 'Silverstone': 0.0, 'Snetterton': 0.0, 'Spa-Francorchamps': 0.0, 'Suzuka': 0.0, 'Watkins Glen': 0.0, 'Zandvoort': 0.0, 'Zolder': 0.0}
    }
default_driver_laptimes = {
    'Imola':100,
    'Barcelona':103.5,
    'Brands Hatch':83,
    'COTA':125.5,
    'Doningto Park':86,
    'Hungaroring':104,
    'Indianapolis':95.8,
    'Kyalami':100.1,
    'Laguna Seca':81.8,
    'Misano':92.8,
    'Monza':107,
    'Mount Panorama':119.5,
    'Nurburgring':113.1,
    'Oulton Park':92.8,
    'Paul Ricard':112.8,
    'Silverstone':117.7,
    'Snetterton':106,
    'Spa-Francorchamps':135.8,
    'Suzuka':118.7,
    'Watkins Glen':103,
    'Zandvoort':95,
    'Zolder':87.6
}
tracks_dict={
    'Imola':74.154,
    'Barcelona':64.406,
    'Brands Hatch':54.490,
    'COTA':68.556,
    'Doningto Park':55.318,
    'Hungaroring':60.965,
    'Indianapolis':90.273,
    'Kyalami':55.251,
    'Laguna Seca':63.164,
    'Misano':73.517,
    'Monza':64.810,
    'Mount Panorama':59.795,
    'Nurburgring':61.910,
    'Oulton Park':50.369,
    'Paul Ricard':64.580,
    'Silverstone':58.849,
    'Snetterton':52.503,
    'Spa-Francorchamps':106.403,
    'Suzuka':61.349,
    'Watkins Glen':68.886,
    'Zandvoort':49.318,
    'Zolder':66.386
}
cars = ['Ferrari 296 GT3 2023',
        'Porsche 992 GT3 R 2023',
        'Lamborghini Huracan Evo2 GT3 2023',
        'McLaren 720S GT3 Evo 2023',
        'Aston Martin V8 Vantage GT3 2019',
        'Audi R8 LMS Evo GT3 2019',
        'Audi R8 LMS Evo II GT3 2022',
        'Bentley Continental GT3 2018',
        'BMW M4 GT3 2022',
        'BMW M6 GT3 2017',
        'Ferrari 488 GT3 Evo 2020',
        'Lamborghini Huracan Evo GT3 2019',
        'McLaren 720S GT3 2019',
        'Mercedes AMG GT3 2020',
        'Porsche 911 II GT3 R 2019',
        'Aston Martin V12 Vantage GT3 2013',
        'Audi R8 LMS GT3 2015',
        'Bentley Continental GT3 2015',
        'Emil Frey Jaguar GT3 2012',
        'Ferrari 488 GT3 2018',
        'Honda NSX GT3 2017',
        'Honda NSX Evo GT3 2019',
        'Lamborghini Huracan GT3 2015',
        'Lexus RC F GT3 2016',
        'McLaren 650S GT3 2015',
        'Mercedes AMG GT3 2015',
        'Nissan GTR Nismo GT3 2015',
        'Nissan GTR Nismo GT3 2018',
        'Porsche 911 GT3 R 2018',
        'Reiter Engineering R-EX GT3 2017'
        ]
cars.sort()
cars4 = ['Alpine A110 GT4 2018',
         'Aston Martin V8 Vantage GT4 2018',
         'Audi R8 LMS GT4 2018',
         'BMW M4 GT4 2018',
         'Chevrolet Camaro R GT4 2017',
         'Ginetta G55 GT4 2012',
         'KTM X-Bow GT4 2016',
         'Maserati Granturismo MC GT4 2016',
         'Mclaren 570S GT4 2016',
         'Mercedes ANG GT4 2016',
         'Porsche 718 Cayman GT4 Clubsport 2019']
cars2 = ['Audi R8 LMS GT2',
         'KTM X-Bow GT2',
         'Maserati MC20 GT2',
         'Mercedes AMG GT2',
         'Porsche 991 II GT2 RS CS Evo',
         'Porsche 935 GT2']
carsCUP = ['Ferrari 488 Challenge Evo 2020',
           'Lamborghini Huracan Super Trofeo 2015',
           'Lamborghini Huracan Super Trofeo Evo 2 2021',
           'Porsche 911 II GT3 Cup 2017',
           'Porsche 911 GT3 Cup (992) 2021']
carM2 = 'BMW M2 CS 2020'
tracks=[]
for key in tracks_dict:
    tracks.append(key)

# TXT FILE WITH DEFAULT DATA
    
file_dir = getcwd() + '\\data.txt'

def data_set_up():    
    check_file = path.isfile(file_dir)
    if check_file == True:
        pass
    elif check_file == False:
        data = open('data.txt', 'w')
        data.write(str(default_car_track_fuel))
        data.write('\n|')
        data.write(str(default_driver_laptimes))
        data.close()
data_set_up()

def load_file():
    data = open(file_dir, 'r')
    data_string = str(data.read())
    car_track_fuel = data_string.split('\n|', 1)[0]
    driver_lap_time = data_string.split('|', 1)[-1]
    car_track_fuel = car_track_fuel.replace('[','')
    driver_lap_time = driver_lap_time.replace(']', '')

    try:
        default_car_track_fuel = eval(car_track_fuel)
    except:
        messagebox.showerror('ERROR', 'Error during data loading')
        reset_data = messagebox.askquestion('Data reset', 'Do you want to reset data?')
        if reset_data == 'yes':
            data.close()
            remove('data.txt')
            data_set_up()
            return

    try:
        default_driver_laptimes = eval(driver_lap_time)
    except:
        messagebox.showerror('ERROR', 'Error during data loading')
        reset_data = messagebox.askquestion('Data reset', 'Do you want to reset data?')
        if reset_data == 'yes':
            data.close()
            remove('data.txt')
            data_set_up()
            return

    driver_fuel.delete(0, 'end')
    driver_fuel.insert(0, default_car_track_fuel[car_select.get()][track_select.get()])
    m_driver.delete(0, 'end')
    s_driver.delete(0, 'end')
    default_driver_laptimes_secs = float(default_driver_laptimes.get(track_select.get()))
    m_driver_load = default_driver_laptimes_secs // 60
    s_driver_load = default_driver_laptimes_secs - (m_driver_load * 60)
    s_driver_load = round(s_driver_load, 3)
    m_driver.insert(0, int(m_driver_load))
    s_driver.insert(0, s_driver_load)
    data.close()

def load_data_track_select():
    if car_select.get() == 'CAR':
        pass
    else:
        load_file()

def load_data_car_select():
    if track_select.get() == 'TRACK':
        pass
    else:
        load_file()

def load_file_button():
    if car_select.get() == 'CAR' or track_select.get() == 'TRACK':
        messagebox.showwarning('WARNING', 'Select car and track')
    else:
        load_file()
    
def save_file():
    data = open(file_dir, 'w')
    default_driver_laptimes.update({track_select.get() : ((int(m_driver.get()) * 60) + float(s_driver.get().replace(',','.')))})
    default_car_track_fuel.update({})
    if car_select.get() in default_car_track_fuel:
        default_car_track_fuel[car_select.get()][track_select.get()] = driver_fuel.get().replace(',','.')
    data.write(str(default_car_track_fuel))
    data.write('\n|')
    data.write(str(default_driver_laptimes))
    data.close()
    
### INPUT FRAME ###


car_select = tk.StringVar(value='CAR')
track_select = tk.StringVar(value='TRACK')
input_frame = tk.LabelFrame(fuel_calc, text='Input Data', relief='ridge')
top = None

# CAR SELECT

def accept(top, car_listbox):
    global car_select
    car_select_index = car_listbox.curselection()
    if car_select_index:
        car_select.set(cars[car_select_index[0]])
        load_data_car_select()
        top.destroy()

def accept4(top, car_listbox):
    global car_select
    car_select_index = car_listbox.curselection()
    if car_select_index:
        car_select.set(cars4[car_select_index[0]])
        load_data_car_select()
        top.destroy()

def accept2(top, car_listbox):
    global car_select
    car_select_index = car_listbox.curselection()
    if car_select_index:
        car_select.set(cars2[car_select_index[0]])
        load_data_car_select()
        top.destroy()

def acceptC(top, car_listbox):
    global car_select
    car_select_index = car_listbox.curselection()
    if car_select_index:
        car_select.set(carsCUP[car_select_index[0]])
        load_data_car_select()
        top.destroy()

def top_car():
    global top
    top = tk.Toplevel(fuel_calc)
    car_listbox = tk.Listbox(top, height=30, width=35)
    for item in cars:
        car_listbox.insert(tk.END, item)
    car_listbox.pack()
    tk.Button(top, text='ACCEPT', command=lambda: accept(top, car_listbox)).pack(fill='x')

def top_car4():
    global top
    top = tk.Toplevel(fuel_calc)
    car_listbox4 = tk.Listbox(top, height=11, width=35)
    for item in cars4:
        car_listbox4.insert(tk.END, item)
    car_listbox4.pack()
    tk.Button(top, text='ACCEPT', command=lambda: accept4(top, car_listbox4)).pack(fill='x')

def top_car2():
    global top
    top = tk.Toplevel(fuel_calc)
    car_listbox2 = tk.Listbox(top, height=6, width=35)
    for item in cars2:
        car_listbox2.insert(tk.END, item)
    car_listbox2.pack()
    tk.Button(top, text='ACCEPT', command=lambda: accept2(top, car_listbox2)).pack(fill='x')

def top_carC():
    global top
    top = tk.Toplevel(fuel_calc)
    car_listboxC = tk.Listbox(top, height=6, width=35)
    for item in carsCUP:
        car_listboxC.insert(tk.END, item)
    car_listboxC.pack()
    tk.Button(top, text='ACCEPT', command=lambda: acceptC(top, car_listboxC)).pack(fill='x')


tk.Label(input_frame, text='Choose The Car:', relief='ridge', padx=12, pady=4).place(x=10, y=10)
tk.Button(input_frame, text='GT3', command=top_car, padx=6, bg='#D5D8DC').place(x=139, y=10)
tk.Button(input_frame, text='GT4', command=top_car4, padx=6, bg='#D6EAF8').place(x=188, y=10)
tk.Button(input_frame, text='GT2', command=top_car2, padx=6, bg='#F6DDCC').place(x=237, y=10)
tk.Button(input_frame, text='GTC', command=top_carC, padx=6, bg='#F6DDCC').place(x=286, y=10)
tk.Button(input_frame, text='TCX', command=lambda: car_select.set(carM2), padx=6, bg='#DCF6FF').place(x=336, y=10)
tk.Label(input_frame, textvariable=car_select, relief='ridge', width=51, padx=5).place(x=10, y=50)



# TRACK SELECT

def accept_track(top, track_listbox):
    global track_select
    track_select_index = track_listbox.curselection()
    if track_select_index:
        track_select.set(tracks[track_select_index[0]])
        load_data_track_select()
        top.destroy()

def top_track():
    global top
    top = tk.Toplevel(fuel_calc)
    track_listbox = tk.Listbox(top, height=22)
    for item in tracks:
        track_listbox.insert(tk.END, item)
    track_listbox.pack()
    tk.Button(top, text='ACCEPT', command=lambda: accept_track(top, track_listbox)).pack(fill='x')

tk.Button(input_frame, text='Choose The Track', command=top_track, width=51, padx=3, bg='#D5D8DC').place(x=10, y=82)
tk.Label(input_frame, textvariable=track_select, relief='ridge', width=51, padx=5).place(x=10, y=122)


# DRIVER LAP TIME SELECT

lap_times = tk.LabelFrame(input_frame, text='Lap Time', pady=2, padx=2)

tk.Label(lap_times, text='Min', width=8).grid(column=0 , row=0, padx=2, pady=2)
m_driver = tk.Spinbox(lap_times, from_=1, to=2, width=4, justify='center')
m_driver.grid(column=0 ,row=1, padx=2, pady=2)
tk.Label(lap_times, text='Sec', width=8).grid(column=1 , row=0, padx=2, pady=2)
s_driver = tk.Spinbox(lap_times, from_=0, to=59.999, width=8, justify='center', increment=0.001)
s_driver.grid(column=1 ,row=1, padx=2, pady=2)

lap_times.place(x=10, y=150)

# FUEL PER LAP

fuel = tk.LabelFrame(input_frame, text='Fuel Per Lap', pady=2, padx=2)

tk.Label(fuel, text='L').grid(column=1 , row=0, padx=2, pady=1)
driver_fuel = tk.Spinbox(fuel, from_=0.0, to=5.0, increment=0.01, justify='center', width=5)
driver_fuel.grid(column=1 , row=1, padx=2, pady=2)
tk.Label(fuel, text=' ').grid(column=0 , row=1, padx=2, pady=2)
tk.Label(fuel, text='').grid(column=2 , row=1, padx=2, pady=2)
fuel.place(x=165, y=150)

# RACE LENGHT

race_lenght = tk.LabelFrame(input_frame, text='Race Lenght', pady=2, padx=2)

tk.Label(race_lenght, text='H', width=7).grid(column=0 , row=0, padx=2, pady=2)
h = tk.Spinbox(race_lenght, from_=0, to=24, width=4, justify='center')
h.grid(column=0 ,row=1, padx=2, pady=2)
tk.Label(race_lenght, text='Min', width=7).grid(column=1 , row=0, padx=2, pady=2)
m = tk.Spinbox(race_lenght, from_=0, to=59, width=4, justify='center')
m.grid(column=1 ,row=1, padx=2, pady=2)

race_lenght.place(x=256, y=150)

# PIT STOPS RULES

def on_off_refuel():
    if refuel_var.get():
        tyre_change.select()
        tyre_change.config(state='disabled')
        fixed.config(state='normal')
    else:
        fixed.deselect()
        if tyre_change_var.get():
            tyre_change.toggle()
        tyre_change.config(state='normal')
        fixed.config(state='disable')


pit_rules = tk.LabelFrame(input_frame, text='Pit Stops Rules')

fixed_var = tk.IntVar()
refuel_var = tk.IntVar()
tyre_change_var = tk.IntVar()

tk.Label(pit_rules, text='Number Of Pit Stops').grid(column=0 , row=0)
pits = tk.Spinbox(pit_rules, from_=0, to=30, width=3, justify='center')
pits.grid(column=1 , row=0)

tyre_change = tk.Checkbutton(pit_rules, text='Tyre Change', variable=tyre_change_var)
tyre_change.grid(column= 0, row=1)
fixed = tk.Checkbutton(pit_rules, text='Fixed Refuel', variable=fixed_var, state='disabled')
fixed.grid(column= 3,row=1)
refuel = tk.Checkbutton(pit_rules, text='Refuel', pady=2, variable=refuel_var, command=on_off_refuel)
refuel.grid(column= 3, row=0)

pit_rules.place(x=10 ,y=230)

# FORMATION

formation_lap = tk.LabelFrame(input_frame, text='Formation Lap')

f_list = tk.Listbox(formation_lap, selectmode='single', width=11, height=2, justify='center')
f_list.insert(0,'FULL')
f_list.insert(1,'SHORT')
f_list.grid(column=0 ,row=0, padx=7, pady=8)

formation_lap.place(x=280 ,y=230, width=101)


input_frame.place(x=10, y=10, width=400, height=340)


### CALCULATE ###

race_duration = tk.StringVar(value='00:00:00')
fuel_for_race = tk.StringVar(value='00')
one_less_lap = tk.StringVar(value='0:00.000')
one_more_lap = tk.StringVar(value='0:00.000')
one_less_lap_fuel = tk.StringVar(value='00')
one_more_lap_fuel = tk.StringVar(value='00')
formation_lap_set = 'FULL'
race_laps_var = tk.StringVar(value='Race duration(0 Laps):')

def back_to_start_values():
    global race_duration
    global fuel_for_race
    global one_less_lap
    global one_more_lap
    global one_less_lap_fuel
    global one_more_lap_fuel
    global formation_lap_set
    global race_laps_var
    race_duration = tk.StringVar(value='00:00:00')
    fuel_for_race = tk.StringVar(value='00')
    one_less_lap = tk.StringVar(value='0:00.000')
    one_more_lap = tk.StringVar(value='0:00.000')
    one_less_lap_fuel = tk.StringVar(value='00')
    one_more_lap_fuel = tk.StringVar(value='00')
    formation_lap_set = 'FULL'
    race_laps_var = tk.StringVar(value='Race duration(0 Laps):')

def calculate():
    try:
        global how_many_laps
        global number_of_pits
        pits_value = int(pits.get())
        if pits_value < 0:
            pits_value = '0'
            pits.delete(0, 'end')
            pits.insert(0, '0')
        number_of_pits = int(pits_value)
        if_refuel= refuel_var.get()
        if_fixed_refuel = fixed_var.get()
        if track_select.get() == 'TRACK':
            if if_refuel == 1 and if_fixed_refuel == 0:
                time_lost_in_pits_value = 62
            elif if_refuel == 1 and if_fixed_refuel == 1:
                time_lost_in_pits_value = 57
            elif if_refuel == 0 and if_fixed_refuel == 0:
                time_lost_in_pits_value = 62                
        else:
            if if_refuel == 1 and if_fixed_refuel == 0:
                time_lost_in_pits_value = tracks_dict.get(track_select.get())
            elif if_refuel == 1 and if_fixed_refuel == 1:
                time_lost_in_pits_value = tracks_dict.get(track_select.get()) - 5
            elif if_refuel == 0 and if_fixed_refuel == 0:
                time_lost_in_pits_value = tracks_dict.get(track_select.get())     
        time_lost_in_pits = time_lost_in_pits_value * (number_of_pits)

        #RACE DURATION
        global race_duration
        race_lenght_secs = (int(h.get())*3600)+(int(m.get())*60)
        if race_lenght_secs <= 0:
            raise RaceLenghtError
        h.config(background='#ffffff')
        m.config(background='#ffffff')
        s_driver_value = s_driver.get()
        s_driver_value = s_driver_value.replace(',', '.')
        s_driver.delete(0, 'end')
        s_driver.insert(0, s_driver_value)
        driver_lap_time_secs = (float(m_driver.get())*60)+float(s_driver_value)
        if driver_lap_time_secs <= 0:
            raise DriverLapTimeError
        m_driver.config(background='#ffffff')
        s_driver.config(background='#ffffff')
        race_duration_in_secs = time_lost_in_pits
        how_many_laps = 0
        while race_lenght_secs > race_duration_in_secs:
            race_duration_in_secs = race_duration_in_secs + driver_lap_time_secs
            how_many_laps += 1
        race_laps = 'Race duration(' + str(how_many_laps) + ' Laps):'
        race_laps_var.set(value=race_laps)
        race_duration_time = time.strftime('%H:%M:%S', time.gmtime(race_duration_in_secs))
        race_duration.set(value=race_duration_time)

        #LAP TIME -1 +1
        one_more_lap_sec = driver_lap_time_secs - ((race_duration_in_secs - race_lenght_secs) / how_many_laps)
        one_more_lap_min = int(one_more_lap_sec // 60)
        one_more_lap_sec = one_more_lap_sec - (one_more_lap_min * 60)
        one_more_lap_sec = (f'{one_more_lap_sec:.3f}')
        one_more_lap_time = str(one_more_lap_min) + ':' + str(one_more_lap_sec)
        one_more_lap.set(value=one_more_lap_time)

        one_less_lap_sec = (race_lenght_secs-time_lost_in_pits) / (how_many_laps - 1)
        one_less_lap_min = int(one_less_lap_sec // 60)
        one_less_lap_sec = one_less_lap_sec - (one_less_lap_min * 60)
        one_less_lap_sec = (f'{one_less_lap_sec:.3f}')
        one_less_lap_time = str(one_less_lap_min) + ':' + str(one_less_lap_sec)
        one_less_lap.set(value=one_less_lap_time)

        #FUEL FOR RACE
        global fuel_for_race
        global formation_lap_set
        global brake_draggin_f
        global brake_draggin_s
        global driver_fuel_value
        formation_lap_set_index = f_list.curselection()
        if len(formation_lap_set_index) == 0:
            f_list.selection_set(first=0)
            formation_lap_set_index = f_list.curselection()
        formation_lap_set = f_list.get(formation_lap_set_index)
        brake_draggin_f = 0
        brake_draggin_s = 0
        driver_fuel_value = driver_fuel.get()
        driver_fuel_value = driver_fuel_value.replace(',','.')
        driver_fuel.delete(0, 'end')
        driver_fuel.insert(0, driver_fuel_value)
        if float(driver_fuel_value) <= 0:
            raise FuelError
        driver_fuel.config(background='#ffffff')
        if formation_lap_set == 'FULL':
            how_many_laps = how_many_laps + 1
            brake_draggin_f = (float(driver_fuel_value))*0.1
        elif formation_lap_set == 'SHORT':
            brake_draggin_s = (float(driver_fuel_value))*0.25       
        fuel_needed_dec = (float(how_many_laps) * (float(driver_fuel_value)+0.01))+brake_draggin_f+brake_draggin_s
        if fuel_needed_dec % 1 != 0:
            fuel_needed = str(int(fuel_needed_dec + 1))
        fuel_for_race.set(value=fuel_needed)

        #FUEL +1-1
        one_more_fuel = fuel_needed_dec + (float(driver_fuel_value)+0.01)
        if one_more_fuel % 1 != 0:
            one_more_fuel = str(int(one_more_fuel + 1))
        one_more_lap_fuel.set(value=one_more_fuel)

        one_less_fuel = fuel_needed_dec - (float(driver_fuel_value)+0.01)
        if one_less_fuel% 1 != 0:
            one_less_fuel = str(int(one_less_fuel + 1))
        one_less_lap_fuel.set(value=one_less_fuel)

        pit_button.config(state='normal')

    except RaceLenghtError:
        messagebox.showerror('ERROR', 'Race lenght cannot be lower or equal 0')
        h.config(background='#ea9999')
        m.config(background='#ea9999')
        back_to_start_values()
    
    except FuelError:
        messagebox.showerror('ERROR', 'Fuel cannot be lower or equal 0')
        driver_fuel.config(background='#ea9999')
        back_to_start_values()
    
    except DriverLapTimeError:
        messagebox.showerror('ERROR', 'Lap time cannot be lower or equal 0')
        m_driver.config(background='#ea9999')
        s_driver.config(background='#ea9999')

    except ValueError:
        messagebox.showerror('ERROR', 'Incorrect input data. Check if you entered numbers')

    except:
        messagebox.showerror('ERROR', 'Unknown error')

tk.Button(fuel_calc, text='CALCULATE', width=55, command=calculate, padx=3, bg='#D5D8DC').place(x=10 ,y=356)



### RESULTS ###


result_frame = tk.LabelFrame(fuel_calc, text='Results', relief='ridge')

tk.Label(result_frame, textvariable=race_laps_var, relief='ridge', width=18, padx=2, pady=2).grid(column=0, row=0, padx=9, pady=2)
tk.Label(result_frame, textvariable=race_duration, relief='ridge', padx=2, pady=2).grid(column= 1, row=0)
tk.Label(result_frame, text='Fuel for whole race:', relief='ridge', width=18, padx=2, pady=2).grid(column=2, row=0, padx=9, pady=2)
tk.Label(result_frame, textvariable=fuel_for_race, relief='ridge', padx=2, pady=2).grid(column= 3, row=0)

tk.Label(result_frame, text='Lap time for +1 lap:', relief='ridge', width=18, padx=2, pady=2).grid(column=0, row=1, padx=9, pady=2)
tk.Label(result_frame, textvariable=one_more_lap, relief='ridge', padx=2, pady=2).grid(column= 1, row=1)
tk.Label(result_frame, text='Fuel for +1 lap:', relief='ridge', width=18, padx=2, pady=2).grid(column=2, row=1, padx=9, pady=2)
tk.Label(result_frame, textvariable=one_more_lap_fuel, relief='ridge', padx=2, pady=2).grid(column= 3, row=1)

tk.Label(result_frame, text='Lap time for -1 lap:', relief='ridge', width=18, padx=2, pady=2).grid(column=0, row=2, padx=9, pady=2)
tk.Label(result_frame, textvariable=one_less_lap, relief='ridge', padx=2, pady=2).grid(column= 1, row=2)
tk.Label(result_frame, text='Fuel for -1 lap:', relief='ridge', width=18, padx=2, pady=2).grid(column=2, row=2, padx=9, pady=2)
tk.Label(result_frame, textvariable=one_less_lap_fuel, relief='ridge', padx=2, pady=2).grid(column= 3, row=2)

result_frame.place(x=10 ,y=388, width=400, height=116)


### PIT STOP STRATEGY ###


def pit_stop_strategy():
    
    def pit_calculate():
        if number_of_pits == 0:
            no_pits_frame = tk.LabelFrame(top_pit, text='No pit stops')
            tk.Label(pit_frame, text='Fuel for the start:', relief='ridge', width=18, padx=2, pady=2).grid(column=0,row=0, padx=4, pady=4)
            tk.Label(pit_frame, textvariable=fuel_for_race, relief='ridge', padx=2, pady=2).grid(column=1,row=0, padx=4, pady=4)
            tk.Label(pit_frame, text='No pit stops!', relief='ridge', width=18, bg='#F6DDCC', padx=2, pady=2).grid(column=0,row=1, padx=4, pady=4)
            no_pits_frame.pack()
        elif number_of_pits > 0 and tyre_change_var.get() == 1 and refuel_var.get() == 0:
            start_frame = tk.LabelFrame(pit_frame, text="Start of the race")
            start_frame.pack()
            tk.Label(start_frame, text='Fuel for the start:', relief='ridge', width=18, padx=2, pady=2).grid(column=0,row=0, padx=4, pady=4)
            tk.Label(start_frame, textvariable=fuel_for_race, relief='ridge', padx=2, pady=2).grid(column=1,row=0, padx=4, pady=4) 
            if formation_lap_set == 'FULL':
                how_many_laps_p = how_many_laps - 1      
            laps_per_stint = how_many_laps_p / (number_of_pits + 1)
            if laps_per_stint % 1 == 0:
                laps_first_stint = int(laps_per_stint)
                laps_next_stint = int(laps_per_stint)
            elif laps_per_stint % 1 != 0:
                laps_first_stint = int(laps_per_stint + 1)
                laps_next_stint = int(laps_per_stint)
            stint_laps = tk.StringVar(value=laps_first_stint)          
            for n in range(number_of_pits):
                frame_name = 'Stint' + ' '  + str(n+1) 
                frame_name = tk.LabelFrame(pit_frame,text=frame_name, width=300)
                frame_name.pack(pady=5, padx=5)
                if stint_laps == laps_first_stint:
                    stint_laps = laps_next_stint
                tk.Label(frame_name, text='Refuel after stint' + ' ' + str(n+1) + ':', relief='ridge', width=18, padx=2, pady=2).grid(column=0,row=0, padx=4, pady=4)
                tk.Label(frame_name, text='0', relief='ridge', padx=2, pady=2).grid(column=1,row=0, padx=4, pady=4)
                tk.Label(frame_name, text='Laps in this stint:', relief='ridge', width=18, padx=2, pady=2).grid(column=0,row=1, padx=4, pady=4)
                tk.Label(frame_name, textvariable=stint_laps, relief='ridge', padx=2, pady=2).grid(column=1,row=1, padx=4, pady=4)
        else:
            if formation_lap_set == 'FULL':
                formation_fuel = (float(driver_fuel_value)+0.01)+brake_draggin_f+brake_draggin_s
                how_many_laps_p = how_many_laps - 1
            elif formation_lap_set == 'SHORT':
                formation_fuel = brake_draggin_s            
            laps_per_stint = how_many_laps_p / (number_of_pits + 1)
            if laps_per_stint % 1 == 0:
                laps_first_stint = int(laps_per_stint)
                laps_next_stint = int(laps_per_stint)
                start_fuel = int(float(driver_fuel_value) * laps_first_stint + formation_fuel)
                next_fuel = int(float(driver_fuel_value) * laps_next_stint)
            elif laps_per_stint % 1 != 0:
                laps_first_stint = int(laps_per_stint + 1)
                laps_next_stint = int(laps_per_stint)
                start_fuel = int(float(driver_fuel_value) * laps_first_stint + formation_fuel)
                next_fuel = int(float(driver_fuel_value) * laps_next_stint)
            fuel_for_start = tk.StringVar(value=str(start_fuel))
            fuel_for_next = tk.StringVar(value=str(next_fuel))
            start_frame = tk.LabelFrame(pit_frame, text="Start of the race")
            start_frame.pack()
            tk.Label(start_frame, text='Fuel for the start:', relief='ridge', width=18, padx=2, pady=2).grid(column=0,row=0)
            tk.Label(start_frame, textvariable=fuel_for_start, relief='ridge', padx=2, pady=2).grid(column=1,row=0)
            stint_laps = tk.StringVar(value=laps_first_stint)
            for n in range(number_of_pits):
                frame_name = 'Stint' + ' '  + str(n+1) 
                frame_name = tk.LabelFrame(pit_frame,text=frame_name, width=300)
                frame_name.pack(pady=5, padx=5)
                if stint_laps == laps_first_stint:
                    stint_laps = laps_next_stint
                tk.Label(frame_name, text='Refuel after stint' + ' ' + str(n+1) + ':', relief='ridge', width=18, padx=2, pady=2).grid(column=0,row=0, padx=4, pady=4)
                tk.Label(frame_name, textvariable=fuel_for_next, relief='ridge', padx=2, pady=2).grid(column=1,row=0, padx=4, pady=4)
                tk.Label(frame_name, text='Laps in this stint:', relief='ridge', width=18, padx=2, pady=2).grid(column=0,row=1, padx=4, pady=4)
                tk.Label(frame_name, textvariable=stint_laps, relief='ridge', padx=2, pady=2).grid(column=1,row=1, padx=4, pady=4)

    top_pit = tk.Toplevel(fuel_calc, width=300, height=300)

    scroll = tk.Scrollbar(top_pit)
    scroll.pack(side='right', fill='y')   
    pit_frame = tk.LabelFrame(top_pit, text='Pit stop strategy')
    buttons_frame = tk.Frame(top_pit, width=100)
    buttons_frame.pack()
    tk.Button(buttons_frame, text='CALCULATE AGAIN', command=pit_calculate(), padx=10, bg='#D5D8DC').pack(side='left',fill='x', padx=5)
    tk.Button(buttons_frame, text='CLOSE', command=lambda: top_pit.destroy(), padx=10, bg='#F6DDCC').pack(side='right',fill='x', padx=5)

    pit_frame.pack()

pit_button = tk.Button(fuel_calc, text='PIT STOP STRATEGY', width=55, command=pit_stop_strategy, padx=3, bg='#D5D8DC', state='disabled')
pit_button.place(x=10 ,y=512)

# SAVE AND LOAD BUTTONS

save_button = tk.Button(fuel_calc, text='SAVE', padx=3, command=save_file)
save_button.place(x=10, y=548, width= 197)
load_button = tk.Button(fuel_calc, text='LOAD', padx=3, command=load_file_button)
load_button.place(x=212, y=548, width= 197)

# MENU

def help_info():
    top_help = tk.Toplevel(fuel_calc)
    tk.Button(top_help, text='CLOSE', command=top_help.quit).pack(fill='x')
    tk.Message(top_help, text=help_text, width=300, padx=15, pady=15).pack(fill='x')

menubar = tk.Menu(fuel_calc)

main_menu = tk.Menu(menubar, tearoff=0)
main_menu.add_command(label='Exit', command=fuel_calc.destroy)
menubar.add_cascade(label='Menu', menu=main_menu)

menubar.add_command(label='Help', command=help_info)

fuel_calc.config(menu=menubar)
fuel_calc.mainloop()

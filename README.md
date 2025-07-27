# FlightTrackerFeeder

## Overview


FlightTrackerFeeder started as a C# project demonstrating how to use [SimConnectSharp](https://github.com/kodowiec/SimConnectSharp) to access real-time aircraft location data from Microsoft Flight Simulator 2024. 
Now, the project retrieves vital flight information and forwards it to external service - [ADS-B receiver API](https://github.com/oskarbarcz/adsb-receiver-api) - which in companion with [FlightTracker](https://github.com/oskarbarcz/flight-tracker-app) makes it a convenient and open source flight tracking solution.

---

## Try it out

Download newest release, run it and click 'Connect' button in MSFS section to see monitored data in real time.

------

## Features

- Connects to Microsoft Flight Simulator using SimConnectSharp
- Retrieves live aircraft location data (position, altitude, heading, etc.)
- Sends the data to external endpoints
- Modular and easily extensible for various data destinations
- Real time customizable API property mapping to monitored SimConnect variables
- Customizable MSFS poll and API submit intervals
- Saves config on app exit to keep API URL and token

---

## Prerequisites

- **Microsoft Flight Simulator 2024**
- **.NET Framework 4.7.2**
- [**SimConnectSharp**](https://github.com/kodowiec/SimConnectSharp) library (submodule)
- Windows 11 (should work on 10 but untested)

---

## How it works

1. **Connects to Flight Simulator:** Uses SimConnectSharp to establish a session and subscribe to location data.
2. **Retrieves Aircraft Data:** Periodically fetches aircraft position, altitude, heading, and other telemetry.
3. **Pushes Data:** Sends retrieved data to an external endpoint (customize as needed).

---

## Contributing

Pull requests and suggestions are welcome! Open an issue or submit a PR to improve the project.

---

## References

- [SimConnectSharp](https://github.com/kodowiec/SimConnectSharp) library
- Microsoft Flight Simulator for simulation environment
- [MSFS SDK Documentation](https://docs.flightsimulator.com/html/Introduction/Introduction.htm)

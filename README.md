# DigitalTwin.Comms.PlcSimAdvanced
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) [![build and test](https://github.com/philipp2604/DigitalTwin-Comms-PlcSimAdvanced/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/philipp2604/DigitalTwin-Comms-PlcSimAdvanced/actions/workflows/build-and-test.yml) ![GitHub Release](https://img.shields.io/github/v/release/philipp2604/DigitalTwin-Comms-PlcSimAdvanced) [![NuGet Version](https://img.shields.io/nuget/v/philipp2604.DigitalTwin.Comms.PlcSimAdvanced)](https://www.nuget.org/packages/philipp2604.DigitalTwin.Comms.PlcSimAdvanced/)




## Description 
This library aims to wrap the Siemens PLCSIM Advanced API for easy usage in simulation applications.

**Currently, only Version 5 of the API is being supported!**

**This library is still WIP and not complete yet.**

## Documentation
As the library is still at a very early state, there is no documentation yet.

I suggest looking at the included example application as well as at the library's source code - it shouldn't be hard to understand.

The basic usage consists of:

1. Creating an `PlcSimAdvInstance` instance where you can specify a name, a CPU and the communication interface.
2. Set the instance's `IsSendSyncEventInDefaultModeEnabled` property to true, to be notified after each program cycle of the virtual controller.
3. Handle the cycle's completion by subscribing to the instance's `SyncPointReached` event.
    a. Read the PLC's output variables.
    b. Compute the outputs with your simulation logic.
    c. Write to the PLC's input variables.
4. Done!

The wrapper will automatically update the tag list on the startup of the CPU.

Reading output variables can be done using e.g. `bool result = ReadBool(string tagName);`

Writing to input variables can be done using e.g. `WriteBool(string tagName, bool value);`

## Example application
The repository includes an example application - the Siemens application example 'SIMATIC S7-PLCSIM Advanced: Co - Simulation via API'.

To use this example, you will have to [download](https://support.industry.siemens.com/cs/document/109739660/simatic-s7%E2%80%91plcsim-advanced-co%E2%80%91simulation-via-api) the TIA Portal project, download the plc program to the virtual controller once it's powered on and run the HMI simulation.

All rights and code of this application example / the cosimulation class belong to Siemens!

![Image](/Screenshots/Siemens application example.jpg)

## Download
You can acquire this library either directly via the NuGet package manager or by downloading it from the [NuGet Gallery](https://www.nuget.org/packages/philipp2604.DigitalTwin.Comms.PlcSimAdvanced/).

## Questions? Problems?
**Feel free to reach out!**

## Ideas / TODO
* Implementing the whole api.
* Add 'easy' functions that perform multiple steps at once (setup -> power on -> run).

## License
This library is [MIT licensed](./LICENSE.txt).
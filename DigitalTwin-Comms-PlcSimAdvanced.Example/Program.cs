///
/// This example program is based on the Siemens application example 'SIMATIC S7-PLCSIM Advanced: Co - Simulation via API'
/// The respective copyright belongs to Siemens!
///
using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using DigitalTwin_Comms_PlcSimAdvanced.Models;

namespace DigitalTwin_Comms_PlcSimAdvanced.Example;

internal class Program
{
    readonly PlcSimAdvInstance _instance;
    readonly Cosimulation _coSim;

    public Program()
    {
        //Create a new virtual controller instance.
        _instance = new PlcSimAdvInstance("TestInstance", CpuType.CPU1516, CommunicationInterface.Softbus, 0, "192.168.0.101")
        {
            //Make sure the 'SyncPointReached' event is invoked at every end of a program cycle.
            IsSendSyncEventInDefaultModeEnabled = true
        };

        Console.WriteLine(String.Format("Instance registered: {0}", _instance.Name));

        //Create a new instance of the cosimulation.
        _coSim = new Cosimulation(2000, 1000);
    }

    /// <summary>
    /// Entry point of the console app.
    /// </summary>
    static void Main()
    {
        Program prg = new();
        prg.Run();
    }

    /// <summary>
    /// Main user program.
    /// </summary>
    void Run()
    {
        //Unregister instance when the process exits.
        AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

        //Call Instance_EndOfCycle at the end of every program cycle.
        _instance.SyncPointReached += Instance_EndOfCycle;
        while (true)
        {
            var key = Console.ReadKey();
            Console.WriteLine();

            switch (key.Key)
            {
                case ConsoleKey.S:
                {
                    try
                    {
                        Console.WriteLine(String.Format("Power On Instance: {0}", _instance.Name));
                        _instance.PowerOn();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(String.Format("PowerOn Instance failed: {0}", ex.Message));
                    }

                    break;
                }
                case ConsoleKey.R:
                {
                    try
                    {
                        Console.WriteLine(String.Format("Run Instance: {0}", _instance.Name));
                        _instance.Run();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(String.Format("Run Instance failed: {0}", ex.Message));
                    }

                    break;
                }
                case ConsoleKey.Q:
                {
                    try
                    {
                        Console.WriteLine(String.Format("Stop Instance: {0}", _instance.Name));
                        _instance.Stop();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(String.Format("Stop Instance failed: {0}", ex.Message));
                    }

                    try
                    {
                        Console.WriteLine(String.Format("PowerOff Instance: {0}", _instance.Name));
                        _instance.PowerOff();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(String.Format("PowerOff Instance failed: {0}", ex.Message));
                    }
                    return;
                }
                case ConsoleKey.Y:
                {
                    try
                    {
                        Console.WriteLine("Start CoSim");
                        _coSim.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(String.Format("Start CoSim failed: {0}", ex.Message));
                    }
                    break;
                }
                case ConsoleKey.X:
                {
                    try
                    {
                        Console.WriteLine("Stop CoSim");
                        _coSim.Stop();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(String.Format("Stop CoSim failed: {0}", ex.Message));
                    }
                    break;
                }
                case ConsoleKey.N:
                {
                    try
                    {
                        Console.WriteLine("Simulate error in CoSim");
                        _coSim.Error();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(String.Format("Simulate error in CoSim failed: {0}", ex.Message));
                    }
                    break;
                }
                case ConsoleKey.M:
                {
                    try
                    {
                        Console.WriteLine("Reset simulated error in CoSim");
                        _coSim.PackageOK();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(String.Format("Reset simulated error in CoSim failed: {0}", ex.Message));
                    }
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Called at the end of every program cycle (since we have set '_instance.IsSendSyncEventInDefaultModeEnabled = true').
    /// Reads tags, processes them in the cosimulation, writes tags.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Instance_EndOfCycle(object? sender, Events.SyncPointReachedEventArgs e)
    {
        if(_instance.IsInitialized)
        {
            //Read the output values of the virtual controller into the Co-Simulation.
            _coSim.setOnBeltActive = _instance.ReadBool("setOnBeltActive");
            _coSim.moveBeltActive = _instance.ReadBool("moveBeltActive");
            _coSim.setOffBeltActive = _instance.ReadBool("setOffBeltActive");
            _coSim.releaseActive = _instance.ReadBool("releaseActive");
            _coSim.acknowledgeActive = _instance.ReadBool("acknowledgeActive");
            _coSim.restartActive = _instance.ReadBool("restartActive");

            // Call the Co-Simulation programm
            _coSim.CoSimProgramm();

            // Write the Co-Simulation values to the inputs of the virtual controller  
            _instance.WriteBool("sensorStartPos", _coSim.sensorStartPos);
            _instance.WriteBool("sensorBeltStart", _coSim.sensorBeltStart);
            _instance.WriteBool("sensorBeltDest", _coSim.sensorBeltDest);
            _instance.WriteBool("sensorEndPos", _coSim.sensorEndPos);
        }
    }

    void CurrentDomain_ProcessExit(object? sender, EventArgs e)
    {
        _instance.UnregisterInstance();
    }
}

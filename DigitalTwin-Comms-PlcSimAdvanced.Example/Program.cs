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
        _instance = new PlcSimAdvInstance("TestInstance", CpuType.CPU1500_Unspecified, CommunicationInterface.Softbus, 0, "192.168.0.101")
        {
            //Make sure the 'SyncPointReached' event is invoked at every end of a program cycle.
            IsSendSyncEventInDefaultModeEnabled = true
        };

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
            switch (key.Key)
            {
                case ConsoleKey.P:
                {
                    _instance.PowerOn();
                    Console.WriteLine("PLC Power On");
                    break;
                }
                case ConsoleKey.Q:
                {
                    _instance.PowerOff();
                    Console.WriteLine("PLC Power Off");
                    break;
                }
                case ConsoleKey.R:
                {
                    _instance.Run();
                    Console.WriteLine("PLC Run");
                    break;
                }
                case ConsoleKey.S:
                {
                    _instance.Stop();
                    Console.WriteLine("PLC Stop");
                    break;
                }
                case ConsoleKey.Y:
                {
                    _coSim.Start();
                    Console.WriteLine("Sim Start");
                    break;
                }
                case ConsoleKey.X:
                {
                    _coSim.Stop();
                    Console.WriteLine("Sim Stop");
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

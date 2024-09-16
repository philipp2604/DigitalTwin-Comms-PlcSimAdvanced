using CoSimulationPlcSimAdv.Models;
using DigitalTwin_Comms_PlcSimAdvanced.Models;

namespace DigitalTwin_Comms_PlcSimAdvanced.Example;

internal class Program
{
    readonly PlcSimAdvInstance _instance;
    readonly Cosimulation _coSim;

    public Program()
    {
        _instance = new PlcSimAdvInstance("TestInstance", PlcSimAdvInstance.CommunicationInterfaceType.Softbus, "192.168.0.101");
        _coSim = new Cosimulation(2000, 1000);
    }

    static void Main(string[] args)
    {
        Program x = new();
        x.Run();
    }

    void Run()
    {
        AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

        Console.WriteLine("Hello, World!");
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

    private void Instance_EndOfCycle(object? sender, Events.SyncPointReachedEventArgs e)
    {
        if(_instance.IsInitialized)
        {
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

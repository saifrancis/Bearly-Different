using UnityEngine;
using System.IO.Ports;

public class ArduinoReader : MonoBehaviour
{
    [Header("Serial Settings")]
    public string portName = "COM3";
    public int baudRate = 9600;

    [Header("References")]
    public FrameManager frameManager; //E: reference to FrameManager to call story functions

    private SerialPort serialPort;
    private string lastLine = "";     //E: to prevent repeated input from serial

    void Start()
    {
        //E: set up serial port with selected port and baud rate
        serialPort = new SerialPort(portName, baudRate);
        serialPort.ReadTimeout = 50;

        try
        {
            serialPort.Open(); //E: try open the serial port
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Could not open {portName}: {e.Message}"); //E: if it fails show error
        }
    }

    void Update()
    {
        //E: Check if D key pressed (just there so game can be tested without the arduino)
        if (Input.GetKeyDown(KeyCode.D))
        {
            frameManager.ShowNextFrame(); //E: simulate next button
        }

        //E: Check if A key pressed (just there so game can be tested without the arduino)
        if (Input.GetKeyDown(KeyCode.A))
        {
            frameManager.ShowPreviousPage(); //E: simulate going back a page
        }

        //E: Check serial input from Arduino
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string line = serialPort.ReadLine().Trim(); //E: read serial line

                if (line == lastLine) return; 
                lastLine = line;

                //E: Button 1 pressed on arduino (next)
                if (line == "1")
                {
                    frameManager.ShowNextFrame();
                }

                //E: Button 2 pressed on arduino (back)
                else if (line == "2")
                {
                    frameManager.ShowPreviousPage();
                }
            }
            catch (System.TimeoutException)
            {
                //E: no data received so ignore
            }
        }
    }

    void OnApplicationQuit()
    {
        //E: close serial connection when Unity quits
        if (serialPort?.IsOpen == true)
            serialPort.Close();
    }
}

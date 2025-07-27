using UnityEngine;
using System.IO.Ports;

public class ArduinoReader : MonoBehaviour
{
    [Header("Serial Settings")]
    public string portName = "COM3";
    public int baudRate = 9600;

    [Header("References")]
    public FrameManager frameManager;

    private SerialPort serialPort;
    private bool lastState = false;

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.ReadTimeout = 50;

        try
        {
            serialPort.Open();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Could not open {portName}: {e.Message}");
        }
    }

    void Update()
    {
        //E: Check if D key pressed (just there so game can be tested without the arduino)
        if (Input.GetKeyDown(KeyCode.D))
        {
            frameManager.ShowNextFrame();
        }

        //E: Check if arduino button pressed
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string line = serialPort.ReadLine().Trim();
                bool isPressed = (line == "1");

                if (isPressed && !lastState)
                {
                    frameManager.ShowNextFrame();
                }

                lastState = isPressed;
            }
            catch (System.TimeoutException) { }
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort?.IsOpen == true)
            serialPort.Close();
    }
}

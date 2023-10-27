using System.IO.Ports;

SerialPort serialPort = new SerialPort("COM3", 9600);
serialPort.Open();
await Task.Delay(1000);
serialPort.DataReceived += (sender, e) =>
{
    if (sender is SerialPort sender1)
    {
        Console.WriteLine($"sender: {sender1.ReadExisting()}");
        Console.WriteLine($"e: {e.EventType}");
    }

};
for(int i=0;i<5;i++){
    serialPort.Write($"Hello World {i}");
    await Task.Delay(3000);
}
await Task.Delay(60000);
namespace RaspiAir.Measurement;

public interface ISensor
{
    event Action<SensorData> OnDataReceived;
    
    void Start();

    void Stop();
}
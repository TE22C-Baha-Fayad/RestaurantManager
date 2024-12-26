
using System.Text.Json;
/// <summary>
/// A simple class to save and load data using json
/// </summary>
/// <typeparam name="T"></typeparam>
class DataSerializer<T>
{
    //decleare data of generic type
    private T _data;
    //create a constant for the name of the file
    private const string FileName = "\\data.json";
    //create a constructor for the file to set the data
    public DataSerializer(T data)
    {
        _data = data;
    }

    /// <summary>
    /// saves the data into data.json file
    /// </summary> <summary>
    /// 
    /// </summary>
    public void SaveData()
    {
        string serializedData = JsonSerializer.Serialize<T>(_data);
        File.WriteAllText(Directory.GetCurrentDirectory() + FileName, serializedData);

    }
    /// <summary>
    /// gets the data from the data.json file
    /// </summary>
    /// <returns>data</returns>
    public static T GetData()
    {
        string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + FileName);
        T data = JsonSerializer.Deserialize<T>(jsonData);
        return data;
    }



}
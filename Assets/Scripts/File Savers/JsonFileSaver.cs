using System;
using System.IO;
using UnityEngine;

public class JsonFileSaver : IFileSaver
{
    private StreamWriter _fileStream;
    
    public IDisposable OpenFileStream(string path)
    {
        _fileStream = File.CreateText(path);
        return _fileStream;
    }

    public void WriteText(object itemToSave)
    {
        if (_fileStream == null )
            return;
        
        if (itemToSave == null)
            return;
        
        // Convert the object to a JSON string
        var text = JsonUtility.ToJson(itemToSave, true);
        
        // This method assumes that the file stream is open
        _fileStream.WriteLine(text);
    }
}

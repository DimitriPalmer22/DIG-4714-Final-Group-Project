using System;
using System.IO;

public class TxtFileSaver : IFileSaver
{
    private StreamWriter _fileStream;

    public IDisposable OpenFileStream(string path)
    {
        var file = File.Create(path);
        file.Close();
        
        _fileStream = new StreamWriter(path);
        return _fileStream;
    }

    public void WriteText(object itemToSave)
    {
        if (_fileStream == null)
            return;

        if (itemToSave == null)
            return;

        // This method assumes that the file stream is open
        var text = itemToSave.ToString();
        _fileStream.WriteLine(text);
    }
}

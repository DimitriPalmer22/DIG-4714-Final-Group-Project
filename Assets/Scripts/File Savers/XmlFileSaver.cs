using System;
using System.IO;
using System.Xml.Serialization;

public class XmlFileSaver : IFileSaver
{
    private FileStream _fileStream;

    public IDisposable OpenFileStream(string path)
    {
        _fileStream = File.Create(path);
        return _fileStream;
    }

    public void WriteText(object itemToSave)
    {
        if (_fileStream == null)
            return;

        if (itemToSave == null)
            return;

        var serializer = new XmlSerializer(itemToSave.GetType());
        
        // This method assumes that the file stream is open
        serializer.Serialize(_fileStream, itemToSave);
    }
}

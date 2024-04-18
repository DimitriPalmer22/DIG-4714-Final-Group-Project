using System;
using System.IO;

public interface IFileSaver
{
    public IDisposable OpenFileStream(string path);

    public void WriteText(object itemToSave);
}

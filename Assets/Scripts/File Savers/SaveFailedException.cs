using System;

public class SaveFailedException : Exception
{
    public string Location { get; private set; }
    
    public SaveFailedException(string location)
        : base($"Failed to save to {location}")
    {
        Location = location;
    }
    
}
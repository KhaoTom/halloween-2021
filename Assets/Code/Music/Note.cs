public struct Note
{
    public int noteNumber;
    public float volume;
    public int strumCount;
    public int strumInterval;
    public float strumDelay;
    public float strumVolumeChange;

    public Note(int noteNumber, float volume, int strumCount = 0, int strumInterval = 0, float strumDelay = 0, float strumVolumeChange = 0)
    {
        this.noteNumber = noteNumber;
        this.volume = volume;
        this.strumCount = strumCount;
        this.strumInterval = strumInterval;
        this.strumDelay = strumDelay;
        this.strumVolumeChange = strumVolumeChange;
    }

    private static Note _none = new Note(-1, -1f);
    public static Note None { get => _none; }

    public static bool operator ==(Note c1, Note c2)
    {
        return c1.Equals(c2);
    }

    public static bool operator !=(Note c1, Note c2)
    {
        return !c1.Equals(c2);
    }

    public override bool Equals(object obj)
    {
        return obj is Note note &&
               noteNumber == note.noteNumber &&
               volume == note.volume;
    }

    public override int GetHashCode()
    {
        int hashCode = -627556953;
        hashCode = hashCode * -1521134295 + noteNumber.GetHashCode();
        hashCode = hashCode * -1521134295 + volume.GetHashCode();
        return hashCode;
    }

    public override string ToString()
    {
        return $"Note({noteNumber}, {volume})";
    }
}

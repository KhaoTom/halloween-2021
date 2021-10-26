public interface IMusicSequence
{
    public Note GetNote(int index, Scales scale, int rootNoteNumber);
    public int Length { get; }
}

[System.Serializable]
public class BeatStep
{
    public int interval = 1;
    public float volume = 1.0f;
}

public abstract class SequenceScriptableObject : UnityEngine.ScriptableObject
{
    public abstract IMusicSequence GetSequence();
}


[System.Serializable]
public class SequencePlayer
{
    public BeatStep[] beatSteps;
    public IMusicSequence sequence;
    public int nextBeat = -1;
    public int currentBeatStepIndex = 0;
    public int currentSequenceIndex = 0;

    public Note GetNote(int beat, int rootNoteNumber, Scales scale)
    {
        if (beat >= nextBeat)
        {
            Note result = sequence.GetNote(currentSequenceIndex, scale, rootNoteNumber);
            result.volume = beatSteps[currentBeatStepIndex].volume;

            nextBeat += beatSteps[currentBeatStepIndex].interval;
            currentBeatStepIndex = currentBeatStepIndex.Plus1Wrap0(beatSteps.Length);
            currentSequenceIndex = currentSequenceIndex.Plus1Wrap0(sequence.Length);
            return result;
        }
        return Note.None;
    }
}


[System.Serializable]
public class SimpleSequence : IMusicSequence
{
    public int[] scaleDegrees;

    public int Length => scaleDegrees.Length;

    public Note GetNote(int index, Scales scale, int rootNoteNumber)
    {
        var scaleDegree = scaleDegrees[index];
        var noteNumber = MusicUtils.GetScaledNote(rootNoteNumber, scaleDegree, scale);
        var result = new Note(noteNumber, 1.0f);
        return result;
    }
}

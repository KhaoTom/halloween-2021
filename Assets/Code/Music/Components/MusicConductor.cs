using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The conductor of the musical world!
/// Provides the tempo.
/// Provides the key and scale.
/// </summary>
public class MusicConductor : MonoBehaviour
{
    public int defaultBpm = 90;
    public Scales defaultScale = Scales.Ionian;
    public int defaultRootNote = 0;
    public float delaySecondsBeforeFirstBeat = 1f;

    private int _bpm;
    public int bpm { get => _bpm; private set { timePerBeat = 60.0f / value; _bpm = value; } }
    public float timePerBeat { get; private set; }
    public float currentBeatStartTime { get; private set; }
    public int currentBeat { get; private set; }
    public Scales currentScale { get; private set; }
    public int currentRootNote { get; private set; }

    private TempoInfluencer[] tempoInfluencers;

    private void Awake()
    {
        bpm = defaultBpm;
        currentBeat = -1;
        currentBeatStartTime = Time.time + delaySecondsBeforeFirstBeat;
        tempoInfluencers = FindObjectsOfType<TempoInfluencer>();

        currentRootNote = defaultRootNote;
        currentScale = defaultScale;
    }

    private void Update()
    {
        var t = Time.time;
        if (t >= currentBeatStartTime + timePerBeat)
        {
            currentBeat += 1;
            currentBeatStartTime += timePerBeat;
        }

        List<int> bpmChanges = new List<int>();
        foreach (var influencer in tempoInfluencers)
        {
            if (influencer.influence > 0f)
            {
                bpmChanges.Add(Mathf.FloorToInt(influencer.bpmOffset * influencer.influence));
            }
        }
        bpm = defaultBpm + bpmChanges.Average();
    }

    public void ChangeScale(Scales newScale)
    {
        currentScale = newScale;
    }

    public void ChangeRootNote(int newRootNote)
    {
        currentRootNote = newRootNote;
    }

    public void ChangeDefaultBPM(int newDefaultBpm)
    {
        defaultBpm = newDefaultBpm;
    }

    public void ReduceBPM(int amount)
    {
        defaultBpm = Mathf.Max(defaultBpm - amount, 10);
    }
}

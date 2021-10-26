using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

/// <summary>
/// A musical being animates and plays music notes.
/// It reacts to stimuli in the world.
/// </summary>
public class MusicalBeing : MonoBehaviour
{
    [System.Serializable]
    public class OnPlayNoteEvent : UnityEvent<float> { }
    [System.Serializable]
    public class OnAllNotesEndEvent : UnityEvent { }

    public int areaId = 0;
    public AudioMixerGroup mixerGroup;
    public float basevolume = 1.0f;
    public int rootOffset = 0;
    public int startBeatDelay = 0;

    public SequenceScriptableObject startSequence;
    public BeatSequenceScriptableObject startBeatSequence;

    public AudioClip[] clips;

    public OnPlayNoteEvent onPlayNote;
    public OnAllNotesEndEvent onAllNotesEnd;

    public UnityEvent onEnable;
    public UnityEvent onDisable;

    private MusicConductor musicConductor;

    private int lastBeatHandled = -1;
    private int lastUpdateNumSrcPlaying = 0;

    private List<AudioSource> sources = new List<AudioSource>();

    private IMusicSequence currentSequence;
    private SequencePlayer sequencePlayer;

    private static readonly float intervalConstant = Mathf.Pow(2f, 1.0f / 12f);
    private static readonly int middleNoteNumber = 69;

    void Awake()
    {
        // create the ur AudioSource.
        var go = new GameObject($"{gameObject.name} Audio 0");
        go.transform.SetParent(transform);
        go.transform.localPosition = Vector3.zero;
        var src = go.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.outputAudioMixerGroup = mixerGroup;
        src.spatialBlend = 1.0f;
        src.dopplerLevel = 0.0f;
        sources.Add(src);

        sequencePlayer = new SequencePlayer();

        musicConductor = FindObjectOfType<MusicConductor>();
    }

    private void OnEnable()
    {
        var currentBeat = musicConductor.currentBeat;

        currentSequence = startSequence.GetSequence();

        sequencePlayer.sequence = currentSequence;
        sequencePlayer.beatSteps = startBeatSequence.beatSteps;
        sequencePlayer.nextBeat = currentBeat + startBeatDelay;

        onEnable.Invoke();
    }

    private void OnDisable()
    {
        onDisable.Invoke();
    }

    void Update()
    {
        var currentBeat = musicConductor.currentBeat;
        if (lastBeatHandled < currentBeat)
        {
            HandleBeat(currentBeat);
        }

        int numSrcPlaying = 0;
        foreach (var src in sources)
        {
            if (src.isPlaying) numSrcPlaying++;
        }
        if (lastUpdateNumSrcPlaying != numSrcPlaying && numSrcPlaying == 0)
        {
            onAllNotesEnd.Invoke();
        }
    }

    void HandleBeat(int currentBeat)
    {
        var baseNote = musicConductor.currentRootNote + rootOffset;
        var note = sequencePlayer.GetNote(currentBeat, baseNote, musicConductor.currentScale);
        PlayNote(note);
        lastBeatHandled = currentBeat;
    }

    void PlayNote(Note note)
    {
        if (note == Note.None) return;
        
        var diff = note.noteNumber - middleNoteNumber;
        var pitch = Mathf.Pow(intervalConstant, diff);
        var volume = basevolume;

        PlayOnNextAvailableSource(clips.RandomItem(), pitch, volume);

        onPlayNote.Invoke(pitch);
    }

    IEnumerator StrumNote(Note note, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (note == Note.None) yield break;

        var diff = note.noteNumber - middleNoteNumber;
        var pitch = Mathf.Pow(intervalConstant, diff);
        var volume = note.volume;

        PlayOnNextAvailableSource(clips.RandomItem(), pitch, volume);
    }

    void PlayOnNextAvailableSource(AudioClip clip, float pitch, float volume)
    {
        var src = GetFirstAvailableSource();
        src.clip = clip;
        src.pitch = pitch;
        src.volume = volume;
        src.Play();
    }

    AudioSource GetFirstAvailableSource()
    {
        foreach (var src in sources)
        {
            if (!src.isPlaying)
            {
                return src;
            }
        }
        return CloneSource();
    }

    AudioSource CloneSource()
    {
        var go = Instantiate<GameObject>(sources[0].gameObject, transform);
        go.name = $"{gameObject.name} Audio {sources.Count}";
        var src = go.GetComponent<AudioSource>();
        sources.Add(src);
        return src;
    }
}

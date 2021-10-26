using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeatSequence", menuName = "Sequences/BeatSequence", order = 2)]
public class BeatSequenceScriptableObject : ScriptableObject
{
    public BeatStep[] beatSteps;
}

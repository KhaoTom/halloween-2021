using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleSequence", menuName = "Sequences/SimpleSequence", order = 1)]
public class SimpleSequenceScriptableObject : SequenceScriptableObject
{
    public SimpleSequence sequence;

    public override IMusicSequence GetSequence()
    {
        return sequence;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MusicUtils
{
    public static int GetScaledNote(int rootNote, int scaleDegree, Scales scale)
    {
        int octaveOffset = 0;
        int degree = scaleDegree;
        if (scaleDegree > 7 || scaleDegree < 0)
        {
            octaveOffset = Mathf.FloorToInt(scaleDegree / 8f) * 12;
            degree = Mathf.Abs(scaleDegree % 8);
        }
        try
        {
            int interval = Intervals.FromEnum(scale)[degree];
            return rootNote + octaveOffset + interval;
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.Log($"degree={degree}, scaleDegree={scaleDegree}, mod={scaleDegree % 8}");
            throw;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Scales
{
    Ionian,
    Dorian,
    Phrygian,
    Lydian,
    Mixolydian,
    Aeolian,
    Locrian
}

public static class Intervals
{
    // we refer to the scale degrees counting from 1, so index 0 should never be used.
    public static readonly int[] Ionian =     { 0, 2, 4, 5, 7, 9, 11, 12 };
    public static readonly int[] Dorian =     { 0, 2, 3, 5, 7, 9, 10, 12 };
    public static readonly int[] Phrygian =   { 0, 1, 3, 5, 7, 8, 10, 12 };
    public static readonly int[] Lydian =     { 0, 2, 4, 6, 7, 9, 11, 12 };
    public static readonly int[] Mixolydian = { 0, 2, 4, 5, 7, 9, 10, 12 };
    public static readonly int[] Aeolian =    { 0, 2, 3, 5, 7, 8, 10, 12 };
    public static readonly int[] Locrian =    { 0, 1, 3, 5, 6, 8, 10, 12 };

    private static readonly int[][] all = { Ionian, Dorian, Phrygian, Lydian, Mixolydian, Aeolian, Locrian };
    public static int[] FromEnum(Scales scale)
    {
        return all[(int)scale];
    }
}

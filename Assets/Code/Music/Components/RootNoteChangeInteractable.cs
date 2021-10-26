using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNoteChangeInteractable : MonoBehaviour
{
    public int targetNoteNumber;
    public bool changeGameObjectName = false;

    private MusicConductor conductor;

    private void Start()
    {
        if (changeGameObjectName)
            gameObject.name = $"Change root to {targetNoteNumber}";

        conductor = FindObjectOfType<MusicConductor>();
    }

    public void DoInteraction()
    {
        conductor.ChangeRootNote(targetNoteNumber);
    }
}

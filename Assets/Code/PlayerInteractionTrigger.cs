using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Use physics layers to limit collisions and manage what goes into the inTrigger list.
/// </summary>
public class PlayerInteractionTrigger : MonoBehaviour
{
    public string objectNameFilter = "";
    public List<GameObject> inTrigger = new List<GameObject>();
    public HashSet<string> excludedNames = new HashSet<string>();

    public UnityEvent<List<GameObject>> onTriggerEnter;
    public UnityEvent<List<GameObject>> onTriggerExit;

    public void SetObjectNameFilter(string gameObjectName)
    {
        objectNameFilter = gameObjectName;
    }

    public void RemoveFromExcludedNames(string gameObjectName)
    {
        excludedNames.Remove(gameObjectName);
    }

    public void AddToExcludedNames(string gameObjectName)
    {
        excludedNames.Add(gameObjectName);
    }

    public void RemoveFromTrigger(GameObject gameObject)
    {
        if (inTrigger.Remove(gameObject))
        {
            onTriggerExit.Invoke(inTrigger);
        }
    }

    public void SendDoInteractionMessage(GameObject sender)
    {
        if (inTrigger.Count > 0)
        {
            inTrigger[0].SendMessageUpwards("DoInteraction");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (objectNameFilter == other.gameObject.name || !excludedNames.Contains(other.gameObject.name))
        {
            inTrigger.Add(other.gameObject);
            onTriggerEnter.Invoke(inTrigger);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveFromTrigger(other.gameObject);
    }

}

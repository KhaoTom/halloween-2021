using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Text interactionText;

    public void SetInteractionText(string text)
    {
        interactionText.text = text;
    }

    public void SetInteractionTextFromFirst(List<GameObject> gameObjects)
    {
        if (gameObjects.Count > 0)
        {
            SetInteractionText(gameObjects[0].name);
        }
        else
        {
            SetInteractionText("");
        }
    }
}

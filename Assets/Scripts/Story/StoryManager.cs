using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public Text GetText;
    public void StartStroy(Story story)
    {
        GetText.text = story.TextForStory;
    }
}

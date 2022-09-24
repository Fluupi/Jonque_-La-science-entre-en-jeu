using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Dialogue", menuName = "Dialogue")]
public class DialogueBranch : ScriptableObject
{
    public int numOfSentences;
    [TextArea(3, 10)]
    public string[] sentences;
    public Sprite[] char1;
    public Sprite[] char2;
    public string dialogueName;
    [Range(0,2)]
    public int lastDialogue;
}

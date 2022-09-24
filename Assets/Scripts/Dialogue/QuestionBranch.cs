using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new question", menuName = "Question")]
public class QuestionBranch : ScriptableObject
{
    public string[] answer;
    public int[] karma;
    public int[] dialogueIndex;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
public class DialogueTrigger : MonoBehaviour
{
    [Header("Stats")]
    [Space(5)]
    public int currentLikability;
    [Space(15)]
    [Header("Question")]
    [Space(5)]
    public QuestionBranch[] question;
    public int questionIndex;
  
    [Space(15)]
    [Header("Dialogue")]
    [Space(5)]
    public DialogueBranch[] dialogue;
    int textIndex;
    DialogueBranch currentDialogue;

    [Space(15)]
    [Header("Components Question")]
    [Space(5)]
    public GameObject Answer;
    public TextMeshProUGUI answer1, answer2, answer3;
    public Button a1, a2, a3;

    [Space(15)]
    [Header("Components Question")]
    [Space(5)]
    public GameObject Dialogue;
    public Button skipButton;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueName;
    public Image char1;




    




    public void StartDialogue(int dialogueIndex)
    {
        currentDialogue = dialogue[dialogueIndex];
        textIndex = 0;

        if (dialogueIndex == 0)
        {
            skipButton.onClick.AddListener(() => SkipDialogue());
            Dialogue.SetActive(true);
            dialogueName.text = dialogue[dialogueIndex].dialogueName;
        }
        else
            questionIndex++;

        dialogueText.text = dialogue[dialogueIndex].sentences[textIndex];
        char1.sprite = dialogue[dialogueIndex].char1[textIndex];
    }

    public void SkipDialogue()
    {
        if(textIndex < currentDialogue.numOfSentences - 1)
        {
            print(0);

            textIndex += 1;
            dialogueText.text = currentDialogue.sentences[textIndex];
            char1.sprite = currentDialogue.char1[textIndex];
        }
        else if (currentDialogue.lastDialogue == 0)
        {
            print(1);

            StartQuestion(true);
        }
        else if(currentDialogue.lastDialogue == 1)
        {
            print(2);

            StartQuestion(false);
        }
        else if(currentDialogue.lastDialogue == 2)
        {
            print(3);
            skipButton.onClick.RemoveListener(() => SkipDialogue());
            a1.onClick.RemoveListener(() => A1());
            a2.onClick.RemoveListener(() => A2());
            a3.onClick.RemoveListener(() => A3());
            Dialogue.SetActive(false);
            dialogueName.text = "";
        }
    }

    public void StartQuestion(bool firstQ)
    {
        Answer.SetActive(true);
        if (firstQ)
        {
            questionIndex = 0;
            a1.onClick.AddListener(() => A1());
            a2.onClick.AddListener(() => A2());
            a3.onClick.AddListener(() => A3());
        }

        answer1.text = question[questionIndex].answer[0];
        answer2.text = question[questionIndex].answer[1];
        answer3.text = question[questionIndex].answer[2];

    }

    public void A1()
    {
        currentLikability += question[questionIndex].karma[0];
        Answer.SetActive(false);
        StartDialogue(question[questionIndex].dialogueIndex[0]);
    }

    public void A2()
    {
        currentLikability += question[questionIndex].karma[1];
        Answer.SetActive(false);
        StartDialogue(question[questionIndex].dialogueIndex[1]);
    }

    public void A3()
    {
        currentLikability += question[questionIndex].karma[2];
        Answer.SetActive(false);
        StartDialogue(question[questionIndex].dialogueIndex[2]);
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartDialogue(0);
    }
}

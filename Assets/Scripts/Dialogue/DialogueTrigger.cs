using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using FMODUnity;
public class DialogueTrigger : MonoBehaviour
{
    public UnityEvent<float, float, bool> endDialogueEventFade;
    public UnityEvent<Vector2, Vector2, bool> endDialogueEventPop;
    public UnityEvent<Vector2, Vector2, bool> endQuestionEventPop;
    public UnityEvent disactivateZoneEvent;
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
    public Image char1, char2;

    public Image RelatedQuest;
    public float dialogueSpeed;
    public Bateau bateau;

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }

    //Start a dialogue or resume dialogue after question
    public void StartDialogue(int dialogueIndex)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_Menu/UI_Menu_Skip");
        currentDialogue = dialogue[dialogueIndex];
        char2.color = Color.grey;
        char1.color = Color.white;
        textIndex = 0;
        skipButton.interactable = true;


        //Invoque seulement lors du premier dialogue
        if (dialogueIndex == 0)
        {
            questionIndex = 0;

            bateau.enDialogue = true;
            //skipButton.onClick.

            skipButton.onClick.AddListener(() => SkipDialogue());
            Dialogue.SetActive(true);
            dialogueName.text = dialogue[dialogueIndex].dialogueName;
        }
/*        else
            questionIndex++;*/
        StartCoroutine(TypeSentence(dialogue[dialogueIndex].sentences[textIndex]));
        //dialogueText.text = dialogue[dialogueIndex].sentences[textIndex];
        char1.sprite = dialogue[dialogueIndex].char1[textIndex];
        char2.sprite = dialogue[dialogueIndex].char2[textIndex];
    }

    //Appuyer sur le bouton continuer durant un dialogue
    public void SkipDialogue()
    {
        //Next texte


        if (textIndex < currentDialogue.numOfSentences - 1)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI_Menu/UI_Menu_Skip");
            StopAllCoroutines();
            dialogueText.text = "";
            textIndex += 1;
            StartCoroutine(TypeSentence(currentDialogue.sentences[textIndex]));

            char1.sprite = currentDialogue.char1[textIndex];
            char2.sprite = currentDialogue.char2[textIndex];
        }
        //First Question
        else if (currentDialogue.lastDialogue == 0) 
        {
            StartQuestion(true);
        }
        // Question that is not first
        else if(currentDialogue.lastDialogue == 1)
        {
            StartQuestion(false);
        }
        //End Dialogue
        else if(currentDialogue.lastDialogue == 2)
        {
            disactivateZoneEvent?.Invoke();
            endDialogueEventFade?.Invoke(.6f, 0f, false);
            endDialogueEventPop?.Invoke(Vector2.zero, new Vector2(0, -950f), true) ;
            skipButton.onClick.RemoveAllListeners();
            //skipButton.onClick.RemoveListener(() => SkipDialogue());
            bateau.enDialogue = false;

            skipButton.onClick.RemoveListener(() => SkipDialogue());
            bateau.numberOfQuestsDone++;
            RelatedQuest.color = Color.grey;
            a1.onClick.RemoveAllListeners();
            a2.onClick.RemoveAllListeners();
            a3.onClick.RemoveAllListeners();
            //a1.onClick.RemoveListener(() => A1());
            //a2.onClick.RemoveListener(() => A2());
            //a3.onClick.RemoveListener(() => A3());
            //Dialogue.SetActive(false);
            dialogueName.text = "";
        }
    }

    public void StartQuestion(bool firstQ)
    {
        Answer.SetActive(true);
        skipButton.interactable = false;
        char2.color = Color.white;
        char1.color = Color.grey;
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
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_Menu/UI_Menu_Validation");
        StopAllCoroutines();

        currentLikability += question[questionIndex].karma[0];
        endQuestionEventPop?.Invoke(new Vector2(0, 116), new Vector2(0, -575), true);
        //Answer.SetActive(false);
        StartDialogue(question[questionIndex].dialogueIndex[0]);
        questionIndex = question[questionIndex].nextQuestionIndex[0];
    }

    public void A2()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_Menu/UI_Menu_Validation");
        StopAllCoroutines();

        currentLikability += question[questionIndex].karma[1];
        endQuestionEventPop?.Invoke(new Vector2(0, 116), new Vector2(0, -575), true);
        //Answer.SetActive(false);
        StartDialogue(question[questionIndex].dialogueIndex[1]);
        questionIndex = question[questionIndex].nextQuestionIndex[1];

    }

    public void A3()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_Menu/UI_Menu_Validation");
        StopAllCoroutines();

        currentLikability += question[questionIndex].karma[2];
        endQuestionEventPop?.Invoke(new Vector2(0, 116), new Vector2(0, -575), true);
        //Answer.SetActive(false);
        StartDialogue(question[questionIndex].dialogueIndex[2]);
        questionIndex = question[questionIndex].nextQuestionIndex[2];

    }

}

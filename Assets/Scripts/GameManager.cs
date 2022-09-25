using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Game")]
    [SerializeField] private Bateau player;
    [SerializeField] private LevelEndDisplay levelEndDisplay;
    [SerializeField] private DialogueTrigger[] dialogueTriggers;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !player.enDialogue)
        {
            if(Random.Range(0,1) > .5f)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Ship_Movement");
            }

            player.Kick();
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            foreach (DialogueTrigger d in dialogueTriggers)
                d.currentLikability = 2;

            player.numberOfQuestsDone = 3;
        }
    }

    public void TriggerVoyageDeRtour()
    {
        bool quest1 = dialogueTriggers[0].currentLikability >= dialogueTriggers[0].neededLikability;
        bool quest2 = dialogueTriggers[1].currentLikability >= dialogueTriggers[1].neededLikability;
        bool quest3 = dialogueTriggers[2].currentLikability >= dialogueTriggers[2].neededLikability;

        bool level = quest1 && quest2 || quest1 && quest3 || quest2 && quest3;

        levelEndDisplay.gameObject.SetActive(true);
        levelEndDisplay.DisplayResults(level, quest1, quest2, quest3);
    }
}
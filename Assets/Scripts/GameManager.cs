using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    [Header("FMOD")]
    [HideInInspector] public float CommentaryTimer;
    public float timeBeforeNewCommentary;
    [Range(0,1)] public float probaComCollision, probaComRotation, probaComNothingToSay;

    [Header("Game")]
    [SerializeField] private Bateau player;
    [SerializeField] private LevelEndDisplay levelEndDisplay;
    [SerializeField] private DialogueTrigger[] dialogueTriggers;
    public float angleToTriggerRotationDialogue = 60;

    float timer = 0;
    public float commentaryFrequence;

    private void Update()
    {
        timer += Time.deltaTime;
        CommentaryTimer += Time.deltaTime;



        if (Input.GetKeyDown(KeyCode.Space) && !player.enDialogue)
        {
            float rd = Random.Range(0f, 1f);

            if (rd > .5f)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Ship_Movement");
            }
            timer = 0;

            Vector3 targetDirection = player.target - player.transform.position;
            float angle = Vector2.Angle((Vector2)targetDirection, (Vector2)player.transform.up);
            // print(angle);

            float rd2 = Random.Range(0f, 1f);
            if (rd2 > probaComRotation && angle > angleToTriggerRotationDialogue && CommentaryTimer > timeBeforeNewCommentary)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Commentary/Nelson/Rotation");
                CommentaryTimer = 0;
            }

            player.Kick();
        }


        if (timer > commentaryFrequence)
        {
            float rd3 = Random.Range(0f, 1f);
            if (rd3 > probaComNothingToSay)
            {
                if(!player.enDialogue && CommentaryTimer > timeBeforeNewCommentary)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Commentary/Nelson/Nothing_To_Say");
                    CommentaryTimer = 0;
                    timer = -5;
                }
            }
            else
            {
                timer = 0;
            }
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
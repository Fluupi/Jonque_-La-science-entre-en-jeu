using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Game")]
    [SerializeField] private Bateau player;
    [SerializeField] private LevelEndDisplay levelEndDisplay;
    [SerializeField] [Range(0,2)] private int appliquerResNum;
    [SerializeField] private resultat[] res;
    float timer = 0;
    public float commentaryFrequence;
    private void Start()
    {
        res = new resultat[3];
        string[] n = { "echo", "coco", "pasGrec" };
        string[] n2 = {};

        res[0].victoire = true;
        res[0].noms = n;

        res[1].victoire = false;
        res[1].noms = n;

        res[2].victoire = false;
        res[2].noms = n2;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && !player.enDialogue)
        {
            float rd = Random.Range(0, 1);
            if (rd > .5f)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Ship_Movement");
                FMODUnity.RuntimeManager.PlayOneShot("event:/Commentary/Nelson/Rotation");
            }
            timer = 0;

            player.Kick();
        }

        if(timer > commentaryFrequence)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Commentary/Nelson/Nothing_To_Say");
            timer = -5;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            levelEndDisplay.gameObject.SetActive(true);

            //levelEndDisplay.Display(res[appliquerResNum].victoire, res[appliquerResNum].noms);
        }
    }
}

struct resultat
{
    public bool victoire;
    public string[] noms;
}
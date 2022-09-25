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
        if (Input.GetKeyDown(KeyCode.Space) && !player.enDialogue)
        {
            if(Random.Range(0,1) > .5f)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Ship_Movement");
            }

            player.Kick();
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
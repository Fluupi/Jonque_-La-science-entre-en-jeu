using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Game")]
    [SerializeField] private Bateau player;
    [SerializeField] private int energy;

    [Header("UI")]
    [SerializeField] private EnergyDisplayer scoreDisplayer;
    [SerializeField] private EnergyDisplayer energyDisplayer;

    [Header("Cheats")]
    [SerializeField] private bool infiniteEnergy;

    public Bateau bateau;
    private void Start()
    {
        scoreDisplayer.onValueUpdate.Invoke(0);
    }

    private void Update()
    {
        CheckCheats();

        if (energy > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !bateau.enDialogue)
            {
                if(Random.Range(0,1) > .5f)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/UI_Menu/Ship_Movement");
                }

                player.ResetMovingStraight();
                player.Kick();  
                energy--;
                energyDisplayer.onValueUpdate.Invoke(energy);
            }

            /*if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                int dir;

                if (Input.GetKeyDown(KeyCode.Z))
                    dir = 0;
                else if (Input.GetKeyDown(KeyCode.Q))
                    dir = 1;
                else if (Input.GetKeyDown(KeyCode.S))
                    dir = 2;
                else
                    dir = 3;

                player.StraightMove(dir);
                energy--;
                energyDisplayer.onValueUpdate.Invoke(energy);
            }*/
        }
    }

    private void CheckCheats()
    {
        if (infiniteEnergy && energy < 5)
        {
            energy++;
            energyDisplayer.onValueUpdate.Invoke(energy);
        }
    }
}

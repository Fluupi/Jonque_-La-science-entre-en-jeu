using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEndDisplay : MonoBehaviour
{
    [Header("Results")]
    [SerializeField] private CharacterResult characterResult1;
    [SerializeField] private CharacterResult characterResult2;
    [SerializeField] private CharacterResult characterResult3;

    [SerializeField] private TextMeshProUGUI generalResult;

    [Header("Details")]
    [SerializeField] private GameObject detailsGO;
    [SerializeField] private TextMeshProUGUI detailText;


    public void DisplayResults(bool victory, bool char1Victory, bool char2Victory, bool char3Victory)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Commentary/Nelson/Win");

        generalResult.text = (victory ? "Victoire !" : "Echec...");

        characterResult1.gameObject.SetActive(true);
        characterResult1.SetVictorious(char1Victory);

        characterResult2.gameObject.SetActive(true);
        characterResult2.SetVictorious(char2Victory);

        characterResult3.gameObject.SetActive(true);
        characterResult3.SetVictorious(char3Victory);
    }

    public void DisplayDetail(string details)
    {
        detailsGO.SetActive(true);

        detailText.text = details;
    }

    public void HideDetails()
    {
        detailsGO.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

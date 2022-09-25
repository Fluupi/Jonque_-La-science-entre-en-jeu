using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndDisplay : MonoBehaviour
{
    [SerializeField] private CharacterResult characterResult1;
    [SerializeField] private CharacterResult characterResult2;
    [SerializeField] private CharacterResult characterResult3;

    [SerializeField] private Image generalResult;

    [SerializeField] private Sprite generalVictorySprite;
    [SerializeField] private Sprite generalDefeatSprite;

    public void Display(bool victory, bool char1Victory, bool char2Victory, bool char3Victory)
    {
        generalResult.sprite = (victory ? generalVictorySprite : generalDefeatSprite);

        characterResult1.gameObject.SetActive(true);
        characterResult1.SetVictorious(char1Victory);
    }
}

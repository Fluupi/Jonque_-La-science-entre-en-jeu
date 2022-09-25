using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterResult : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image image;
    [SerializeField] private Button button;


    [Header("Data")]
    [SerializeField] private Sprite spriteChar;
    [SerializeField] private Sprite nameChar;
    [SerializeField] private Sprite support;

    public void SetVictorious(bool char1Victory)
    {
        throw new NotImplementedException();
    }
}

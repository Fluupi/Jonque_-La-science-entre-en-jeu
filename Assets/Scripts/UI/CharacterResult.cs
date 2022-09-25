using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterResult : MonoBehaviour
{
    [SerializeField] private LevelEndDisplay display;
    [SerializeField] private Button detailButton;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private string charWinText;
    [SerializeField] private string charLoseText;
    [SerializeField] private string detailText;

    private void Start()
    {
        detailButton.onClick.AddListener(() => display.DisplayDetail(detailText));
    }

    public void SetVictorious(bool charVictory)
    {
        text.SetText(charVictory ? charWinText : charLoseText);
    }
}

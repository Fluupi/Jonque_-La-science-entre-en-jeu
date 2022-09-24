using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image zeus;

    [SerializeField] [Tooltip("Fier/Heureux puis mécontent")] private Sprite[] zeusFaces;

    public void Display(bool victory, string[] charNames)
    {
        zeus.sprite = zeusFaces[victory ? 0 : 1];

        text.text = victory ? "Bravo ! Vous avez réussi à convaincre\n" : "Incapable ! Vous n'avez convaincu " + (charNames.Length > 0 ? "que\n" : "personne !");

        if (charNames.Length == 0)
            return;

        if(charNames.Length > 1)
        {
            for(int i=0; i<charNames.Length; i++)
            {
                text.text += charNames[i];
                if(i!= charNames.Length - 1)
                    text.text += (i != charNames.Length - 2 ? ",\n" : " et\n");
            }
        }
    }
}

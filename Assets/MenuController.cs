using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;

    private void Awake()
    {
        buttons[0].onClick.AddListener(() => SceneManager.LoadScene("Flupi"));
    }
}

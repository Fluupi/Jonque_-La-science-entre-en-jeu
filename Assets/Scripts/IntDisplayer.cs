using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class IntDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public UnityEvent<int> onValueUpdate;

    private void Start()
    {
        onValueUpdate.AddListener((x) => { text.text = x.ToString(); });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeStopRemainText : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    TextMeshProUGUI text;


    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    /// <summary>
    /// Displays the number of remaining TimeStop skills of the player.
    /// </summary>
    void Update()
    {
        text.text = player.timeStop.ToString();
    }
}

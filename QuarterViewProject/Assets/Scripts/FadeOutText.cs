using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeOutText : MonoBehaviour
{
    [SerializeField]
    float remainTime;

    float wholeTime;
    TextMeshProUGUI text;
    Color alpha;

    private void Start()
    {
        text= GetComponent<TextMeshProUGUI>();
        wholeTime = remainTime;
        alpha = text.color;
    }
    void Update()
    {
        

        alpha.a -= 1 / remainTime * Time.deltaTime;
        
        text.color = alpha;

        if(alpha.a < 0 )
        {
            gameObject.SetActive(false);
        }
    }
}

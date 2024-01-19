using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    float fadeOutTime;

    Image buttonImage;
    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
    }


    /// <summary>
    /// Change the alpha value of the image to make the UI change smooth.
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        var alpha = buttonImage.color;

        if (playerController.isMove)
        {
            alpha.a -= 1 / fadeOutTime * Time.deltaTime;
            if(alpha.a < 0 )
            {
                alpha.a = 0;
            }
            buttonImage.color = alpha;
            gameObject.GetComponent<Button>().enabled= false;
        }

        else
        {
            alpha.a += 1 / fadeOutTime * Time.deltaTime;
            if (alpha.a > 1)
            {
                alpha.a = 1;
            }
            buttonImage.color = alpha;
            gameObject.GetComponent<Button>().enabled = true;
        }
    }
}

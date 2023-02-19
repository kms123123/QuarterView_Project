using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    GameObject[] skillList;

    GameObject godModeImage;
    GameObject timeStopImage;
    GameObject speedUpImage;

    bool isGodMode;
    bool isTimeStop;
    bool isSpeedUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGodMode)
        {
            godModeImage.GetComponent<Image>().fillAmount = playerController.GodModeInTime / 3;

            if (!playerController.isGodMode)
            {
                Destroy(godModeImage);
                godModeImage= null;
                isGodMode= false;
            }
        }

        if (isTimeStop)
        {
            timeStopImage.GetComponent<Image>().fillAmount = playerController.TimeStopInTime / 3;

            if (!playerController.isTimeStop)
            {
                Destroy(timeStopImage);
                timeStopImage= null;
                isTimeStop= false;
            }
        }

        if (isSpeedUp)
        {
            speedUpImage.GetComponent<Image>().fillAmount = playerController.SpeedUpInTime / 3;

            if (!playerController.isSpeedUp)
            {
                Destroy(speedUpImage);
                speedUpImage = null;
                isSpeedUp = false;
            }
        }


    }

    public void UseGodMode()
    {
        if(!isGodMode)
        {
            isGodMode= true;
            godModeImage = Instantiate(skillList[0]);
            godModeImage.transform.SetParent(transform);
        }
    }

    public void UseTimeStop()
    {
        if(!isTimeStop)
        {
            isTimeStop = true;
            timeStopImage = Instantiate(skillList[1]);
            timeStopImage.transform.SetParent(transform);
        }
    }

    public void UseSpeedUp()
    {
        if(!isSpeedUp)
        {
            isSpeedUp = true;
            speedUpImage = Instantiate(skillList[2]);
            speedUpImage.transform.SetParent(transform);
        }
    }
}

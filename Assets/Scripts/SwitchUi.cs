using System;
using UnityEngine;

public class SwitchUi : MonoBehaviour
{
    private Transform[] santaButtons = new Transform[4];

    private int focusedButton = 0;

    private void Awake()
    {
        Transform[] tempSantaButtons = new Transform[24];
        tempSantaButtons = GetComponentsInChildren<Transform>();
        for (int i = 0; i < 4; i++)
            santaButtons[i] = tempSantaButtons[1 + i * 6];
    }

    public void SwapFocus(int buttonIndex)
    {
        
        if(buttonIndex == focusedButton)
            return;

        float extraPos = 0;
        var newPos = new Vector3(-10, -10, 0);

        if (buttonIndex == 0)
        {
            extraPos = 5;
            newPos.x -= extraPos;
            newPos.y -= extraPos;
            santaButtons[0].localPosition = newPos;
            newPos.x += extraPos;
        }
        else
        {
            santaButtons[0].localPosition = newPos;
        }
        
        for (int i = 1; i < 4; i++)
        {
            if (i == buttonIndex)
            {
                extraPos = 5;
                newPos.x -= extraPos;
                newPos.y -= (20 + extraPos);
                santaButtons[i].localPosition = newPos;
                newPos.x += extraPos;
                
            }
            else
            {
                newPos.y -= (20 + extraPos);
                santaButtons[i].localPosition = newPos;
            }
            Debug.Log(santaButtons[0].localPosition);
        }
        Debug.Log(santaButtons[0].localPosition);

    }
}

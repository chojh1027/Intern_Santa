using System;
using UnityEngine;

public class SwitchUi : MonoBehaviour
{
    private RectTransform[] santaButtons = new RectTransform[4];

    private int focusedButton = 0;

    private void Awake()
    {
        RectTransform[] tempSantaButtons = new RectTransform[24];
        tempSantaButtons = GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < 4; i++)
            santaButtons[i] = tempSantaButtons[1 + i * 6];
        
    }

    public void SwapFocus(int buttonIndex)
    {
        
        if(buttonIndex == focusedButton)
            return;
        
        focusedButton = buttonIndex;

        float extraPos = 0;
        var newPos = new Vector3(-10, -10, 0);
        var focusScale = new Vector3(30, 30, 0);
        var nomScale = new Vector3(20, 20, 0);

        if (buttonIndex == 0)
        {
            extraPos = 5;
            newPos.x -= extraPos;
            newPos.y -= extraPos;
            santaButtons[0].localPosition = newPos;
            //santaButtons[0]. = focusScale;
            newPos.x += extraPos;
        }
        else
        {
            santaButtons[0].localPosition = newPos;
            //santaButtons[0].localPosition.Scale(nomScale);
        }
        
        for (int i = 1; i < 4; i++)
        {
            if (i == buttonIndex)
            {
                extraPos = 5;
                newPos.x -= extraPos;
                newPos.y -= (20 + extraPos);
                santaButtons[i].localPosition = newPos;
                //santaButtons[i].localScale = focusScale;
                newPos.x += extraPos;
                
            }
            else
            {
                newPos.y -= (20);
                santaButtons[i].localPosition = newPos;
                //santaButtons[i].localScale = nomScale;
            }
            
        }
        
    }
}

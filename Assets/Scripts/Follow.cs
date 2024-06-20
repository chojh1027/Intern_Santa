using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    Camera mainCam;
    
    RectTransform rect;
    public Transform followSanta;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        mainCam = Camera.main;
        //followSanta = GameManager.instance.players[0].transform;
    }

    
    void FixedUpdate()
    {
        rect.position = mainCam.WorldToScreenPoint(followSanta.position);
    }
}

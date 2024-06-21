using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainSantaArea : MonoBehaviour
{
    private Transform _mainSanta;
    private BoxCollider2D _area;
    void Start()
    {
        if (_area == null) _area = GetComponent<BoxCollider2D>();
        if (_mainSanta == null) _mainSanta = GameManager.instance.players[GameManager.instance.MainSantaIndex].transform;
    }

    void Update()
    {
        transform.position = _mainSanta.position;
    }

    //이벤트로 호출되게 바꿔야함
    private void FixedUpdate()
    {
        _mainSanta = GameManager.instance.players[GameManager.instance.MainSantaIndex].transform;
    }
}

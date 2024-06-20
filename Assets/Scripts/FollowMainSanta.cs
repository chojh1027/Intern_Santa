using System;
using Cinemachine;
using UnityEngine;

public class FollowMainSanta : MonoBehaviour
{
    private Transform _mainSanta;
    private CinemachineVirtualCamera VC;

    private void Awake()
    {
        if (VC == null) VC = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        _mainSanta = GameManager.instance.players[GameManager.instance.MainSantaIndex].transform;
    }
    
    private void Update()
    {
        VC.Follow = _mainSanta;
    }

    //이벤트로 호출되게 바꿔야함
    private void FixedUpdate()
    {
        _mainSanta = GameManager.instance.players[GameManager.instance.MainSantaIndex].transform;
    }
}

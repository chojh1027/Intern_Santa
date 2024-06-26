using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Reposition : MonoBehaviour
{
    Collider2D coll;
    private Vector2 playerPos;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
        
    }

    private void Start()
    {
        //if(playerPos == Vector2.zero) playerPos = GameManager.instance.players[GameManager.instance.MainSantaIndex].transform.position;
    }

    private void Update()
    {
        playerPos = GameManager.instance.players[GameManager.instance.MainSantaIndex].transform.position;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area"))
            return;

        
        Vector3 myPos = transform.position;
        Vector3 playerDir = GameManager.instance.inputVec;
        
        switch (transform.tag)
        {
            case "Ground":
                
                float diffx = playerPos.x - myPos.x;
                float diffy = playerPos.y - myPos.y;

                float dirX = Mathf.Abs(diffx) < 20 ? 0 : diffx < 0 ? -1 : 1;
                float dirY = Mathf.Abs(diffy) < 26 ? 0 : diffy < 0 ? -1 : 1;

                var changedPos = Vector3.up * dirY * 52 + Vector3.right * dirX * 40;
                transform.Translate(changedPos);

                break;
            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f,3f),Random.Range(-3f,3f),0f));
                }
                break;
        }
    }
}

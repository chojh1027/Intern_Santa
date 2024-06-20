using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.players[GameManager.instance.MainSantaIndex].transform.position;
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

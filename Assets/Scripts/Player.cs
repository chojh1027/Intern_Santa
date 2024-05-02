using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;

    Rigidbody2D rigid;
    SpriteRenderer[] sprites;
    [SerializeField]
    Animator[] anims;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        anims = GetComponentsInChildren<Animator>();
        scanner = GetComponent<Scanner>();
    }

   
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    
    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        
        for(int i = 0; i < 4; i++)
            anims[i].SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            for(int i = 0; i < 4; i++)
            sprites[i].flipX = inputVec.x < 0;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        GameManager.instance.health -= Time.deltaTime * 10;
        
        if(GameManager.instance.health < 0)
        {
            for(int index=2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            for(int i = 0; i < 4; i++)
                anims[i].SetTrigger("Dead");
            
            GameManager.instance.GameOver();
        }
    }
}

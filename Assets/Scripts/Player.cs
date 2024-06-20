using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public SantaState state;
    
    private Rigidbody2D RB;
    private SpriteRenderer SP;
    private Animator ANIM;

    //private Vector2[] santaPosForm = { new(0f, 0f), new(0f, 2f), new(-1.7f, 1.2f), new(1.7f, 1.2f) };
    private static readonly int AnimSpeed = Animator.StringToHash("Speed");
    private static readonly int AnimDead = Animator.StringToHash("Dead");

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        SP = GetComponent<SpriteRenderer>();
        ANIM = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();

        state = SantaState.ChaseMain;
    }

   
    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        inputVec = GameManager.instance.inputVec;

        switch (state)
        {
            
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        
    }
    
    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        
        if (inputVec.x != 0)
        {
            SP.flipX = inputVec.x < 0;
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        GameManager.instance.currentHealth -= Time.deltaTime * 10;
        
        if(GameManager.instance.currentHealth < 0)
        {
            ANIM.SetTrigger(AnimDead);
            
            GameManager.instance.GameOver();
        }
    }
}

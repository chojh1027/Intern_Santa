using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public SantaState state;
    public int SantaIndex;
    
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
        scanner = GetComponentInChildren<Scanner>();

        state = SantaState.ChaseMain;
    }

   
    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        inputVec = GameManager.instance.inputVec;

        switch (state)
        {
            case SantaState.MainSanta:
                MainSanta();
                break;
            case SantaState.ChaseMain:
                ChaceMain();
                break;
            case SantaState.NormalAtt:
                break;
            case SantaState.SpecialAtt:
                break;
            case SantaState.Dead:
                break;
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
        
        if (RB.velocity.x != 0)
        {
            SP.flipX = RB.velocity.x < 0;
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        if (!GameManager.instance.isLive)
            return;

        GameManager.instance.currentHealth[SantaIndex] -= Time.deltaTime * 10;
        
        if(GameManager.instance.currentHealth[SantaIndex] < 0)
        {
            state = SantaState.Dead;
            ANIM.SetTrigger(AnimDead);
            
            GameManager.instance.GameOver();
        }
    }

    private void MainSanta()
    {
        //var dirVec = GameManager.instance.inputVec;
        RB.velocity = inputVec * speed;
    }

    private bool chaseInRange;
    private float chaseTimer;
    private void ChaceMain()
    {
        var dir = GameManager.instance.players[GameManager.instance.MainSantaIndex].GetComponent<Rigidbody2D>()
            .position - RB.position;
        if (dir.sqrMagnitude > 10f)
        {
            RB.velocity = dir.normalized * speed;
        }
        else
        {
            
            RB.velocity = inputVec * (speed * 0.5f);
        }
    }
}

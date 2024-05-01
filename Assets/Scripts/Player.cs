using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;

    private Rigidbody2D[] rigids;
    private SpriteRenderer[] sprites;
    [SerializeField]
    private Animator[] anims;

    private Vector3 dirVec;
    private Vector3[] santaPoss = new Vector3[3];
    void Awake()
    {
        rigids = GetComponentsInChildren<Rigidbody2D>();
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

        Vector2 nextVec = inputVec * (speed * Time.fixedDeltaTime);
        
        //rigids[0].MovePosition(rigids[0].position + nextVec);

        transform.position += new Vector3(nextVec.x, nextVec.y, 0);

        
        for (int i = 0; i < 5; i++)
        {
            
        }
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();

        if (!inputVec.Equals(Vector3.zero))
        {
            dirVec = inputVec;

            santaPoss[0] = dirVec * 1.7f;
            santaPoss[1].x = santaPoss[0].x * -0.5f - santaPoss[0].y * 0.886f;
            santaPoss[1].y = santaPoss[0].x * 0.886f + santaPoss[0].y * -0.5f;
            santaPoss[2].x = santaPoss[0].x * -0.5f - santaPoss[0].y * -0.886f;
            santaPoss[2].y = santaPoss[0].x * -0.886f + santaPoss[0].y * -0.5f;
        }
            
        
        
    }

    //인자값(santa)의 santaPos와 santaPos가 센터인 santa의 santaPos를 스왑
    public void ChangeToCenter()
    {
        
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

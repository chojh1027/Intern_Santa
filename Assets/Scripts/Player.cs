using System.Collections;
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
    
    private Transform[] santaNowPoss;

    private Vector3 dirVec;
    private Vector2[] santaPoss = new Vector2[3];
    private int centerSantaIndex = 1;
    void Awake()
    {
        rigids = GetComponentsInChildren<Rigidbody2D>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        anims = GetComponentsInChildren<Animator>();
        scanner = GetComponent<Scanner>();
        santaNowPoss = GetComponentsInChildren<Transform>();
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
        
        transform.position += new Vector3(nextVec.x, nextVec.y, 0);
        
        if(Input.GetKeyDown(KeyCode.Space))
            ChangeToCenter(2);
        
        //for(int i = 0; i < 3; i++)
        //{
        //    new
        //    rigids[i + 2].MovePosition(santaPoss[i] * (speed * Time.fixedDeltaTime));
        //}
        
        //var maxDis = (rigids[centerSantaIndex].position - (Vector2)transform.position).magnitude > 3f;
        //
        //if(maxDis)
        //    return;
        //rigids[0].MovePosition(rigids[1].position + nextVec);

    }

    //인자값(santa)의 santaPos와 santaPos가 센터인 santa의 santaPos를 스왑
    public void ChangeToCenter(int index)
    {
        Debug.Log("ctccalled");
        StartCoroutine(CTCRoutine(2));
    }

    private IEnumerator CTCRoutine(int index)
    {
        Debug.Log("Routine started");
        var startLoPos = santaNowPoss[index].localPosition;
        var targetLoPos = santaNowPoss[centerSantaIndex].localPosition;
        var dir = targetLoPos - startLoPos;
        float velo = 1;
        float timedelta = 0;
        while (true)
        {
            santaNowPoss[index].localPosition += dir * (velo * Time.fixedDeltaTime);
            santaNowPoss[centerSantaIndex].localPosition += dir * (velo * Time.fixedDeltaTime);

            timedelta += Time.fixedDeltaTime;
            
            if (timedelta > 1.0)
                break;
        }
        santaNowPoss[index].localPosition = targetLoPos;
        santaNowPoss[centerSantaIndex].localPosition = startLoPos;
        centerSantaIndex = index;
        yield return null;
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

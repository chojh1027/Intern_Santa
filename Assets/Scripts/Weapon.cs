using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
    public float skillSpeed;
    public float skillInter;
    public bool usingSkill = true;

    float timer;
    float skillTimer;
    float skillInterTimer;
    Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        if (usingSkill)
        {
            UseSkill();
            return;
        }
        
        if(player.scanner.nearestTarget == null)
            return;

        

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * (speed * Time.deltaTime));
                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    
                    Fire(player.scanner.nearestTarget.position);
                }
                break;

        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = -150f;
                Batch();

                break;
            default:
                speed = 0.3f;
                break;

        }
    }
    
    void Batch()
    {
        for (int index=0; index<count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
        }
    }

    void Fire(Vector3 targetPos)
    {
        

        
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }

    private void UseSkill()
    {
        if (skillTimer < skillSpeed)
        {
            if (skillInterTimer > skillInter)
            {
                Vector2 myPos = transform.position;
                Fire(UnityEngine.Random.insideUnitCircle + myPos);
                skillInterTimer = 0f;
            }
            
            skillTimer += Time.deltaTime;
            skillInterTimer += Time.deltaTime;
        }
        else
        {
            skillTimer = 0f;
            usingSkill = false;
        }
    }

    public void ActiveSkill()
    {
        usingSkill = true;
    }
}

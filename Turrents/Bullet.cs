using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;
    private float speed;
   
    private ParticleSystem hitEffect;
    private bool hitTarget = false;
    private int damage;
    public bool HitTarget { get => hitTarget; set => hitTarget = value; }
    public int Damage { get => damage; set => damage = value; }

    //public Transform Target { get => target; set => target = value; }
    //public float Speed { get => speed; set => speed = value; }
    //public int Damage { get => damage; set => damage = value; }

    private void Awake()
    {
        hitEffect = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        //problem here with it not destroying the bullet on hit andpalying the particle effect
        //if (target == null)
        //{
        //    if (hitEffect == null)
        //    {
        //        Destroy(gameObject);
        //        return;
        //    }
        //    if (HitTarget) return;
        //    hitEffect.Play();
        //    Destroy(hitEffect, 1f);
        //    Destroy(gameObject, 1f);
        //    return;
        //}

        if (target == null)
        {
            if (HitTarget) return;
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject,1f);

            return;
        }
            
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //if (HitTarget)
        //{
        //    HitTargetLogic();
        //}
        //if (direction.magnitude <= distanceThisFrame)
        //{
   
        //    return;
        //}
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    public void HitTargetLogic()
    {
        // Add logic to damage the target
        //Debug.Log("hit " + target.name + " for " + damage + " damage.");
        //Enemy enemyStats = target.GetComponent<Enemy>();
        //enemyStats.CurrentHealth -= Damage;
        // Disables any 2D collider on the bullet, regardless of type
        if(HitTarget) return;
        HitTarget = true;
        Collider2D bulletCollider = GetComponent<Collider2D>();
        if (bulletCollider != null)
            bulletCollider.enabled = false;
        hitEffect.Play();
        // Disable the bullet's visual and collider
        GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject,10f);
        

    }

    public void Fire(Transform target, int damage, float speed)
    {
        this.target = target;
        this.Damage = damage;
        this.speed = speed;
    }
}

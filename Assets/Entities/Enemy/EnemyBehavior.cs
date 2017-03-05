using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float health = 150;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(collision);

        //we check that the thing we bumped into has a projectile component (o laser tem o projectile component)
        //chamar de missil o que ta colidindo, desde que seja projectile
        Projectile missile = collision.gameObject.GetComponent<Projectile>(); 
        if (missile) //se o missile existe
        {
            health -= missile.GetDamage();
            missile.Hit(); //destroi o missil 
            if (health <= 0)
            {             
                Destroy(gameObject);
            }
            Debug.Log("Hit by a projectile");
        }
    }
}

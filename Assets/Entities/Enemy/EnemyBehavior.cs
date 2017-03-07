using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float health = 150;  
    public GameObject projectile;
    public float projectileSpeed=5;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 150;

    public AudioClip fireSound;
    public AudioClip deathSound;

    private ScoreKeeper scoreKeeper;

    private void Start()
    {
       scoreKeeper =  GameObject.Find("Score").GetComponent<ScoreKeeper>(); //return an object of type scorekeeper
    }

    private void Update()
    {
        float probability = Time.deltaTime * shotsPerSecond;
        //turning a probability into a value
        if (Random.value < probability) //basically true 80% of time

        {
            Fire();
        }
       
    }

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
                Die();
            }
           // Debug.Log("Hit by a projectile");
        }
    }


    void Fire()
    {
       
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    void Die()
    {
        Destroy(gameObject);
        scoreKeeper.Score(scoreValue);
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
    }
}

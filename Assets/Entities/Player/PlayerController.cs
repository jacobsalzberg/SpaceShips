using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Vector3 shipPos;
    public float speed;
    public float padding = 0.1F;
    public GameObject projectile;
    public float projectileSpeed;
    public float firingRate = 0.2f;
    public float health = 250;

    public AudioClip fireSound;

    float xmin;
    float xmax;
    // Use this for initialization
    void Start ()
    {
        //viewport is relative to the camera
        //camera.main <-- this camera. viewporttoworldpoint referts to planes, mostly in 3D!
        float distance = transform.position.z - Camera.main.transform.position.z;
        //a vector3 position in the world
        
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x+padding;
        xmax = rightmost.x-padding;
    }

    // Update is called once per frame
    void Update()
    {
        //shipPos = this.transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            // VERSAO ANTIGA
            //Vector3 shipMove = new Vector3(speed * Time.deltaTime, 0, 0);
            //shipPos = shipPos - shipMove;
            //this.transform.position = shipPos;

            //codigo melhorado
            this.transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            //Vector3 shipMove = new Vector3(speed * Time.deltaTime, 0, 0);
            //shipPos = shipPos + shipMove;
            //this.transform.position = shipPos;

            this.transform.position += Vector3.right * speed * Time.deltaTime;
        }
    /*   // if (Input.GetKey(KeyCode.W))
        {
            //Vector3 shipMove = new Vector3(0, speed * Time.deltaTime, 0);
            //shipPos = shipPos + shipMove;
            //this.transform.position = shipPos;

         //   this.transform.position += Vector3.up * speed * Time.deltaTime;
        }
       // else if (Input.GetKey(KeyCode.S))
        {
            //Vector3 shipMove = new Vector3(0, speed * Time.deltaTime, 0);
            //shipPos = shipPos - shipMove;
            //this.transform.position = shipPos;

            this.transform.position += Vector3.down * speed * Time.deltaTime;
        } */

        //Restrict the Player to the gamespace
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire",0.000001f,firingRate);           
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Plaed collied with the missile);

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
            // Debug.Log("Hit by a projectile");
        }
    }

    void Fire()
    {
        //offset
        Vector3 offset = new Vector3(0, 1, 0);

        GameObject beam = Instantiate(projectile, transform.position+offset, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
}

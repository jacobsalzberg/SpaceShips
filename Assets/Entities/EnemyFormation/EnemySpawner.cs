using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float width;
    public float height;
    public float speed;
    
    private bool movingRight = true;
    private float xmin;
    private float xmax;

    // Use this for initialization
    void Start ()
    {
        //takes an object, position and a quaternion (sobre a rotacao)
     
        foreach(Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;                 //parent atributed to this
        }
        
        // Calculate Edges of the Screen
        Vector3 leftBoundry = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));
        Vector3 rightBoundry = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        xmax = leftBoundry.x;
        xmin = rightBoundry.x;
    }

    
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height,0));
    }

    // Update is called once per frame
    void Update ()
    {
        if (movingRight)
        {
            this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);  //deltatime -> movement independant
            // OR
            // transform.position += Vector3.right*speed*Time.deltaTime;

        }
        else  //moving left
        {
            this.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0); //deltatime -> movement independant
        }

        float rightEdgeOfFormation = transform.position.x + 0.5f * width;
        float leftEdgeOfFormation = transform.position.x - 0.5f * width;

        if (leftEdgeOfFormation < xmin)
        {
            movingRight = true;
        }
        else if (rightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }
       


        //float limitX = Mathf.Clamp(transform.position.x, xmin, xmax);
        //this.transform.position = new Vector3(limitX, transform.position.y, transform.position.z);


    }
}

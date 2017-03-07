using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float width;
    public float height;
    public float speed;
    public float spawnDelay = 0.5f;

    private bool movingRight = true;
    private float xmin;
    private float xmax;

    // Use this for initialization
    void Start()
    {
        //takes an object, position and a quaternion (sobre a rotacao)

        SpawnUntilFull();

        // Calculate Edges of the Screen
        Vector3 leftBoundry = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));
        Vector3 rightBoundry = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        xmax = leftBoundry.x;
        xmin = rightBoundry.x;
    }

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;                 //parent atributed to this
        }
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition != null) //so spawna se tiver posicao livre
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition()) // only spawn an enemy after all are dead
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
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

        if (AllMembersDead())
        {
            Debug.Log("Empty formation");
            SpawnUntilFull();
        }

        //float limitX = Mathf.Clamp(transform.position.x, xmin, xmax);
        //this.transform.position = new Vector3(limitX, transform.position.y, transform.position.z);


    }
    private bool AllMembersDead()
    {
        //transform is going to be what we loop through all positions (a bit weird, but whatever, thats just how it is)

        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
        
    }
    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }
}

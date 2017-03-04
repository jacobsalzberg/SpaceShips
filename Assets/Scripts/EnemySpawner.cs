using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
	// Use this for initialization
	void Start ()
    {
        //takes an object, position and a quaternion (sobre a rotacao)
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        enemy.transform.parent = this.transform; //parent atributed to this

        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}

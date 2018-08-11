using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject[] DataPieces;
	public GameObject[] Enemies;
	
	// Update is called once per frame
	void Update () {

		if (UnityEngine.Random.Range (0,100) == 0) {

			Instantiate (DataPieces [UnityEngine.Random.Range (0, DataPieces.Length)], 
				new Vector3 (UnityEngine.Random.Range (transform.position.x - GetComponent<SpriteRenderer> ().bounds.size.x / 2,
					transform.position.x + GetComponent<SpriteRenderer> ().bounds.size.x / 2), transform.position.y, 0),
				Quaternion.identity);
		}

		if (UnityEngine.Random.Range (0,150) == 0) {

			Instantiate (Enemies [UnityEngine.Random.Range (0, Enemies.Length)], 
				new Vector3 (UnityEngine.Random.Range (transform.position.x - GetComponent<SpriteRenderer> ().bounds.size.x / 2,
					transform.position.x + GetComponent<SpriteRenderer> ().bounds.size.x / 2), transform.position.y, 0),
				Quaternion.identity);
		}

	}
}

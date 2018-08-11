using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject[] DataPieces;
	public GameObject[] Enemies;

	private int dataPieceChance, enemyChance;

	private int minEnemyDifficulty, maxEnemyDifficulty;

	private float timer = 0.0f;

	void Start () {

		dataPieceChance = 200;
		enemyChance = 150;
		minEnemyDifficulty = 0;
		maxEnemyDifficulty = Enemies.Length/2;

	}

	// Update is called once per frame
	void Update () {

		if (UnityEngine.Random.Range (0,dataPieceChance) == 0) {

			Instantiate (DataPieces [UnityEngine.Random.Range (0, DataPieces.Length)], 
				new Vector3 (UnityEngine.Random.Range (transform.position.x - GetComponent<SpriteRenderer> ().bounds.size.x / 2,
					transform.position.x + GetComponent<SpriteRenderer> ().bounds.size.x / 2), transform.position.y, 0),
				Quaternion.identity);
		}

		if (UnityEngine.Random.Range (0,enemyChance) == 0) {

			Instantiate (Enemies [UnityEngine.Random.Range (minEnemyDifficulty, maxEnemyDifficulty)], 
				new Vector3 (UnityEngine.Random.Range (transform.position.x - GetComponent<SpriteRenderer> ().bounds.size.x / 2,
					transform.position.x + GetComponent<SpriteRenderer> ().bounds.size.x / 2), transform.position.y, 0),
				Quaternion.identity);
		}

		if (timer > 1.8f) {
			if (dataPieceChance > 50) 
				dataPieceChance--;

			if (enemyChance > 50) 
				enemyChance--;

			if (enemyChance == 120){
				//minEnemyDifficulty++;
				maxEnemyDifficulty++;
			}

			if (enemyChance == 100){
				//minEnemyDifficulty++;
				maxEnemyDifficulty++;
			}

			if (enemyChance == 70){
				//minEnemyDifficulty++;
				maxEnemyDifficulty++;
			}
			timer = 0;
		}

		timer += Time.deltaTime;
	}
}

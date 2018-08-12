using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject[] DataPieces;
	public GameObject[] Enemies;

	public GameObject EliminationBox;

  public float window_cd, window_limit;
	private int dataPieceChance, enemyChance, windowChance;

	private int minEnemyDifficulty, maxEnemyDifficulty;

	private float timer = 0.0f;

	void Start () {

    windowChance = 100000;
		dataPieceChance = 200;
		enemyChance = 150;
		minEnemyDifficulty = 0;
		maxEnemyDifficulty = Enemies.Length/2;

	}

	// Update is called once per frame
	void Update () {

		if (GameManager.instance.game_state != GameManager.GameState.PauseMenu){

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

			if (UnityEngine.Random.Range(0,windowChance) == 0 || window_cd >= window_limit) {

				Vector3 newPos = new Vector3 (UnityEngine.Random.Range (-3.98f,3.89f), UnityEngine.Random.Range (-3.75f,3.39f), 0 );

				Instantiate (EliminationBox, newPos, Quaternion.identity);
	      window_cd = 0.0f;
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
	    window_cd += Time.deltaTime;
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trojan : Enemy {

	private Animator animator;

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator> ();

		rb2d.velocity = Vector2.down * speed * Time.deltaTime;

	}

	// Update is called once per frame
	void Update () {

		if (animator.GetBool("ToGlitch"))
			animator.SetBool ("ToGlitch", false);

		Vector3 hitStartPos = transform.position - new Vector3 (0, GetComponent<SpriteRenderer> ().size.y / 2 + 0.5f, 0);

		RaycastHit2D hit = Physics2D.Raycast(hitStartPos, Vector2.down);

		if (hit.collider != null) {

			if (hit.collider.tag == "DataPiece") {
				
				float distance = Mathf.Abs (hit.point.y - hitStartPos.y);

				distance = distance * 20;

				if (UnityEngine.Random.Range (0, (int)distance) == 0)
					animator.SetBool ("ToGlitch", true);
			}

		}
			
	}

	void OnCollisionEnter2D (Collision2D col){

		if (col.gameObject.tag == "DataPiece" && col.gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static) {
			rb2d.bodyType = RigidbodyType2D.Static;
		}

		if (col.gameObject.tag == "Bullet") {
			lives--;
			if (lives == 0) {
				Destroy (gameObject);
			}
		}
	}
}

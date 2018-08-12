using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {

  public AnimationClip explosion;

	// Use this for initialization
	void Start () {
    Destroy(gameObject, explosion.length);
	}
}

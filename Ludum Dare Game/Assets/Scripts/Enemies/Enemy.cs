﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int lives, score;
	public float corruptionLevel;

	public float speed;

	protected Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () {

		rb2d = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

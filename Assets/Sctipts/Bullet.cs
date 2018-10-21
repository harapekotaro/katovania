using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

	public float speed;
	public float destroyTime;

	public Rigidbody2D cRigidbody2D
	{
		get
		{
			return _cRigidbody2D ? _cRigidbody2D : _cRigidbody2D = GetComponent<Rigidbody2D>();
		}
	}
	Rigidbody2D _cRigidbody2D;

	void FixedUpdate()
	{
		cRigidbody2D.velocity = new Vector2(speed, cRigidbody2D.velocity.y);
		destroyTime -= Time.deltaTime;
		if(destroyTime <= 0) Destroy(gameObject);
	}
}
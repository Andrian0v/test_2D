﻿using UnityEngine;
using System.Collections;
public class PlatformerPlayer : MonoBehaviour 
{
	public float speed = 250.0f;
	private Rigidbody2D _body;
	private Animator _anim;
	public float jumpForce = 12.0f;
	private BoxCollider2D _box;
	void Start () 
	{
		_body = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
		_box = GetComponent<BoxCollider2D>();
	}
	void Update() 
	{
		float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		Vector2 movement = new Vector2(deltaX, _body.velocity.y);
		_body.velocity = movement;
		bool grounded = false;
		Vector3 max = _box.bounds.max;
		Vector3 min = _box.bounds.min;
		Vector2 corner1 = new Vector2(max.x, min.y - .1f);
		Vector2 corner2 = new Vector2(min.x, min.y - .2f);
		Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
		if (hit != null) 
		{
			grounded = true;
		}
		_body.gravityScale = grounded && deltaX == 0 ? 0 : 1;
		if (grounded && Input.GetKeyDown(KeyCode.Space))
		{
			_body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
		MovingPlatform platform = null;
		if (hit != null) 
		{
			platform = hit.GetComponent<MovingPlatform>();
		}
		_anim.SetFloat("speed", Mathf.Abs(deltaX));
		Vector3 pScale = Vector3.one;
		if (platform != null) 
		{
			transform.parent = platform.transform;
			pScale = platform.transform.localScale;
		}
		else 
		{
			transform.parent = null;
		}
		if (deltaX != 0) 
		{
			transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1/pScale.y, 1);
		}
		_anim.SetFloat("speed", Mathf.Abs(deltaX));
		_anim.SetFloat("speed", Mathf.Abs(deltaX));
		if (!Mathf.Approximately(deltaX, 0)) 
		{
			transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
		}

	}
}

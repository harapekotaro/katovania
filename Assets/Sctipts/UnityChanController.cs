using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class UnityChanController : MonoBehaviour {

	public float moveSpeed = 5;
	public float jumpForce = 1000;
	public Transform groundcheck;
	public float checkRodius;
	public LayerMask WhatIsGround;

	float InputHorValue;

	bool isGrounded;
	bool canJump;
	bool jumpmode;

	public Rigidbody2D cRigidbody2D
	{
		get
		{
			return _cRigidbody2D ? _cRigidbody2D : _cRigidbody2D = GetComponent<Rigidbody2D>();
		}
	}
	Rigidbody2D _cRigidbody2D;

	public Transform cTransform
	{
		get
		{
			return _cTransform ? _cTransform : _cTransform = GetComponent<Transform>();
		}
	}
	Transform _cTransform;

	public Animator cAnimator
	{
		get
		{
			return _cAnimator ? _cAnimator : _cAnimator = GetComponent<Animator>();
		}
	}
	Animator _cAnimator;
	void Update()
	{
		InputCheck();
		MecCheck();
	}

	void FixedUpdate()
	{
		Move();
		Jump();
	}
	void InputCheck()
	{
		InputHorValue = Input.GetAxisRaw("Horizontal");
		if(isGrounded && Input.GetButtonDown("Jump")) {
			canJump = true;
			jumpmode = true;
		}
		else if(jumpmode && Input.GetButtonUp("Jump")){
			if(cRigidbody2D.velocity.y > 0){
				cRigidbody2D.velocity = new Vector2 (cRigidbody2D.velocity.x, cRigidbody2D.velocity.y * 0.5f);
			}
			jumpmode = false;
		}
	}

	void MecCheck()
	{
		isGrounded = Physics2D.OverlapCircle(groundcheck.position, checkRodius, WhatIsGround);
		bool isRunning = InputHorValue != 0;
		float velY = cRigidbody2D.velocity.y;
		bool isJumping = velY > 0.1f ? true:false;
		bool isFalling = velY < -0.1f ? true:false;
		if(isFalling) jumpmode = false;
		cAnimator.SetBool("isRunning",isRunning);
		cAnimator.SetBool("isJumping",isJumping);
		cAnimator.SetBool("isFalling",isFalling);
		cAnimator.SetBool("isGrounded",isGrounded);
	}


	void Move()
	{
		if((cTransform.localScale.x > 0 && Input.GetAxisRaw("Horizontal") <0)
			|| (cTransform.localScale.x < 0 && Input.GetAxisRaw("Horizontal") > 0))
		{
			Vector2 temp = cTransform.localScale;
			temp.x *= -1;
			cTransform.localScale = temp;
		}

		cRigidbody2D.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"),
		cRigidbody2D.velocity.y);
	}

	void Jump()
	{
		if(canJump)
		{
			jumpmode = true;
			canJump = false;
			cRigidbody2D.AddForce(Vector2.up * jumpForce);
		}
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public LayerMask layerGround;
	public LayerMask layerEnemy;
	bool grounded = false;

	public float jumpForce = 15.0f;

	public float airSpeedLimit = 10.0f;
	public float airSpeedFactor = 0.3f;

	public float playerTorque = 80.0f;

	public Vector2 spawnPoint;

	public int numJumps = 1;
	private int jumpNumber = 0;

	private float move;

	private bool leftWall;
	private bool rightWall;
	private float boundingSize;
	private float curMoveSpeed;
	
	// Use this for initialization
	void Start()	
	{
		spawnPoint = transform.position;
		boundingSize = 0.32f;
	}

	// FixedUpdate is called once per frame
	void FixedUpdate()	
	{
//		Debug.Log(rigidbody2D.velocity.x);
		GroundCheck();
		WallCheck();

		if(grounded)
		{
			jumpNumber = 0;
		}
	
		move = Input.GetAxisRaw("Horizontal");

		rigidbody2D.AddTorque(move * -playerTorque);
		if(move == 0 && rigidbody2D.angularVelocity > 0)
			rigidbody2D.angularVelocity -= rigidbody2D.angularVelocity/10;
		else if(move == 0 && rigidbody2D.angularVelocity < 0)
			rigidbody2D.angularVelocity += Mathf.Abs(rigidbody2D.angularVelocity/10);
		/*
		if(grounded)
		{
			jumpNumber = 0;
		}
*/

		if (move != 0)
		{
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, rigidbody2D.velocity.y);
			if(move == -1 && rigidbody2D.velocity.x > -airSpeedLimit)
			{
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x - airSpeedFactor, rigidbody2D.velocity.y);
			}
			if(move == 1 && rigidbody2D.velocity.x < airSpeedLimit)
			{
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x + airSpeedFactor, rigidbody2D.velocity.y);
			}
		}
	}

	void Update()
	{
		if(jumpNumber < numJumps && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
		{
			//Debug.Log(jumpNumber);
			jumpNumber += 1;
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
		}

	}

	void OnTriggerEnter2D (Collider2D col)
	{

	}

	public void Respawn()
	{
		GetComponent<SpriteRenderer>().color = Color.black;
		rigidbody2D.fixedAngle = true;
		rigidbody2D.velocity = new Vector2(0,0);
		this.rigidbody2D.transform.rotation = new Quaternion(0,0,0,0);
		transform.position = spawnPoint;
	}

	private void GroundCheck()
	{
		if(Physics2D.Raycast(this.transform.position, -Vector2.up, boundingSize + 0.01f, layerGround))
		{
			grounded = true;
		}
		else
			grounded = false;
	}

	private void WallCheck()
	{
		/*
		if(!grounded && Physics2D.Raycast(this.transform.position, -Vector2.up, boundingSize + (0.01f * (rigidbody2D.velocity.y * -1.5f)), layerGround))
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0.2f * (Mathf.Abs(rigidbody2D.velocity.y) + 0.5f));
			Debug.Log (rigidbody2D.velocity.y);
		}*/

		if(Physics2D.Raycast(this.transform.position, -Vector2.right, boundingSize + (0.01f * (rigidbody2D.velocity.x * -2.0f)), layerGround))
		{
			Debug.Log("Left");

			if(rigidbody2D.velocity.x == 0)
				Debug.Log ("Left: 0");

			rigidbody2D.AddForce(new Vector2(200 * (Mathf.Abs(rigidbody2D.velocity.x) + 0.5f), 0));
		}
		else if(Physics2D.Raycast(this.transform.position, Vector2.right, boundingSize + (0.01f * (rigidbody2D.velocity.x * 2.0f)), layerGround))
		{
			Debug.Log("Right");

			if(rigidbody2D.velocity.x == 0)
				Debug.Log ("Right: 0");

			rigidbody2D.AddForce(new Vector2(-200 * (Mathf.Abs(rigidbody2D.velocity.x) + 0.5f), 0));
		}
		else{

		}
	}
}




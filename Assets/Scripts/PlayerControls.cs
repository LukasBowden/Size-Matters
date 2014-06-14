using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour
{
	public LayerMask layerGround;
	public LayerMask layerEnemy;
	bool grounded = false;

	public float jumpForce = 15.0f;

	public float airSpeedLimit = 10.0f;
	public float airSpeedFactor = 0.3f;

	public float playerTorque = 80.0f;

	public int numJumps = 1;
	private int jumpNumber = 0;

	public float attackOneCD = 0.2f;
	private float curAttackOneCD = 0.2f;
	public float attackTwoCD = 2.0f;
	private float curAttackTwoCD = 2.0f;
	public float attackThreeCD = 5.0f;
	private float curAttackThreeCD = 5.0f;

	public float chargeTime = 2.0f;
	private float curChargeTime = 0.0f;
	private Vector2 prevPos;
	private bool charging = false;

	public float jumpTime = 0.2f;
	private float curJumpTime = 0.0f;
	private bool jumpAttack;

	private short move;
	private short facing;

	private bool leftWall;
	private bool rightWall;
	private float boundingSize;
	private float curMoveSpeed;

	private float scale;

	//adsasdasdasdasdasdasd
	public GameObject Enemy;
	//asdasdasdasdasdasdasd

	// Use this forinitialization
	void Start()	
	{
		boundingSize = 0.32f * rigidbody2D.transform.localScale.x;
	}

	// FixedUpdate is called once per frame
	void FixedUpdate()	
	{
		scale = this.gameObject.GetComponent<PlayerSystems>().scale;
		rigidbody2D.transform.localScale = new Vector3(scale, scale, 1);
		boundingSize = 0.32f * rigidbody2D.transform.localScale.x;

		GroundCheck();
		WallCheck();

		if(grounded)
		{
			jumpNumber = 0;
		}
	
		if(move == -1)
			facing = -1;
		if(move == 1)
			facing = 1;

		rigidbody2D.AddTorque(move * -playerTorque * (scale*2));
		if(move == 0 && rigidbody2D.angularVelocity > 0)
			rigidbody2D.angularVelocity -= rigidbody2D.angularVelocity/10;
		else if(move == 0 && rigidbody2D.angularVelocity < 0)
			rigidbody2D.angularVelocity += Mathf.Abs(rigidbody2D.angularVelocity/10);
		/*
		if(grounded)
		{
			jumpNumber = 0;
		}*/

		if (move != 0)
		{
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, rigidbody2D.velocity.y);
			if(move == -1 && rigidbody2D.velocity.x > -airSpeedLimit && !Input.GetKey (KeyCode.S))
			{
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x - airSpeedFactor, rigidbody2D.velocity.y);
			}
			if(move == 1 && rigidbody2D.velocity.x < airSpeedLimit && !Input.GetKey (KeyCode.S))
			{
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x + airSpeedFactor, rigidbody2D.velocity.y);
			}
		}
		if(jumpAttack == true)
			curJumpTime += 0.02f;
		curAttackOneCD += 0.02f;
		curAttackTwoCD += 0.02f;
		curAttackThreeCD += 0.02f;
	}
	void Update()
	{
		//Movement
		if (Input.GetKey (KeyCode.RightArrow))
			move = 1;
		else if (Input.GetKey (KeyCode.LeftArrow))
			move = -1;
		else if(!Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.RightArrow))
			move = 0;
		//Movement END

		if (grounded && (Input.GetKey(KeyCode.UpArrow))) 
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce + scale);
		}
		if(jumpNumber < numJumps && Input.GetKeyDown(KeyCode.UpArrow))
		{
			//Debug.Log(jumpNumber);
			jumpNumber += 1;
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce + scale);
		}
	
		//Player Attacks
		AttackOne();
		AttackTwo();
		AttackThree();

		//SudoEnemySpawning
		if (Input.GetMouseButton (0))
			Instantiate (Enemy, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
		//END SudoEnemySpawning
	}

	private void AttackOne()
	{
		if(Input.GetKey(KeyCode.A) && curAttackOneCD >= attackOneCD)
		{
			RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.transform.position, facing * Vector2.right, boundingSize + 5.0f , layerEnemy);
			Debug.Log("hit1");
			if(hit.collider != null)
			{
				hit.transform.gameObject.GetComponent<Enemy>().Die();
			}
			curAttackOneCD = 0.0f;
		}

	}

	private void AttackTwo()
	{
		if (curAttackTwoCD >= attackTwoCD && grounded && !charging)
			prevPos = rigidbody2D.transform.position;
		if (Input.GetKey (KeyCode.S) && curAttackTwoCD >= attackTwoCD && grounded)
		{
			charging = true;
			rigidbody2D.transform.position = prevPos;
			curChargeTime += Time.deltaTime;
		}
		if (Input.GetKeyUp (KeyCode.S) && curChargeTime < chargeTime && curAttackTwoCD >= attackTwoCD)
		{
			charging = false;
			rigidbody2D.velocity = new Vector2(20.0f * facing, rigidbody2D.velocity.y);
			curChargeTime = 0.0f;
			curAttackTwoCD = 0.0f;
		}
		if (Input.GetKeyUp (KeyCode.S) && curChargeTime > chargeTime * 1 && curChargeTime < chargeTime * 2 && curAttackTwoCD >= attackTwoCD)
		{
			charging = false;
			rigidbody2D.velocity = new Vector2(40.0f * facing, rigidbody2D.velocity.y);
			curChargeTime = 0.0f;
			curAttackTwoCD = 0.0f;
		}
		if (Input.GetKeyUp (KeyCode.S) && curChargeTime > chargeTime * 2 && curAttackTwoCD >= attackTwoCD)
		{
			charging = false;
			rigidbody2D.velocity = new Vector2(60.0f * facing, rigidbody2D.velocity.y);
			curChargeTime = 0.0f;
			curAttackTwoCD = 0.0f;
		}
	}
	private void AttackThree()
	{
		if (Input.GetKeyDown (KeyCode.D) && curAttackThreeCD >= attackThreeCD && !jumpAttack)
		{
			jumpAttack = true;
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 20.0f + (scale * 5));
		}
		if(jumpAttack && curJumpTime > 0.3f) 
		{
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, -50.0f - (scale * 5));
			curAttackThreeCD = 0.0f;
			curJumpTime = 0.0f;
			jumpAttack = false;
		}
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
		if(Physics2D.Raycast(this.transform.position, -Vector2.right, boundingSize + (0.01f * (rigidbody2D.velocity.x * -2.0f)), layerGround))
		{
			rigidbody2D.AddForce(new Vector2(200 * (Mathf.Abs(rigidbody2D.velocity.x) + 0.5f), 0));
		}
		else if(Physics2D.Raycast(this.transform.position, Vector2.right, boundingSize + (0.01f * (rigidbody2D.velocity.x * 2.0f)), layerGround))
		{
			rigidbody2D.AddForce(new Vector2(-200 * (Mathf.Abs(rigidbody2D.velocity.x) + 0.5f), 0));
		}
		else{

		}
	}
}




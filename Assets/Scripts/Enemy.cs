using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	//Generic enemy class, Holds health and meat on death

	public int minMeat = 3;
	public int maxMeat = 10;
	private int numMeat1 = 0;
	private int numMeat2 = 0;

	public GameObject meatChunk1;
	public GameObject meatChunk2;

	public float xVelocityRangeChunk = 30.0f;
	public float yVelocityRangeChunk = 30.0f;

	private float xVelocityChunk = 0.0f;
	private float yVelocityChunk = 0.0f;

	private Vector3 deathPos;

	public float maxHealth = 100.0f;
	public float curHealth = 100.0f;
	// Use this for initialization

	void Start ()
	{
		numMeat1 = Random.Range (minMeat, maxMeat);
		numMeat2 = Random.Range (minMeat, maxMeat - numMeat1);
	}

	void FixedUpdate()
	{
		if(curHealth <= 0.0f)
		{
			Die ();
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.K))
		{
			Die();
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{

	}

	public void Die()
	{
		deathPos.x = rigidbody2D.transform.position.x;
		deathPos.y = rigidbody2D.transform.position.y;

		for(int i = 0; i < numMeat1; ++i)
		{
			GameObject chunk = (GameObject)Instantiate (meatChunk1, deathPos, new Quaternion(0,0,0,0));
			chunk.rigidbody2D.velocity = InitVelocity();
			chunk.rigidbody2D.AddTorque(Random.Range(-50, 50));
		}
		for(int i = 0; i < numMeat2; ++i)
		{
			GameObject chunk = (GameObject)Instantiate (meatChunk2, deathPos, new Quaternion(0,0,0,0));
			chunk.rigidbody2D.velocity = InitVelocity();
			chunk.rigidbody2D.AddTorque(Random.Range(-50, 50));
		}
		Destroy(this.gameObject);
	}
	private Vector2 InitVelocity()
	{
		Vector2 retVal;
		xVelocityChunk = Random.Range ( (-xVelocityRangeChunk - xVelocityRangeChunk/2), xVelocityRangeChunk + xVelocityRangeChunk/2);
		yVelocityChunk = Random.Range ( yVelocityRangeChunk/2, yVelocityRangeChunk);
		retVal.x = xVelocityChunk;
		retVal.y = yVelocityChunk;
		return retVal;
	}

	public void Damage(float damageDealt)
	{
		curHealth -= damageDealt;
		Debug.Log(curHealth);
	}

	public void Knockback(float knockbackAmount)
	{
		rigidbody2D.velocity = new Vector2(knockbackAmount, 0);
	}
	public void Knockback(float knockbackAmount, float verticalKnockback)
	{
		rigidbody2D.velocity = new Vector2(knockbackAmount, verticalKnockback);
	}
}

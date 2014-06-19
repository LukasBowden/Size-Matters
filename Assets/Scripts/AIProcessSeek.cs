using UnityEngine;
using System.Collections;

public class AIProcessSeek : MonoBehaviour {

	private GameObject player;
	private Vector2 playerPos;
	private Vector2 velocity;

	public float speed = 10.0f;
	public float jumpforce = 0.0f;


	public float minTimeToMove = 1.0f;
	public float maxTimeToMove = 5.0f;

	private float curTimeToMove = 0.0f;
	private float curTimeMoved = 0.0f;
	private bool moving = false;


	public float minTimeToWait = 1.0f;
	public float maxTimeToWait = 5.0f;

	private float curTimeToWait = 0.0f;
	private float curTimeWaited = 0.0f;
	private bool waiting = false;

	/*	
	 *	Runs around randomly
	 *	stopping every few seconds or less
	 *	randomize direction weighted towards the player
	 *	
	 */

	// Use this for initialization
	void Start () 
	{
		Wait();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerPos = player.transform.position;
	}

	// FixedUpdate is called every at set intervals
	void FixedUpdate () 
	{
		velocity.y = this.rigidbody2D.velocity.y;
		if(!this.GetComponent<Enemy>().stunned && moving)
		{
			playerPos = player.transform.position;
			if (playerPos.x < this.transform.position.x - 0.5f || playerPos.x > this.transform.position.x + 0.5f)
			{
				if(playerPos.x < this.transform.position.x && velocity.x > -speed)
				{
					velocity.x += -speed * 0.1f;
				}
				else if(playerPos.x < this.transform.position.x && velocity.x <= -speed)
				{
					velocity.x = -speed;
				}
				if(playerPos.x > this.transform.position.x && velocity.x < speed)
				{
					velocity.x += speed * 0.1f;
				}
				else if(playerPos.x > this.transform.position.x && velocity.x >= speed)
				{
					velocity.x = speed;
				}
			}
			else
			{
				velocity.x = 0;
			}
			if(playerPos.y > transform.position.y)
			{

			}
			this.rigidbody2D.velocity = velocity;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		curTimeWaited += Time.deltaTime;
		curTimeMoved += Time.deltaTime;

		if(waiting && curTimeWaited > curTimeToWait)
		{
			waiting = false;
			curTimeWaited = 0;
			Move();
		}

		if(moving && curTimeMoved > curTimeToMove)
		{
			moving = false;
			curTimeMoved = 0;
			Wait();
		}

	}

	void Move()
	{
		moving = true;
		curTimeToMove = Random.Range (minTimeToMove, maxTimeToMove);
		curTimeMoved = 0;
	}

	void Wait()
	{
		waiting = true;
		curTimeToWait = Random.Range (minTimeToWait, maxTimeToWait);
		curTimeWaited = 0;
	}
}

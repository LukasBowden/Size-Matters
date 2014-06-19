using UnityEngine;
using System.Collections;

public class AIProcessRelentlessSeek : MonoBehaviour {
	
	private GameObject player;
	private Vector2 playerPos;
	private Vector2 velocity;
	
	public float speed = 10.0f;
	public float jumpforce = 0.0f;
	
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerPos = player.transform.position;
	}
	
	// FixedUpdate is called every 0.02 seconds
	void FixedUpdate () 
	{
		velocity.y = this.rigidbody2D.velocity.y;
		if(!this.GetComponent<Enemy>().stunned)
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
		
	}
	
	void Move()
	{
		
	}
}

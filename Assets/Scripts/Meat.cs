using UnityEngine;
using System.Collections;

public class Meat : MonoBehaviour {

	public float xVelocityRange = 100.0f;
	public float yVelocityRange = 100.0f;

	public float minSize = 0.5f;
	public float maxSize = 2.0f;

	//public PlayerSystems playerSystems;

	private float xVelocity = 0.0f;
	private float yVelocity = 0.0f;

	private float scale = 0.0f;


	// Use this for initialization
	void Start () 
	{
		scale = Random.Range (minSize, maxSize);
		rigidbody2D.transform.localScale *= scale;
		InitVelocity ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown (KeyCode.R))
			InitVelocity ();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			col.GetComponent<PlayerSystems>().AddMeat(scale);
			//Add size/ health to player
			Destroy(this.gameObject);
		}
	}

	void InitVelocity()
	{
		xVelocity = Random.Range ( - xVelocityRange, xVelocityRange);
		yVelocity = Random.Range ( 0, yVelocityRange);
		
		rigidbody2D.velocity = new Vector2 (xVelocity, yVelocity);
	}
}

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int minMeat = 3;
	public int maxMeat = 10;
	private int numMeat = 0;

	public GameObject meatChunk;

	private Vector3 deathPos;

	// Use this for initialization
	void Start () {
		numMeat = Random.Range (minMeat, maxMeat);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.K))
		{
			Die();
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			Die();
		}
		if (col.gameObject.tag == "Spit") 
		{
			Die();
		}
	}

	public void Die()
	{
		deathPos.x = rigidbody2D.transform.position.x;
		deathPos.y = rigidbody2D.transform.position.y;

		for(int i = 0; i < numMeat; ++i)
			Instantiate (meatChunk, deathPos, new Quaternion(0,0,0,0));
		Destroy(this.gameObject);
	}
}

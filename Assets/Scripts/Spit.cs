using UnityEngine;
using System.Collections;

public class Spit : MonoBehaviour {

	private float timeToLive = 0.1f;
	private float timeAlive = 0.0f;
	private short facing = 0;

	// Use this for initialization
	void Start () {
		timeAlive = 0.0f;
		facing = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().GetFacing();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(timeAlive <= 0)
			transform.position += new Vector3(5.0f * facing, 0, 0);
		timeAlive += 0.02f;
		if(timeAlive >= timeToLive)
			Destroy(this.gameObject);
	}
}

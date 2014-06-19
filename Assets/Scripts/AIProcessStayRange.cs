using UnityEngine;
using System.Collections;

public class AIProcessStayRange : MonoBehaviour {

	public float maxRangeX = 0.0f;
	public float minRangeX = 0.0f;

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
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}

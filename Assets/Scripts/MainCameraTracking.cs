using UnityEngine;
using System.Collections;

public class MainCameraTracking : MonoBehaviour {

	public float yMargin = 1.0f;
	public float ySmooth = 1.0f;

	private Transform player;

	void Awake () 
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	bool CheckYMargin()
	{
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}
		
	void FixedUpdate ()
	{
		TrackPlayer();
	}

	void TrackPlayer ()
	{
		float targetX = transform.position.x;
		float targetY = transform.position.y;
	
		targetX = player.position.x;
		
	
		if(CheckYMargin())
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
	
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}

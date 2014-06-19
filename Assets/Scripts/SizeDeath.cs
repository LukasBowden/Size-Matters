using UnityEngine;
using System.Collections;

public class SizeDeath : MonoBehaviour {

	Vector3 parentScale;

	// Use this for initialization
	void Start () 
	{
		parentScale.x = transform.parent.localScale.x;
		parentScale.y = transform.parent.localScale.y;
		parentScale.z = transform.parent.localScale.z;
		transform.localScale = parentScale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}

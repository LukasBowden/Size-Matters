using UnityEngine;
using System.Collections;

public class PlayerSystems : MonoBehaviour {
	
	private float health = 178.0f;
	private float maxHealth = 178.0f;
	private float healthRegen = 0.1f;
	private float defence = 0.0f;
	private float damage = 10.0f;
	public float scale = 1.0f;

	private bool dead = false;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(!dead)
		{

			if(health < 0)
			{
				Die ();
			}
			if(health < maxHealth * scale)
			{
				health += healthRegen;
			}
			if(health > maxHealth * scale)
			{
				health = maxHealth * scale;
			}
			Debug.Log (maxHealth * scale);
		}

	}

	private void Die()
	{
		//Kill Player, for now set to 0, 0
		dead = true;
		rigidbody2D.transform.position = new Vector2(0, 0);
	}

	public void AddMeat(float meatScale)
	{
		scale += meatScale / scale * 0.005f;
	}
}

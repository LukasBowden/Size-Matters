using UnityEngine;
using System.Collections;


public class PlayerSystems : MonoBehaviour {
	
	public float health = 178.0f;
	public float maxHealth = 178.0f;
	public float healthRegen = 0.1f;
	public float defence = 0.0f;
	public float damage = 10.0f;
	public float knockback = 10.0f;
	public float scale = 1.0f;	

	private bool dead = false;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{

	}

	void Update()
	{
		if(!dead)
		{			
			if( health < 0)
			{
				Die ();
			}
			if( health <  maxHealth * scale)
			{
				health +=  healthRegen * Time.deltaTime;
			}
			if( health >  maxHealth * scale)
			{
				health =  maxHealth * scale;
			}
		}
		if(Input.GetKeyDown(KeyCode.KeypadPlus))
			 scale += 1;
		if(Input.GetKeyDown(KeyCode.KeypadMinus))
			 scale -= 1;
	}

	private void Die()
	{
		dead = true;
		Application.LoadLevel(Application.loadedLevel);
	}

	public void AddMeat(float meatScale)
	{
		 scale += meatScale /  scale * 0.005f;
	}
}

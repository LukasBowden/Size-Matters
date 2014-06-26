using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFactory : MonoBehaviour 
{
	enum EnemyType
	{
		ET_BEETLE,
		ET_LIZARD
	};

	public float timeToSpawn = 0.0f;
	private float curTimeToSpawn = 0.0f;

	public float maxSpawn = 30.0f;
	private float curSpawn = 0.0f;
	public float chargePerSecond = 10.0f;

	private List<int> enemyList = new List<int>();

	private Vector3 spawnPoint;

	public GameObject beetle;
	public float beetleCost = 3;
	public float beetleWeight = 5;
	public GameObject lizard;
	public float lizardWeight = 3;
	public float lizerdCost = 5;

	void Start()
	{
		spawnPoint.z = 0;
		curSpawn = 30.0f;
		PopSpawnLoop ();
	}
	
	void Update()
	{
		if(curSpawn < maxSpawn)
			curSpawn += chargePerSecond * Time.deltaTime;


		curTimeToSpawn += Time.deltaTime;

		if(curTimeToSpawn > timeToSpawn)
		{
			Spawn();
			curTimeToSpawn = 0;
			PopSpawnLoop();
		}	
	}

	void PopSpawnLoop()
	{
		float curPopValue = Random.Range (curSpawn / 3, curSpawn);
		while (curPopValue > 0)
		{
			EnemyType enemyType = EnemyType.ET_BEETLE;
			float val = Random.Range(0, beetleWeight + lizardWeight);
			if(val <= beetleWeight)
				enemyType = EnemyType.ET_BEETLE;
			else if(val > beetleWeight && val <= beetleWeight + lizardWeight)
				enemyType = EnemyType.ET_LIZARD;

			switch (enemyType)
			{
			case EnemyType.ET_BEETLE:
				Debug.Log("Beetle");
				enemyList.Add(1);
				curPopValue -= beetleCost;
				break;

			case EnemyType.ET_LIZARD:
				Debug.Log("Lizard");
				enemyList.Add(2);
				curPopValue -= lizerdCost;
				break;
			}
		}
	}

	void Spawn()
	{
		for (int i = 0; i < enemyList.Count; ++i)
		{
			spawnPoint.x = Random.Range(-15 + transform.position.x, 15 + transform.position.x);
			spawnPoint.y = Random.Range(-5 + transform.position.y, 5 + transform.position.y);
			if(enemyList[i] == 1)
				Instantiate(beetle, spawnPoint, new Quaternion(0,0,0,0));
			if(enemyList[i] == 2)
				Instantiate(lizard, spawnPoint, new Quaternion(0,0,0,0));
		}
	}
}
















using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class script_spawn_rain : MonoBehaviour {

	public Vector3 spawnLocation;
	public GameObject raindrop;
	public float spawnRate;

	public int pooledRain;
	public List <GameObject> raindrops;

	// Use this for initialization
	void Start ()
	{
		pooledRain=120;
		for(int i=0; i<pooledRain; i++){
			GameObject obj = (GameObject)Instantiate(raindrop);
			obj.SetActive(false);
			raindrops.Add(obj);
		}

		spawnRate=0.02f;
		InvokeRepeating("Spawn", 0, spawnRate);
	}

	void Spawn ()
	{
		for(int i=0; i<pooledRain; i++){
			if(!raindrops[i].activeInHierarchy)
			{
				raindrops[i].SetActive(true);
				break;
			}
		}
	}
}

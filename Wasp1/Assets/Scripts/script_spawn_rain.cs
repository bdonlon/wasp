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
		if(ApplicationModel.touchScreen){
			pooledRain=50;
			spawnRate=0.02f;
		}else{
			pooledRain=500;
			spawnRate=0.005f;
		}
		for(int i=0; i<pooledRain; i++){
			GameObject obj = (GameObject)Instantiate(raindrop);
			obj.SetActive(false);
			raindrops.Add(obj);
		}

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

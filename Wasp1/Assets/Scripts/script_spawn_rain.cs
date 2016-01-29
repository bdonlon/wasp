using UnityEngine;
using System.Collections;

public class script_spawn_rain : MonoBehaviour {

	public Vector3 spawnLocation;
	public GameObject raindrop;
	public float jitterX,jitterY;
	public float spawnRate;

	// Use this for initialization
	void Start ()
	{
		spawnRate=0.005f;
		if(ApplicationModel.touchScreen){
			spawnRate=0.02f;
		}
		jitterX=10f;
		jitterY=8f;
		spawnLocation = new Vector3(0,0,0);
		InvokeRepeating("Spawn", 0, spawnRate);
	}

	void Spawn ()
	{
		spawnLocation.x = transform.position.x + Random.Range(-jitterX,jitterX);
		spawnLocation.y = transform.position.y + Random.Range(-jitterY,jitterY);
		Instantiate(raindrop, spawnLocation, transform.rotation);
	}
}

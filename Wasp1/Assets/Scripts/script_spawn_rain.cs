using UnityEngine;
using System.Collections;

public class script_spawn_rain : MonoBehaviour {

	public Vector3 spawnLocation;
	public GameObject raindrop;
	public float jitter;

	// Use this for initialization
	void Start ()
	{
		jitter=6f;
		spawnLocation = new Vector3(0,0,0);
		InvokeRepeating("Spawn", 0, 0.2f);
	}

	void Spawn ()
	{
		spawnLocation.x = transform.position.x + Random.Range(-jitter,jitter);
		spawnLocation.y = transform.position.y + Random.Range(0,1);
		Instantiate(raindrop, spawnLocation, transform.rotation);
	}
}

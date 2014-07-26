using UnityEngine;
using System.Collections;

public class spawnWasps : MonoBehaviour {

	public float spawnTime;		// The amount of time between each spawn.
	public float spawnDelay;		// The amount of time before spawning starts.
	public GameObject wasp;
	public Vector3 spawnLocation;
	public float jitter;
	public bool left,right,top,bottom = false;

	public GameObject player;
	public GameObject _GM;

	// Use this for initialization
	void Start () {
		jitter=6f;
		spawnDelay = 1;
		spawnTime = 2;

		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", Random.Range(spawnDelay-0.1f,spawnDelay+0.1f), spawnTime);
	}
	
	void Spawn ()
	{
		if((!player.GetComponent<playerMovement>().isDead()) && (_GM.GetComponent<Setup>().numWasps < _GM.GetComponent<Setup>().maxWasps) && (_GM.GetComponent<Setup>().spawnPhase==true)){
			if(top){
				spawnLocation.x = transform.position.x + Random.Range(-jitter,jitter);
				spawnLocation.y = transform.position.y + Random.Range(0,1);
			}else if(bottom){
				spawnLocation.x = transform.position.x + Random.Range(-jitter,jitter);
				spawnLocation.y = transform.position.y + Random.Range(-1,0);
			}else if (right){
				spawnLocation.x = transform.position.x + Random.Range(0,1);
				spawnLocation.y = transform.position.y + Random.Range(-jitter,jitter);
			}else if (left){
				spawnLocation.x = transform.position.x + Random.Range(-1,0);
				spawnLocation.y = transform.position.y + Random.Range(-jitter,jitter);
			}
			//spawnLocation=transform.position;	//Spawn directly on spawner
			Instantiate(wasp, spawnLocation, transform.rotation);
			_GM.GetComponent<Setup>().numWasps++;
			_GM.GetComponent<Setup>().waspsSpawnedThisWave++;
		}else{
		//player dead, stop spawning
		}
	}
}

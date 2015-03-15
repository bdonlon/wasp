using UnityEngine;
using System.Collections;

public class script_GUI_wave_text : MonoBehaviour {

	public Sprite[] Numbers;
	public GameObject 	wave, X, of, Y,
						finalWaveFINAL, finalWaveWAVE;
	SpriteRenderer rendWave,rendX,rendOf,rendY,rendFWF,rendFWW;
	int currWave,maxWaves;
	Color colour;

	// Use this for initialization
	void Start () {
		rendWave=wave.GetComponent<SpriteRenderer>();
		rendX=X.GetComponent<SpriteRenderer>();
		rendOf=of.GetComponent<SpriteRenderer>();
		rendY=Y.GetComponent<SpriteRenderer>();
		rendFWF=finalWaveFINAL.GetComponent<SpriteRenderer>();
		rendFWW=finalWaveWAVE.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setWaveInfo(int curr, int max)
	{
		curr++; //Value starts at 0
		if(curr<max){
			wave.active=true;
			X.active=true;
			of.active=true;
			Y.active=true;
			setX(curr);
			setY(max);
		}else if(curr==max){
			finalWaveFINAL.active=true;
			finalWaveWAVE.active=true;
		}

		StartCoroutine(drawWave());
	}

	void deactiveateAll(){
		wave.active=false;
		X.active=false;
		of.active=false;
		Y.active=false;
		finalWaveFINAL.active=false;
		finalWaveWAVE.active=false;
	}

	void setX(int x){
		rendX.sprite=Numbers[x];
	}
	
	void setY(int y){
		rendY.sprite=Numbers[y];
	}

	IEnumerator drawWave()
	{
		for (float i = 0; i < 90; i++) {	//Fade in
			colour = new Color(1, 1, 1, i/255*3);
			rendWave.color = colour;
			rendX.color = colour;
			rendOf.color = colour;
			rendY.color = colour;
			rendFWF.color = colour;
			rendFWW.color = colour;
			yield return new WaitForSeconds(0.0f);
		}

		yield return new WaitForSeconds(0.5f);

		for (float i = 90; i >= 0; i--) {	//Fade out
			colour = new Color(1, 1, 1, i/255*3);
			rendWave.color = colour;
			rendX.color = colour;
			rendOf.color = colour;
			rendY.color = colour;
			rendFWF.color = colour;
			rendFWW.color = colour;
			yield return new WaitForSeconds(0.0f);
		}

		deactiveateAll();
	}
}

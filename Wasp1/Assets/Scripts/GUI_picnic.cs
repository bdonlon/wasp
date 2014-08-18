using UnityEngine;
using System.Collections;

public class GUI_picnic : MonoBehaviour {
	
	public GameObject picnic;
	public string healthString;
	public float healthInt;
	
	// Update is called once per frame
	void Update () {
		healthInt = picnic.GetComponent<picnic_health_script>().getPercentage();
		healthString = "Picnic Integrity: "+healthInt.ToString()+"%";
		guiText.text = healthString;
	}
}

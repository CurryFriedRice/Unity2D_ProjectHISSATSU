using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Player ThePlayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CameraFuck ();
	}

	void CameraFuck(){
		float Tits = ThePlayer.GetPositionY();
		transform.position = new Vector3(0,Tits+3,-10);
//		print ("I'm Trying to follow?");
	}

}

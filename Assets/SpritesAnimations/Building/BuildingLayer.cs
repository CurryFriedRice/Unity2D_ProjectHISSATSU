using UnityEngine;
using System.Collections;

public class BuildingLayer : BuildingScript {


	public BuildingPart LeftSide;
	public BuildingPart Center;
	public BuildingPart RightSide;
	public bool Broken = false;
	// Use this for initialization
	void Start () {
		StartCoroutine (CheckBuildingHP ());
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPlayer(Player NewUnit){
		PlayerUnit = NewUnit;
		LeftSide.SetPlayer (NewUnit);
		Center.SetPlayer (NewUnit);
		RightSide.SetPlayer(NewUnit);
	}

	IEnumerator CheckBuildingHP(){
		while (Broken == false) {
			if (LeftSide.GetBuildingHP () <= 0 || 
				Center.GetBuildingHP () <= 0 || 
				RightSide.GetBuildingHP () <= 0) {//if ANY of the building parts break the layer will break
				print ("This should be destroyed");
				Broken = true;
				Destroy(this.gameObject);
			}
			yield return new WaitForSeconds (0.01f);
		}
	}
}

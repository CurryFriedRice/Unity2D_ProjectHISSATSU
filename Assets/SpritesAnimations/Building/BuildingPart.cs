using UnityEngine;
using System.Collections;

public class BuildingPart : BuildingLayer {
	
	int BuildingHP = 10;

	// Use this for initialization
	void Start () {
		StartCoroutine (TakeDamage ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPlayer(Player NewUnit){
		PlayerUnit = NewUnit;
	}
	//DIfferent Buildings have Different HP values
	//If the building's HP hits 0 it breaks the layer

	IEnumerator TakeDamage(){
		while (BuildingHP >= 0) {
			if (transform.position.y <= PlayerUnit.GetPositionY () + 5 && PlayerUnit.isSlashing () == true) {
				BuildingHP += -1;
			}
			yield return new WaitForSeconds(0.01F);
		}
		PlayerUnit.NotTouching ();

	}

	public int GetBuildingHP(){
		return BuildingHP;
	}

}

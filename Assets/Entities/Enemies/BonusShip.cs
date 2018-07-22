using UnityEngine;
using System.Collections;

public class BonusShip : MonoBehaviour {
	public GameObject shipPrefab;

	private int waitTime;
	private float initialTime;
	private string state;

	// Use this for initialization
	void Start () {
		waitTime = (int)(Random.Range (6, 15));
	}
	
	// Update is called once per frame
	void Update () {
		string previousState = state;

		if ((int)(Time.realtimeSinceStartup % 10) == waitTime) {
			state = "Bonus";
		} else {
			state = "Not Bonus";
		}

		if (state != previousState) {
			if(state.Equals("Bonus")) {
				CallBonusShip();
			}
		}
	}

	void CallBonusShip() {
		GameObject ship = Instantiate (shipPrefab, this.transform.position, Quaternion.identity) as GameObject;
		ship.rigidbody2D.velocity = new Vector3 (8f, 0f, 0f);
		ship.transform.parent = this.transform;
	}
}

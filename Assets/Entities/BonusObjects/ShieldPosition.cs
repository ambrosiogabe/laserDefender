using UnityEngine;
using System.Collections;

public class ShieldPosition : MonoBehaviour {
	public GameObject shieldPrefab;
	public GameObject[] prefab;
	
	private int waitTime;
	private float initialTime;
	private string state;

	private int waitTime2;
	private float initialTime2;
	private string state2;
	
	// Use this for initialization
	void Start () {
		waitTime = (int)(Random.Range (20, 30));
		waitTime2 = (int)(Random.Range (0, 2));
	}
	
	// Update is called once per frame
	void Update () {
		string previousState = state;
		string previousState2 = state2;
		
		if ((int)(Time.realtimeSinceStartup % 30) == waitTime) {
			state = "Bonus";
		} else {
			state = "Not Bonus";
		}
		
		if (state != previousState) {
			if(state.Equals("Bonus")) {
				CallShield();
			}
		}

		if ((int)(Time.realtimeSinceStartup % 2) == waitTime2) {
			state2 = "Bonus";
		} else {
			state2 = "Not Bonus";
		}
		
		if (state2 != previousState2) {
			if(state2.Equals("Bonus")) {
				CallAsteroid();
			}
		}
	}
	
	void CallShield() {
		GameObject shield = Instantiate (shieldPrefab, this.transform.position + new Vector3((float)Random.Range(0, 20), 0f, 0f), Quaternion.identity) as GameObject;
		shield.rigidbody2D.velocity = new Vector3 (0f, -5f, 0f);
		shield.transform.parent = this.transform;
	}

	void CallAsteroid() {
		int num = Random.Range (0, 3);
		Debug.Log ("ASTEROID");
		GameObject asteroid = Instantiate (prefab[num], this.transform.position + new Vector3((float)Random.Range(0, 20), 0f, 0f), Quaternion.identity) as GameObject;
		asteroid.rigidbody2D.velocity = new Vector3 (0f, -5f, 0f);
		asteroid.transform.parent = this.transform;
	}
}

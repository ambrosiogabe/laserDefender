using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	public GameObject shieldPrefab;

	void OnTriggerEnter2D(Collider2D col) {
		PlayerController player = col.gameObject.GetComponent<PlayerController> ();
		if (player && !player.shield) {
			player.shield = true;
			player.shieldObject = Instantiate(shieldPrefab, this.transform.position, Quaternion.identity) as GameObject;
			Destroy(gameObject);
		} else if(player && player.shield) {
			Scorekeeper scoreKeeper = GameObject.Find ("Score").GetComponent<Scorekeeper> ();
			scoreKeeper.Score(50);
			Destroy (gameObject);
		}
	}
}

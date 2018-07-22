using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	public LevelManager levelManager;

	void OnTriggerEnter2D(Collider2D col) {
		PlayerController player = col.gameObject.GetComponent<PlayerController> ();
		if (player && !player.shield) {
			player.health -= 50f;
			if (player.health <= 0) {
				Destroy (gameObject);
				levelManager.LoadLevel ("Win Screen");
			}
		} else if (player && player.shield) {
			Destroy (gameObject);
		}
	}
}

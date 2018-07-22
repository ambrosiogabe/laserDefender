using UnityEngine;
using System.Collections;

public class BonusShipScript : MonoBehaviour {
	private float health = 125f;
	private AudioSource shipExplodeSource;
	private AudioClip shipExplode;
	private Scorekeeper scoreKeeper;
	private int scoreValue;

	// Use this for initialization
	void Start () {
		scoreValue = (int)(health * 5);
		scoreKeeper = GameObject.Find ("Score").GetComponent<Scorekeeper> ();
		shipExplodeSource = this.audio;
		shipExplode = shipExplodeSource.clip;
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		Projectile missile = col.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit ();
			if (health <= 0) {
				AudioSource.PlayClipAtPoint(shipExplode, transform.position);
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue);
			}
		}
	}
}

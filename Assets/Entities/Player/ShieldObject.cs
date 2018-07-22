using UnityEngine;
using System.Collections;

public class ShieldObject : MonoBehaviour {
	int numOfHits;
	public Sprite[] sprites;
	SpriteRenderer curSprite;


	void Start() {
		curSprite = GetComponent<SpriteRenderer> ();
		numOfHits = 0;
	}

	void OnTriggerEnter2D(Collider2D col) {
		PlayerController player = GameObject.Find ("Player").GetComponent<PlayerController> ();
		Projectile missile = col.gameObject.GetComponent<Projectile> ();
		if (missile) {
			numOfHits++;
			if(numOfHits != 3) {
				curSprite.sprite = sprites[numOfHits];
				Destroy (missile.gameObject);
			} else if(numOfHits >= 3) {
				player.shield = false;
				Destroy (gameObject);
			}
		}
	}
}

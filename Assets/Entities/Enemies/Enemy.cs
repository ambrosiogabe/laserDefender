using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public GameObject laser;
	public float shotsPerSecond = 0.5f;
	public GameObject healthBarObject = null;
	public float health;

	private int scoreValue;
	private float totalHealth;
	private float missileSpeed = -5f;
	private Scorekeeper scoreKeeper;
	private AudioSource[] sounds;
	private AudioSource shootLaser;
	private AudioClip shipExplode;
	GameObject healthBar;

	void Start() {
		sounds = GetComponents<AudioSource>();
		totalHealth = health;
		scoreValue = (int)(totalHealth * 1.25);
		Vector3 startVector = new Vector3 (0f, 0.6f, 0f);

		healthBar = Instantiate (healthBarObject, this.transform.position + startVector, Quaternion.identity) as GameObject;
		healthBar.transform.parent = this.transform;
		healthBar.transform.Rotate (new Vector3 (0f, 0f, 90f));

		scoreKeeper = GameObject.Find("Score").GetComponent<Scorekeeper>();
		shootLaser = sounds [0];
		shipExplode = sounds [1].clip;
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

	void Update() {
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability) {
			Fire ();
		}

		healthBar.transform.position = this.transform.position + new Vector3 (0f, 0.6f, 0f);
		healthBar.transform.localScale = new Vector3(1f, (health / totalHealth) * 2, 1f);
	}

	void Fire() {
		Vector3 startPosition = this.transform.position + new Vector3 (0, -1, 0);
		GameObject missile = Instantiate (laser, startPosition, Quaternion.identity) as GameObject;
		missile.rigidbody2D.velocity = new Vector3(0, missileSpeed, 0);
		shootLaser.Play();
	}
}

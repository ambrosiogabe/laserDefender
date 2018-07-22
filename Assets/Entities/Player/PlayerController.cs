using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject laser;
	public float speed = 10;
	public float padding = 0.8f;
	public GameObject healthBarObject;
	public bool shield = false;

	public float laserSpeed;
	public float firingRate = 0.2f;

	GameObject healthBar;
	float xMin;
	float xMax;
	public float health = 250f;
	float totalHealth;
	AudioSource shootLaserSound;
	public LevelManager levelManager;
	public GameObject shieldObject;

	// Use this for initialization
	void Start () {
		totalHealth = health;
		
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		
		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
		
		healthBar = Instantiate (healthBarObject, this.transform.position - new Vector3 (0f, 0.6f, 1f), Quaternion.identity) as GameObject;
		healthBar.transform.parent = this.transform;
		healthBar.transform.Rotate (new Vector3 (0f, 0f, 90f));
		shootLaserSound = this.audio;
	}

	void OnTriggerEnter2D(Collider2D col) {
		Projectile enemyMissile = col.gameObject.GetComponent<Projectile> ();
		if (enemyMissile && !shield) {
			health -= enemyMissile.GetDamage();
			enemyMissile.Hit ();
			if (health <= 0) {
				Destroy(gameObject);
				levelManager.LoadLevel("Win Screen");
			}
		}
	}

	void Fire() {
		Vector3 startPosition = this.transform.position + new Vector3 (0, 1, 0);
		GameObject laserObject = Instantiate(laser, startPosition, Quaternion.identity) as GameObject;
		laserObject.rigidbody2D.velocity = new Vector3(0, laserSpeed, 0);
		shootLaserSound.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.00001f, firingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke("Fire");
		}	

		float newX = Mathf.Clamp (transform.position.x, xMin, xMax);
		transform.position = new Vector3 (newX, transform.position.y, 0);

		healthBar.transform.position = this.transform.position - new Vector3 (0f, 0.6f, 0f);
		healthBar.transform.localScale = new Vector3(1f, (health / totalHealth) * 2, 1f);

		if(shieldObject)
			shieldObject.transform.position = this.transform.position;
	}
}

using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public GameObject enemyPrefab2;
	public GameObject enemyPrefab3;
	public GameObject enemyPrefab4;
	public GameObject enemyPrefab5;

	public float width = 10f;
	public float height = 5f;
	public float spawnDelay = 0.5f;

	private bool movingRight = false;
	private float speed = 5;
	private float xMax;
	private float xMin;
	private Vector3 initialPosition;
	private float incrementDown = 0f;
	private Scorekeeper scoreKeeper;
	private int maxPositions = 9;
	private int numOfEnemies = 0;

	// Use this for initialization
	void Start () {
		scoreKeeper = GameObject.Find("Score").GetComponent<Scorekeeper>();

		float distance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1f, 0f, distance));
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0f, distance));
		xMin = leftMost.x;
		xMax = rightMost.x;

		SpawnUntilFull ();
		initialPosition = this.transform.position;
	}

	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition ();

		if ( (freePosition && numOfEnemies < maxPositions) ) {
			GameObject enemy = null;
			if(scoreKeeper.GetScore () >= 26964) {enemy = Instantiate (enemyPrefab5, freePosition.position, Quaternion.identity) as GameObject; maxPositions=15;}
			else if(scoreKeeper.GetScore () >= 16848) {enemy = Instantiate (enemyPrefab4, freePosition.position, Quaternion.identity) as GameObject; maxPositions=12;}
			else if(scoreKeeper.GetScore () >= 8982) {enemy = Instantiate (enemyPrefab3, freePosition.position, Quaternion.identity) as GameObject; maxPositions=11;}
			else if(scoreKeeper.GetScore() >= 3366) {enemy = Instantiate (enemyPrefab2, freePosition.position, Quaternion.identity) as GameObject; maxPositions=10;}
			else {enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;}
			enemy.transform.parent = freePosition;

			if(scoreKeeper.GetScore() < 45000)
				numOfEnemies++;
		} 
		if (NextFreePosition () && numOfEnemies < maxPositions) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}
	
	public void OnDrawGizmos() {
		new Vector3 (width, height);
		Gizmos.DrawWireCube(this.transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update () {
		if (movingRight) {
			this.transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			this.transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float rightEdgeFormation = this.transform.position.x + (0.5f * width);
		float leftEdgeFormation = this.transform.position.x - (0.5f * width);
		if (rightEdgeFormation > xMax) {
			movingRight = false;
			this.transform.position -= new Vector3(0f, incrementDown, 0f);
		}

		if (leftEdgeFormation < xMin) {
			movingRight = true;
			this.transform.position -= new Vector3(0f, incrementDown, 0f);
		}

		if (AllMembersDead ()) {
			this.transform.position = initialPosition;
			SpawnUntilFull ();
		} 

		if (scoreKeeper.GetScore () >= 45000) {
			SpawnUntilFull();
		}
	}

	bool AllMembersDead() {
		foreach (Transform child in transform) {
			if(child.childCount > 0) {
				return false;
			}
		}

		numOfEnemies = 0;
		return true;
	}

	Transform NextFreePosition() {
		foreach (Transform child in transform) {
			if(child.childCount < 1) {
				return child;
			}
		}

		return null;
	}
}

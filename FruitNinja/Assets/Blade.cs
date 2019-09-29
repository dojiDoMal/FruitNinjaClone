using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour {

	bool isCutting = false;
	public float minCuttingVelocity = .001f;
	Rigidbody2D rb;
	public GameObject bladeTrailPrefab;
	GameObject currentBladeTrail;
	Camera cam;
	CircleCollider2D circleCollider;
	Vector2 previousPos;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		cam = Camera.main;
		circleCollider = GetComponent<CircleCollider2D>();
	}

	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			StartCutting();
		}
		else if (Input.GetMouseButtonUp(0)) {
			StopCutting();
		}

		if (isCutting) {
			UpdateCut();
		}

	}

	void UpdateCut() {
		Vector2 newPos = cam.ScreenToWorldPoint(Input.mousePosition);
		rb.position = newPos;
		float velocity = (newPos - previousPos).magnitude/Time.deltaTime;
		if (velocity > minCuttingVelocity) {
			circleCollider.enabled = true;
		}
		else { circleCollider.enabled = false; }
		previousPos = newPos;
	}

	void StartCutting() {
		isCutting = true;
		currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
		previousPos = cam.ScreenToWorldPoint(Input.mousePosition);
		circleCollider.enabled = false;
	}

	void StopCutting() {
		isCutting = false;
		currentBladeTrail.transform.SetParent(null);
		Destroy(currentBladeTrail,2f);
		circleCollider.enabled = false;
	}
}

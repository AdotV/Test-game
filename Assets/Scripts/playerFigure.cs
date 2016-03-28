using UnityEngine;
using System.Collections;

public class playerFigure : MonoBehaviour
{
	public GameObject hand;
	private bool isDrawing;
	private bool isMouseMoving;
	private Vector3 startVertex;
	private Vector3 startVertex_WS;
 	private Vector3 currentVertex;
	private Vector3 currentMousePosition;
	private Vector2[] vertexDir;
	private Vector2[] vertexList;
	private Vector2 priviousMouseAxis;
	private int frameCount;
	private int vertexCount;

	void Start()
	{
		isDrawing = false;
		frameCount = 0;
	}
	private void startDrawing() 
	{
		isDrawing = true;
		isMouseMoving = true;
		startVertex_WS = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		startVertex_WS.z = 0;
		vertexCount = 0;
		vertexDir = GameObject.Find ("ExampleFigure").GetComponent<figureGenerator> ().vertexDirection;
		priviousMouseAxis = new Vector2 (0, 0);
	}
	private void checkAngle() {
		Vector3 currentMousePosition_WS = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 mouseDirection = new Vector2(currentMousePosition_WS.x - startVertex_WS.x, currentMousePosition_WS.y - startVertex_WS.y);
		mouseDirection = new Vector2 (Mathf.Abs(mouseDirection.x), Mathf.Abs(mouseDirection.y));
		float mouseRange = Mathf.Sqrt (Mathf.Pow (mouseDirection.x, 2) + Mathf.Pow (mouseDirection.y, 2));
		Vector2 newMouseDir = new Vector2 (mouseDirection.x / mouseRange, mouseDirection.y / mouseRange);
		if (vertexCount < GameObject.Find ("ExampleFigure").GetComponent<figureGenerator> ().vectorsAngle.Length) {
			float directionX = newMouseDir.x - vertexDir [vertexCount].x;
			float directionY = newMouseDir.y - vertexDir [vertexCount].y;
			if (directionX <= 0.5 && directionX >= -0.5) {
				if (directionY <= 0.5 && directionY >= -0.5) {
					

					vertexCount++;
					startVertex_WS = currentMousePosition_WS;
					startVertex_WS.z = 0;
					if (vertexCount == GameObject.Find ("ExampleFigure").GetComponent<figureGenerator> ().vectorsAngle.Length) {
						GameObject.Find ("ExampleFigure").GetComponent<gameManager> ().nextFigure ();
						isDrawing = false;
						vertexCount = 0;
					}
				} else {
					isDrawing = false;
					vertexCount = 0;
				}
				} else {
					isDrawing = false;
					vertexCount = 0;
				}
			} else {
				isDrawing = false;
				vertexCount = 0;
			}
	}
	public void moveHand() {
		Vector3 handPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		handPosition.z = -1;
		hand.GetComponent<Transform>().position = handPosition;
	}
	void Update()
	{
		if (GameObject.Find ("ExampleFigure").GetComponent<gameManager> ().isPlay) {
			if (Input.GetMouseButtonDown (0)) {
				if (!isDrawing) {
					startDrawing ();
				}
			} else if (Input.GetMouseButtonUp (0)) {
				isDrawing = false;
			}
		} else {
			isDrawing = false;
		}
	}
	void FixedUpdate() 
	{
		if (isDrawing) {
			moveHand ();
			if ((Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") ==0)) {
				if (frameCount < 4) {
					if (isMouseMoving) {
						isMouseMoving = false;
						currentMousePosition = Input.mousePosition;
					}
					frameCount++;
				} else {
					frameCount = 0;
					if (!isMouseMoving) {
						isMouseMoving = true;
						if ((currentMousePosition.x-Input.mousePosition.x < 5 || currentMousePosition.x-Input.mousePosition.x > -5) &&
							(currentMousePosition.y-Input.mousePosition.y < 5 || currentMousePosition.y-Input.mousePosition.y > -5)) {
								priviousMouseAxis = new Vector2 (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"));
							checkAngle ();
						}
					}
					
				}

			}
		}
	}
}


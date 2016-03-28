using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class templateController : MonoBehaviour
{
	public InputField countField;
	public GameObject savePanel;
	public Button saveButton;


	public GameObject hand;
	public GameObject example;
	public List<figure> figureList;
	public List<Vector3> vertexList;
	private Vector3 currentMousePosition;
	private Vector2 priviousMouseAxis;
	public int figureCount;
	public int vertexN;
	public bool drawingMod;
	public bool isDrawing;
	private int frameCount;
	private bool isMouseMoving;
	public int currentFigure;

	void Start()
	{
		drawingMod = false;
		isDrawing = false;
		figureList = new List<figure> ();
	}
	public void createFigureArray(Vector3 newVertex)
	{
		vertexList.Add (newVertex);
	}
	public void switchDrawingMod()
	{
		drawingMod = true;
	}
	public void addNewFigure()
	{
		figureList.Add (new figure (vertexList.Count));
		for (int i = 0; i < vertexList.Count; i++) {
			figureList [figureCount].addNewVertex (vertexList [i], i);
		}
		drawExample ();
	}
	private void drawExample()
	{
		example.GetComponent<LineRenderer> ().SetVertexCount (vertexList.Count);
		example.GetComponent<LineRenderer>().SetPositions(figureList [figureCount].getVertexPosition ());
		StartCoroutine (waitAnwShowMessage ());
	}
	public void startDrawingTemplate()
	{
		vertexList = new List<Vector3>();
		vertexList.Add(Camera.main.ScreenToWorldPoint (Input.mousePosition));
		isMouseMoving = true;
		frameCount = 0;
	}
	public void moveHand() {
		Vector3 handPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		handPosition.z = -1;
		hand.GetComponent<Transform>().position = handPosition;
	}
	public void saveFigure(bool save)
	{
		if (save) {
			figureCount++;
		} else {
			figureList.RemoveAt (figureCount);
		}
	}
	public void setFigureLevel()
	{
		if (countField.text.Length != 0) {
			saveButton.interactable = true;
			figureList [figureCount].setLevel (int.Parse (countField.text));
		}
	}
	void Update()
	{
		if (drawingMod) {
			if (Input.GetMouseButtonDown (0)) {
				if (!isDrawing) {
					isDrawing = true;
					startDrawingTemplate ();
				}
			} else if (Input.GetMouseButtonUp (0)) {
				if (isDrawing) {
					drawingMod = false;
					isDrawing = false;
					addNewFigure ();
				}
			}
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
							vertexList.Add(Camera.main.ScreenToWorldPoint (Input.mousePosition));
						}
					}

				}

			}
		}
	}

	IEnumerator waitAnwShowMessage()
	{
		yield return new WaitForSeconds (2.5f);
		savePanel.GetComponent<CanvasGroup> ().alpha = 1;
		savePanel.GetComponent<CanvasGroup> ().interactable = true;
	}
}


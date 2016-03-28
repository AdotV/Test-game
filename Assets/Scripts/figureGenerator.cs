using UnityEngine;
using System.Collections;

public class figureGenerator : MonoBehaviour
{	
	private Vector2 center;
	private float radius;
	private Vector3[] vertexPosition;
	public Vector2[] vertexDirection;
	public float[] vectorsAngle;
	public Material _material;
	private float Angle; 

	void Start()
	{
		center = new Vector2 (0, 0); 
		radius = 3f;					
		Angle = 360;
		Angle = Angle * Mathf.Deg2Rad;
	}
	public void setVertexPositions(int vertexNumb)  
	{
		vertexPosition = new Vector3[vertexNumb+1];
		for (int i = 0; i < vertexPosition.Length; i++) {
			if (i != vertexPosition.Length - 1) {
				float x = center.x + Mathf.Sin (Angle / vertexNumb * i) * radius;
				float y = center.y + Mathf.Cos (Angle / vertexNumb * i) * radius;
				vertexPosition [i] = new Vector3 (x, y, 0);
			} else {
				vertexPosition [i] = vertexPosition [0];
			}
		}
		setVectorAngle ();
		drawExample ();
	}
	public void manualCreating(int _fN, GameObject _template)
	{
		vertexPosition = _template.GetComponent<templateController> ().figureList[_fN].getVertexPosition();
		setVectorAngle ();
		drawExample ();
	}
	private void drawExample() 
	{
		GetComponent<LineRenderer> ().SetVertexCount(vertexPosition.Length);
		GetComponent<LineRenderer> ().SetPositions (vertexPosition);
		GetComponent<LineRenderer> ().material = _material;
	}
	private void setVectorAngle() 					
	{
		vectorsAngle = new float[vertexPosition.Length-1];
		vertexDirection = new Vector2[vertexPosition.Length - 1];
		for (int i = 0; i < vectorsAngle.Length; i++) {
			if (i + 1 != vertexPosition.Length) {
				float dirX = vertexPosition [i + 1].x - vertexPosition [i].x;
				float dirY = vertexPosition [i + 1].y - vertexPosition [i].y;
				dirX = Mathf.Abs (dirX);
				dirY = Mathf.Abs (dirY);
				float vertexRange = Mathf.Sqrt (Mathf.Pow (dirX, 2) + Mathf.Pow (dirY, 2));
				vertexDirection [i] = new Vector2(dirX/vertexRange, dirY/vertexRange);
			}
		}
	}
}


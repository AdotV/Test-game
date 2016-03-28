using UnityEngine;
using System.Collections;

public class figure 
{
	private int vertexCount;
	private Vector3[] vertexPosition;
	private int level;

	public figure(int count) 
	{
		level = 0;	
		vertexPosition = new Vector3[count];
	}
	public void addNewVertex(Vector3 _position, int positionNumber)
	{
		vertexPosition [positionNumber] = _position;
		vertexPosition [positionNumber].z = -1;
	}
	public Vector3[] getVertexPosition() 
	{
		return vertexPosition;
	}
	public void setLevel(int _level)
	{
		level = _level;
	}
	public int getLevel()
	{
		return level;
	}
}


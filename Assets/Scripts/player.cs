using UnityEngine;
using System.Collections;

public class player 
{
	private float playerTime;
	private int playerScore;
	private int playerLevel;

	public player()
	{
		playerTime = 30;
		playerScore = 0;
		playerLevel = 0;
	}

	public void setPlayerTime(float _time)
	{
		playerTime = _time;
	}
	public float getPlayerTime() 
	{
		return playerTime;
	}
	public void setPlayerScore(int _score) 
	{
		playerScore = _score;
	}
	public int getPlayerScore()
	{
		return playerScore;
	}
	public void setPlayerLevel(int _level) {
		playerLevel = _level;
	}
	public int getPlayerLevel() {
		return playerLevel;
	}
}


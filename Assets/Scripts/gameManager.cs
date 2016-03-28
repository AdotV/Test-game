using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gameManager : MonoBehaviour {

	public GameObject template;
	public player _player;
	public bool isPlay;
	public bool isTemplate;
	public Button startButton;
	public Text startButtonText;
	public Text timeText;
	public Text message;
	public Text scoreText;
	private float _time;
	void Start () 
	{
		_player = new player ();
		isPlay = false;
		message.GetComponent<CanvasGroup> ().alpha = 0;
		startButtonText.text = "Старт";
	}
	private void setTime() {
		_time = _player.getPlayerTime () - _player.getPlayerLevel ();
		if (_time <= 0) {
			_time = 1.5f;
		}
	}
	public void nextFigure() 
	{
		setTime ();
		createExampleFigure ();
	}
	private void createExampleFigure()
	{
		int figureNumber = 0;
		isPlay = true;
		isTemplate = false;
		if (_player.getPlayerLevel () > 0) {
			_player.setPlayerScore (_player.getPlayerScore () + 1);
		}
		_player.setPlayerLevel (_player.getPlayerLevel () + 1);
		for (int i = 0; i < template.GetComponent<templateController> ().figureList.Count; i++) {
			if (_player.getPlayerLevel () == template.GetComponent<templateController> ().figureList [i].getLevel ()) {
				isTemplate = true;
				figureNumber = i;
				break;
			}
		}
		if (isTemplate) {
			GetComponent<figureGenerator> ().manualCreating (figureNumber, template);
		} else {
			GetComponent<figureGenerator> ().setVertexPositions (_player.getPlayerLevel () + 2);
		}
	}
	IEnumerator waitTwoSeconds() {
		yield return new WaitForSeconds (2.5f);
		message.GetComponent<CanvasGroup> ().alpha = 0;
		startButton.interactable = true;
		startButtonText.text = "Начать сначала";
	}
	void Update()
	{
		if (isPlay) {
			scoreText.text = "Счет: " + _player.getPlayerScore ().ToString ();
			if (_time > 0) {
				_time -= Time.deltaTime;
				timeText.text = _time.ToString();
			} else {
				if (isPlay) {
					message.text = "Вы проиграли :(";
					message.GetComponent<CanvasGroup> ().alpha = 1;
					_player.setPlayerLevel (0);
					_player.setPlayerScore (0);
					StartCoroutine (waitTwoSeconds ());
				}
				isPlay = false;
				timeText.text = "0.00";
			}
		}
	}
	public void endDrawing()
	{
		isPlay = false;
		startButton.interactable = true;
		startButtonText.text = "Старт";
		_player.setPlayerLevel (0);
		_player.setPlayerScore (0);
		message.GetComponent<CanvasGroup> ().alpha = 0;
	}
	public void Exit()
	{
		Application.Quit ();
	}
}

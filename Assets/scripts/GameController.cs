using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Text textTime;
    public Text textShapeshifts;
    public Text textMoves;
    public Text textType1;
    public Text textType2;

    public GameObject overlay;
    public GameObject successPanel;
    public GameObject helpPanel;
    public Text textSuccess_Moves;
    public Text textSuccess_Shapeshifts;
    public Text textSuccess_Time;
    public Text textMap;

    public AudioSource audioTargetHit;
    public AudioSource audioClick;



    public int shapeshifts = 0;
    public int moves = 0;
    private float time = 0;


    public int type1Min = 1;
    public int type2Min = 1;
    public int type1Done = 0;
    public int type2Done = 0;

    private bool isPlaying = false;

    void Start()
    {
        this.audioTargetHit = GetComponent<AudioSource>();

        this.textShapeshifts.text = this.shapeshifts.ToString();
        this.textMoves.text = this.moves.ToString();

        this.textType1.text = this.type1Done.ToString() + " / " + this.type1Min.ToString();
        this.textType2.text = this.type2Done.ToString() + " / " + this.type2Min.ToString();
        this.textTime.text = "0.0s";

        GameData gameData = GameData.GetInstance();
        if(gameData.displayHelp)
        {
            this.overlay.SetActive(true);
            this.helpPanel.SetActive(true);
            gameData.displayHelp = false;
        }else
        {
            this.isPlaying = true;
        }

        this.textMap.text = "Map "+gameData.getMapCountAsString();
    }

    void Update()
    {
        if (!this.isPlaying) return;

        this.time += Time.deltaTime;
        this.textTime.text = string.Format("{0:0.0}s", this.time);

    }

    public void AddShapeshift()
    {
        this.shapeshifts++;
        this.textShapeshifts.text = this.shapeshifts.ToString();
    }

    public void AddMove()
    {
        this.moves++;
        this.textMoves.text = this.moves.ToString();
    }

    public void AddDoneShape(int type)
    {
        this.audioTargetHit.Play();

        switch (type)
        {
            case 1:
                this.type1Done++;
                this.textType1.text = this.type1Done.ToString() + " / " + this.type1Min.ToString();
                break;
            case 2:
                this.type2Done++;
                this.textType2.text = this.type2Done.ToString() + " / " + this.type2Min.ToString();
                break;
        }


       if(this.type1Done == this.type1Min  && this.type2Done == this.type2Min)
       {
            this.isPlaying = false;

            GameData gameData = GameData.GetInstance();

            int bestShapeshifts = PlayerPrefs.GetInt("shapeshifts_"+gameData.map);
            int bestMoves = PlayerPrefs.GetInt("moves_" + gameData.map);
            float bestTime = PlayerPrefs.GetFloat("time_" + gameData.map);

            string bestMovesStr = " (best: )";
            if (bestMoves == 0)
            {
                bestMoves = int.MaxValue;
                
            }else
            {
                bestMovesStr = " (best: " + bestShapeshifts.ToString() + ")";
            }

            string bestShapeshiftsStr = " (best: )";
            if (bestShapeshifts == 0)
            {
                bestShapeshifts = int.MaxValue;
            }else
            {
                bestShapeshiftsStr = " (best: " + bestMoves.ToString() + ")";
            }

            string bestTimeStr = " (best: )";
            if(bestTime == 0.0f)
            {
                bestTime = float.MaxValue;
            }else
            {
                bestTimeStr = " (best: " + string.Format("{0:0.0}s", bestTime) + ")";
            }


            this.textSuccess_Shapeshifts.text = "Shapeshifts: "+this.shapeshifts.ToString() + bestMovesStr;
            this.textSuccess_Moves.text = "Moves: " + this.moves.ToString() + bestShapeshiftsStr;
            this.textSuccess_Time.text = "Time: " + string.Format("{0:0.0}s", this.time) + bestTimeStr;



            if(this.shapeshifts < bestShapeshifts) { 
                PlayerPrefs.SetInt("shapeshifts_" + gameData.map, this.shapeshifts);
            }

            if(this.moves < bestMoves) { 
                PlayerPrefs.SetInt("moves_" + gameData.map, this.moves);
            }

            if(this.time < bestTime) {
                PlayerPrefs.SetFloat("time_" + gameData.map, this.time);
            }
            

            this.overlay.SetActive(true);
            this.successPanel.SetActive(true);
        }
    }

    public void Reload()
    {
        this.audioClick.Play();
        Application.LoadLevel(Application.loadedLevel); 
    }

    public void SetMinCount(int type1Min, int type2Min)
    {
        this.type1Min = type1Min;
        this.type2Min = type2Min;

        this.textType1.text = this.type1Done.ToString() + " / " + this.type1Min.ToString();
        this.textType2.text = this.type2Done.ToString() + " / " + this.type2Min.ToString();
    }

    public void LoadNextMap()
    {
        this.audioClick.Play();

        GameData gameData = GameData.GetInstance();
        gameData.SetNextMap();

        this.Reload();
    }

    public void HideHelp()
    {
        this.audioClick.Play();

        this.overlay.SetActive(false);
        this.helpPanel.SetActive(false);
        this.isPlaying = true;
    }

    public void ShowHelp()
    {
        this.audioClick.Play();

        this.overlay.SetActive(true);
        this.helpPanel.SetActive(true);
       // this.isPlaying = false;
    }
}



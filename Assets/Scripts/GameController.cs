using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI inputField;
    public TextMeshProUGUI inputFieldPlaceHolder;
    public TextMeshProUGUI gameLoadedText;
    public TextMeshProUGUI modeText;
    public TextMeshProUGUI currentModeText;

    public GameObject outButton;
    public GameObject ofButton;
    public GameObject controlButton;
    public GameObject controlOnButton;

    public Vector2 winOffset;

    public Camera cam;

    TextMeshProUGUI outButtonText;
    TextMeshProUGUI ofButtonText;
    TextMeshProUGUI controlButtonText;

    public float feedbackTextDelay = 0.1f;

    bool won = false;

    enum Mode
    {   
        RESET = -1,
        OUT, 
        OF,
        CONTROL
    }
    
    Mode mode = Mode.RESET;

    public int getMode
    {
        get
        {
            return (int)mode;
        }
    }

    private void Awake()
    {
        outButtonText     = outButton.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>();
        ofButtonText      = ofButton.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>();
        controlButtonText = controlButton.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>();

        inputField.text = "reset";

        inputFieldPlaceHolder.enabled = false;
    }

    private void Update()
    {
        input();
        GameOver();

        if (inputField.text == "")
        {
            inputFieldPlaceHolder.enabled = true;
        }
    }

    // Private Update functions ----------------------------------------------------------------------------------------

    void input()
    {
        if (Input.GetKeyDown("return")) {
            string inputText = correctLength(inputField.text.Trim().ToLower());

            gameLoadedText.enabled = false;
            modeText.enabled = false;
            currentModeText.enabled = false;

            StartCoroutine(displayText(inputText, feedbackTextDelay));
        }
    }

    IEnumerator displayText(string inputText, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        gameLoadedText.color = Color.green;
        gameLoadedText.enabled = true;

        modeText.color = Color.green;
        modeText.enabled = true;

        currentModeText.color = Color.yellow;
        currentModeText.enabled = true;

        if (inputText.Equals("out"))
        {
            this.mode = Mode.OUT;
            gameLoadedText.text = "> Command Excecuted Successfully.";
            currentModeText.text = "OUT";
        }
        else if (inputText.Equals("of"))
        {
            this.mode = Mode.OF;
            gameLoadedText.text = "> Command Excecuted Successfully.";
            currentModeText.text = "OF";
        }
        else if (inputText.Equals("control"))
        {
            this.mode = Mode.CONTROL;
            gameLoadedText.text = "> Command Excecuted Successfully.";
            currentModeText.text = "CONTROL";
        }
        else if(inputText.Equals("play") && won)
        {
            SceneManager.LoadScene(0);
        }
        else if (inputText.Equals("reset"))
        {
            this.mode = Mode.RESET;
            SceneManager.LoadScene(0);
        }
        else if (inputText.Contains("out") && inputText.Contains("of") && inputText.Contains("control"))
        {
            gameLoadedText.text = " > Command Excecution Failed.\n" +
                "> (Maybe try using those words.)";
        }
        //else if (inputText.Equals("exit"))
        //{
        //    this.mode = Mode.RESET;
        //    gameLoadedText.color = Color.red;

        //    modeText.enabled = false;
        //    currentModeText.enabled = false;

        //    gameLoadedText.text = "> Exiting...";
        //    Application.Quit();
        //}
        else
        {
            modeText.enabled = false;
            currentModeText.enabled = false;

            gameLoadedText.color = Color.red;
            gameLoadedText.text = "> Command Excecution Failed.";
        }
    }

    void containsOutofControl(string inputText)
    {
        if (inputText.Contains("out") && inputText.Contains("of") && inputText.Contains("control"))
        {
            gameLoadedText.text = " > Command Excecution Failed.\n" +
                "> Maybe try using those words.";
        }
    }

    void GameOver()
    {
        bool xCondition = ofButton.transform.position.x > controlButton.transform.position.x + winOffset.x;
        bool yCondition = Mathf.Abs(ofButton.transform.position.y - controlButton.transform.position.y) < winOffset.y;
        if (xCondition && yCondition && !outButton.activeInHierarchy && !won && ofButtonText.text == "ON")
        {
            won = true;
            Win();
        }
    }

    void Win()
    {
        outButton.SetActive(false);
        ofButton.SetActive(false);
        controlButton.SetActive(false);

        modeText.enabled = false;
        currentModeText.enabled = false;

        controlOnButton.SetActive(true);

        gameLoadedText.text = "> Congratulations!\n\n" +
            "> Now you can finally play the game (YAY!)\n\n" +
            "> Use \"play\" to play the game";
    }

    // Public Button Functions --------------------------------------------------------------------------------------------------
    public void OutButton()
    {
        if (this.mode == Mode.OUT)
        {
            outButton.SetActive(false);
        }

        //else if (this.mode == Mode.OF)
        //{
        //    if (outButtonText.text.Equals("OUT"))
        //        outButtonText.text = "IN";
        //    else
        //        outButtonText.text = "OUT";
        //}
    }

    public void OfButton()
    {
        if (this.mode == Mode.OUT)
        {
            ofButton.SetActive(false);
        }

        else if (this.mode == Mode.OF)
        {
            if (ofButtonText.text.Equals("OF"))
                ofButtonText.text = "ON";
            else
                ofButtonText.text = "OF";
        }
    }

    public void ControlButton()
    {
        if (this.mode == Mode.OUT)
        {
            controlButton.SetActive(false);
        }

        //else if (this.mode == Mode.OF)
        //{
        //    if (controlButtonText.text.Equals("CONTROL"))
        //        controlButtonText.text = "CHAOS";
        //    else
        //        controlButtonText.text = "CONTROL";
        //}
    }

    // Utilities --------------------------------------------------------------------------------------------------------------------------

    string correctLength(string str)
    {
        string filteredString = "";

        for (int i = 0; i < str.Length - 1; i++)
        {
            filteredString += str[i];
        }

        return filteredString;
    }
}

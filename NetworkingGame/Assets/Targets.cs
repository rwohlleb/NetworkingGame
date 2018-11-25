using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using ClientHelper;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.IO;

public class Targets : MonoBehaviour
{

    public float delay = .2f;
    public GameObject cube;
    public GameObject startText;
    public GameObject threeText;
    public GameObject twoText;
    public GameObject oneText;
    public GameObject player1Title;
    public GameObject player2Title;
    public GameObject player1Score;
    public GameObject player2Score;
    public GameObject scoreboard;
    public GameObject player1Win;
    public GameObject player2Win;
    public GameObject tieText;
    public TextMesh player1ScoreText;
    public TextMesh player2ScoreText;
    public bool killInvoke = false;
    private Player playerOne = new Player();
    private Player playerTwo = new Player();
    private bool start = false;
    public bool doThree = false;
    public bool doTwo = false;
    public bool doOne = false;
    public bool player1WinBool = false;
    public bool player2WinBool = false;
    public bool tie = false;
    public string ip = "";
    public bool gameStarted = false;
    Client client;

    // Use this for initialization
    void Start()
    {
        playerOne.score = "0";
        playerTwo.score = "0";
        ip = GetIP();
        client = new Client(ip, 800);
        client.OnClientReceived += DisplayScore;
        client.Start();
        client.Send("ready");
        this.startText = Instantiate(startText, new Vector3(-2.17f, 3.05f, 5.3f), Quaternion.identity);
        EventManager.TargetDestroyed += UpdateScore;
    }

    private void Update()
    {
        
        if (gameStarted)
        {
            this.player1ScoreText.text = playerOne.score;
            this.player2ScoreText.text = playerTwo.score;
        }

        if(player1WinBool)
        {
            Instantiate(player1Win, new Vector3(0, 3, 9), Quaternion.identity);
            player1WinBool = false;
        }

        if (player2WinBool)
        {
            Instantiate(player2Win, new Vector3(0, 3, 9), Quaternion.identity);
            player2WinBool = false;
        }
        if (tie)
        {
            Instantiate(tieText, new Vector3(1, 3, 9), Quaternion.identity);
            tie = false;
        }

        if (killInvoke)
        {
            CancelInvoke();
            killInvoke = false;
            if (playerOne.score.CompareTo(playerTwo.score) > 0)
            {
                Task task = new Task(() =>
                {
                    Thread.Sleep(2000);
                    player1WinBool = true;
                });
                task.Start();
            }
            if (playerTwo.score.CompareTo(playerTwo.score) > 0)
            {
                Task task = new Task(() =>
                {
                    Thread.Sleep(2000);
                    player2WinBool = true;
                });
                task.Start();
            }
            if (playerOne.score.Equals(playerTwo.score))
            {
                Task task = new Task(() =>
                {
                    Thread.Sleep(2000);
                    tie = true;
                });
                task.Start();
            }
        }
        if (start)
        {
            Destroy(this.startText);

            if (doThree)
            {
                Thread.Sleep(1000);
                Destroy(this.threeText);
                doThree = false;
                this.twoText = Instantiate(twoText, new Vector3(4.17f, 3.05f, 5.3f), Quaternion.identity);
                doTwo = true;
            }
            else if (doTwo)
            {
                Thread.Sleep(1000);
                Destroy(this.twoText);
                doTwo = false;
                this.oneText = Instantiate(oneText, new Vector3(4.17f, 3.05f, 5.3f), Quaternion.identity);
                doOne = true;
            }
            else if (doOne)
            {
                Thread.Sleep(1000);
                Destroy(this.oneText);
                doOne = false;
                InvokeRepeating("Spawn", 0, delay);
                Task timer = new Task(() =>
                {
                    Thread.Sleep(10000);
                    this.killInvoke = true;
                });
                this.scoreboard = Instantiate(scoreboard, new Vector3(5f, 6.9f, 8.5f), Quaternion.identity);
                this.player1Title = Instantiate(player1Title, new Vector3(1.85f, 8.2f, 9.5f), Quaternion.identity);
                this.player2Title = Instantiate(player2Title, new Vector3(1.85f, 7.2f, 9.5f), Quaternion.identity);
                this.player1Score = Instantiate(player1Score, new Vector3(5.58f, 8.2f, 9.5f), Quaternion.identity);
                this.player2Score = Instantiate(player2Score, new Vector3(5.58f, 7.2f, 9.5f), Quaternion.identity);
                this.player2ScoreText = this.player2Score.GetComponent("TextMesh") as TextMesh;
                this.player1ScoreText = this.player1Score.GetComponent("TextMesh") as TextMesh;
                timer.Start();
                start = false;
                gameStarted = true;
                
            }

            if (!doThree && !doTwo && !doOne && start)
            {
                this.threeText = Instantiate(threeText, new Vector3(4.17f, 3.05f, 5.3f), Quaternion.identity);
                doThree = true;
            }
        }        
    }

    public string GetIP()
    {
        string dir = Directory.GetCurrentDirectory();
        StreamReader reader = new StreamReader(dir + @"/Assets/Client.txt");
        string ipAdd = reader.ReadLine();
        reader.Close();
        return ipAdd;
    }

    void UpdateScore()
    {
        this.client.Send(ip);
    }

    void DisplayScore(object sender, string data)
    {
        if (data.Equals("start"))
        {
            start = true;
        }
        else
        {
            var arr = data.Split(',');
            this.playerOne.address = arr[0];
            this.playerOne.score = arr[1];
            this.playerTwo.address = arr[2];
            this.playerTwo.score = arr[3];


        }
    }

    void Spawn()
    {
        Instantiate(cube, new Vector3(Random.Range(-15, 25), 10, 9), Quaternion.identity);
    }
}

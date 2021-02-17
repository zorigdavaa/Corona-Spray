using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject player;
    [SerializeField] Transform LevelTrigger;
    [SerializeField] GroundSpawner groundSpawner;
    [SerializeField] GameObject GameCanvas;
    PlayerMovement playerMovement;
    TextMeshProUGUI txtBullet;
    TextMeshProUGUI txtDistance;
    TextMeshProUGUI txtDisInfect;
    TextMeshProUGUI txtLevel;
    public GameObject beforeStartMenu;
    public GameObject afterDieMenu;
    public GameObject afterWinMenu;

    private int disInfected;

    public int DisInfected
    {
        get { return disInfected; }
        set 
        { 
            disInfected = value;
            RefreshDisInfectText();
        }
    }

    [SerializeField] int level;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            RefreshLevelText();
        }
    }

    public int LastBariaScore { get; set; } = 0;
    public int LastBariaBullet { get; set; } = 0;
    public int LastBariaDisInfect { get; set; } = 0;

    private void Awake()
    {
        SingleTon();
        CacheReference();
    }

    private void CacheReference()
    {
        player = GameObject.Find("Player");
        GameCanvas = GameObject.Find("Canvas");
        LevelTrigger = GameObject.Find("Baria").transform;
        groundSpawner = FindObjectOfType<GroundSpawner>();
        txtBullet = GameCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        txtDistance = GameCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        txtDisInfect = GameCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        txtLevel = GameCanvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        beforeStartMenu = GameCanvas.transform.GetChild(4).gameObject;
        afterDieMenu = GameCanvas.transform.GetChild(5).gameObject;
        afterWinMenu = GameCanvas.transform.GetChild(6).gameObject;
        playerMovement = player.GetComponent<PlayerMovement>();
        
        
    }
    private void SingleTon()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Level = 1;
        RefreshBulletText();
        RefreshDisInfectText();
        RefreshLevelText();
    }

    public void RefreshDistanceText()
    {
        txtDistance.text = "Distance: " + playerMovement.Distance.ToString("0");
    }   
    public void RefreshLevelText()
    {
        txtLevel.text = "Level: " + Level;
    }
    public void RefreshBulletText()
    {
        txtBullet.text = "Bullet: " + playerMovement.RemainingBullet + "/" + playerMovement.MaxBulletCapacity;
    }
    public void RefreshDisInfectText()
    {
        txtDisInfect.text = "DisInfect: " + DisInfected;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        playerMovement.StartGame();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SetScoresToLastBaria()
    {
        playerMovement.RemainingBullet = LastBariaBullet;
        DisInfected = LastBariaDisInfect;
    }
    public void ContinuePlaying()
    {
        Floor[] tiles = FindObjectsOfType<Floor>();
        foreach (var tile in tiles)
        {
            Destroy(tile.gameObject);
        }
        //Todo: Suuld Bariand ochson onoog tawih
        SetScoresToLastBaria();
        // suuld ochson levelTriggerees ehelj zamiig tawih
        groundSpawner.setNextSpawnPoint(new Vector3(LevelTrigger.position.x + 5, LevelTrigger.position.y, LevelTrigger.position.z - 300));
        groundSpawner.SpawnTiles();
        // suuld ochson LevelTrigger deer playeriig ochuulah
        player.GetComponent<Transform>().position = new Vector3(LevelTrigger.position.x + 5, 0.5f, LevelTrigger.position.z - 300);
        playerMovement.Alive = true;
        playerMovement.StartingPoint();
    }
}
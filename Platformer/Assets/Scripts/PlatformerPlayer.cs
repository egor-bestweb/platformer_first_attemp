using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class PlatformerPlayer : MonoBehaviour
{
    // for primary settings
    [SerializeField] private float spd;
    private float speed;
    private float jumpForce = 1200.0f;
    private float res;

    // for invisiblity UI
    [SerializeField] private SettingsImage img; // menu
    [SerializeField] private QuestImage img2; // menu

    // for controller about player
    private Rigidbody2D _body;
    private Animator _anim;
    private BoxCollider2D _box;
    private float deltaX = 0f;

    // for UI
    private const float baseSpeed = 3.0f;

    // for detection about both start and end game
    [SerializeField] private BoxCollider2D _endBox;
    [SerializeField] private BoxCollider2D _startBox;
    private bool start_game = false;
    private bool end_game = false;
    private BoxCollider2D _TempstartBox;
    private BoxCollider2D _TempendBox;
    public bool game = true; // !!! 

    // for border around game's scine
    [SerializeField] private GameOver g_over;
    [SerializeField] private BoxCollider2D border;
    private BoxCollider2D tmp_border;

    // for timer
    [SerializeField] private Text _txt;
    [SerializeField] private Text _txttable;
    private float timer = 0.0f;
    private int i = 0;
    private string path= "";

    // for saving
    private DoubleNode savehead;
    static private string writePath;
    BinaryFormatter bf = new BinaryFormatter();
    List L = new List();

    // for enemy
    [SerializeField] private BoxCollider2D _mob;
    private BoxCollider2D _tmpMob;

    // for music 

    // voids

    void Awake()
    {
        path = Path.Combine(Application.dataPath, "Save.json");
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnSpeedChanged(float value)
    {
        speed = spd * res * value;
    }


    private void Start()
    {
        writePath = Path.Combine(Application.dataPath, "score.txt");
        _body = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
        _txt.text = "0";
        _txttable.text = this.LoadGame().Display();
        res = Mathf.Sqrt(Mathf.Pow(Screen.height, 2) + Mathf.Pow(Screen.width, 2));
        speed = spd * res;
    }

    private void Update()
    {
        if(Mathf.Abs(res - Mathf.Sqrt(Mathf.Pow(Screen.height, 2) + Mathf.Pow(Screen.width, 2))) >= 1f)
        {
            res = Mathf.Sqrt(Mathf.Pow(Screen.height, 2) + Mathf.Pow(Screen.width, 2));
            speed = spd * res;
        }
        Debug.Log("Diagonal = " + res);
        Debug.Log("Speed = " + speed);

        if (!img.Status() && !img2.Status() && game)
        {
            deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            Vector2 movement = new Vector2(deltaX, _body.velocity.y);
            _body.velocity = movement;

            Vector3 max = _box.bounds.max; // max is center + extent, max is top-right regtangle
            Vector3 min = _box.bounds.min; // min is center - extent, min is bottom-left regtangle

            Vector2 corner1 = new Vector2(max.x, min.y - 0.1f); // (bottom limit's line - 0.1f) & right
            Vector2 corner2 = new Vector2(min.x, min.y - 0.2f); //  (bottom limit's line - 0.2f) & left
                                                                // thus, terms -0.1f and -0.2f allow to create figure which has wight equals 0.1f

            Collider2D hit = Physics2D.OverlapArea(corner1, corner2); // searching objects within this area and if object are found, then "hit" gets info about founded object

            bool grounded = false;

            if (hit != null)
            {
                grounded = true;
            }

            _body.gravityScale = (grounded && deltaX == 0) ? 0 : 1;

            if (grounded && Input.GetKeyDown(KeyCode.Space))
            {
                _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            ///////// where is display info about game's time 

            if (hit != null)
            {
                _TempstartBox = hit.GetComponent<BoxCollider2D>();
                _TempendBox = hit.GetComponent<BoxCollider2D>();
            }

            if (_startBox != _TempstartBox)
            {
                start_game = true;
            }

            if (_endBox == _TempendBox)
            {
                end_game = true;
            }
            //////////////////////////////////////////////////////////////////////////
            
            if(hit != null)
            {
                _tmpMob = hit.GetComponent<BoxCollider2D>();
            }

            if(_tmpMob == _mob)
            {
                game = false;
                g_over.OnOpenGO();
            }
            
            //////////////////////////////////////////////////////////////////////////

            if (start_game == true && end_game == false)
            {
                // log part
                timer += Time.deltaTime;

                
                _txt.text = System.Convert.ToString(Math.Round(timer, 3));
            }
            //////////////////////////////////////////////////////////////////////////
            if (hit != null)
            {
                tmp_border = hit.GetComponent<BoxCollider2D>();

                if (tmp_border == border)
                {
                    game = false;
                    g_over.OnOpenGO();
                }
            }
            /////////
            MovingPlatform platform = null;
            if (hit != null)
            {
                platform = hit.GetComponent<MovingPlatform>();
            }

            if (platform != null)
            {
                transform.parent = platform.transform;
            }
            else
            {
                transform.parent = null;
            }

            _anim.SetFloat("speed", Mathf.Abs(deltaX));


            if (Mathf.Sign(deltaX) == -1)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (Mathf.Sign(deltaX) == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (end_game)
            game = false;
        if(!game)
        {
            _anim.enabled = false;
        }

        if (end_game && i == 0)
        {
            L = this.LoadGame(); // выгружаем в список сохраненную игру
             
            // выполняем нужные операции в списке 
            L.Add(Math.Round(timer, 3));
            L.DeleteLast();

            this.SaveGame(L); // сохраняем список

            _txttable.text = L.Display(); // выводим список

            i++;
        }
    }


    /// <summary>
    /// Сохраняем игру
    /// </summary>
    /// <param name="lst"></param>
    private void SaveGame(List lst)
    {
        FileStream file;
        if (!File.Exists(writePath))
        {
            file = new FileStream(writePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None); // if don't exist, than create

        } // I need variant that file on writePath is exist, and I take this file and cover data to this file
        else
        {
             file = File.Open(writePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        }

        bf.Serialize(file, lst);
        file.Close();
    }

    /// <summary>
    /// Загружаем игру
    /// </summary>
    /// <returns></returns>
    private List LoadGame()
    {
        FileStream file;
        List lst = new List();
        if (!File.Exists(writePath))
        {
            file = new FileStream(writePath, FileMode.OpenOrCreate);
        }
        else
        {
            file = File.Open(writePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        }
        if(file.Length > 0)
            lst = (List)bf.Deserialize(file);
        file.Close();

        return lst;
    }
    
}


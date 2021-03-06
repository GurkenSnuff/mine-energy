using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Mirror;

public class mapGenerator : NetworkBehaviour
{
    public GameObject block, block1;
    private float b = 0, c = 0, e = 0;
    public Tile[] tiles;
    public GameObject d;
    public SolarZellen solarZellen;
    public List<NetworkConnection> Clients = new List<NetworkConnection>();
    public List<GameObject> Player = new List<GameObject>();
    public GameObject newPlayer;
    public int rand, x = 0;
    [SerializeField]
    private Tilemap map;
    private MapManager mapManager;
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    public GameObject Hitbox, LavaHitbox;
    private float resistanceCheck;
    private int UpdateTile;
    private Vector3 t, s;
    public List<int> UpdateSaveTile = new List<int>();
    public List<Vector3> UpdateSavePosition = new List<Vector3>();
    public bool newClientJoined = false;
    public bool isPlaced = false,check=false;
    public GameObject k;
    private List<Vector3> colliderList = new List<Vector3>();
    private TileUpdater tileUpdater;
    public List<TileUpdater> tileupdater = new List<TileUpdater>();
    [SerializeField]
    private List<TileBase> junkTiles = new List<TileBase>();
    private List<Vector3> positions = new List<Vector3>();
    private bool transmit=false;
    public List<double> TileLife = new List<double>();
    public List<Vector3> TilePosition = new List<Vector3>();
    public int number=0;
    private bool Add = false;

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        tileUpdater = FindObjectOfType<TileUpdater>();


        mapManager = FindObjectOfType<MapManager>();
        map = FindObjectOfType<Tilemap>();
    }
    void Start()
    {
        if (isServer)
        {
            while (b >= -10)
            {

                rand = Random.Range(0, tiles.Length);
                t = new Vector3(transform.position.x, transform.position.y + b, transform.position.z);
                map.SetTile(map.WorldToCell(t), tiles[rand]);
                resistanceCheck = mapManager.GetTileResistance(t);
                UpdateSaveTile.Add(rand);
                UpdateSavePosition.Add(t);

                GameObject a = Instantiate(Hitbox) as GameObject;
                a.tag = "Server";
                a.transform.position = t;

                if (mapManager.GetTileResistance(t) == 18)
                {
                    GameObject b = Instantiate(LavaHitbox) as GameObject;
                    b.tag = "Server";
                    b.transform.position = t;

                }
                while (e <= 10)
                {
                    e += 1.085f;
                    rand = Random.Range(0, tiles.Length);
                    s = new Vector3(transform.position.x + e, transform.position.y + b, transform.position.z);
                    map.SetTile(map.WorldToCell(s), tiles[rand]);
                    resistanceCheck = mapManager.GetTileResistance(s);
                    UpdateSaveTile.Add(rand);
                    UpdateSavePosition.Add(s);

                    GameObject d = Instantiate(Hitbox) as GameObject;
                    d.tag = "Server";
                    d.transform.position = s;
                }
                e = 0;
                b -= 1.183f;
            }
            while (c <= 10)
            {
                c += 1.085f;
                rand = Random.Range(0, tiles.Length);
                s = new Vector3(transform.position.x + c, transform.position.y, transform.position.z);
                map.SetTile(map.WorldToCell(s), tiles[rand]);
                resistanceCheck = mapManager.GetTileResistance(s);
                UpdateSaveTile.Add(rand);
                UpdateSavePosition.Add(s);

                GameObject a = Instantiate(Hitbox) as GameObject;
                a.tag = "Server";
                a.transform.position = s;

                if (mapManager.GetTileResistance(s) == 18)
                {
                    GameObject b = Instantiate(LavaHitbox) as GameObject;
                    b.tag = "Server";
                    b.transform.position = s;

                }

            }
            

        }
        
        
    }



     IEnumerator WaitforplayerTile(int neww, Vector3 pos)
     {
        yield return new WaitForSeconds(0.1f);
        
        GetTile(neww, pos);
     }


    [ClientRpc]
         void GetTile(int neww , Vector3 pos)
         {
           map.SetTile(map.WorldToCell(pos), tiles[neww]);
            
         }

    
    public void AddLife(Vector3 pos, double Life, int place)
    {
        bool Add2 = false;
        if (Add==false)
        {
            TileLife.Add(Life);
            TilePosition.Add(pos);
            Add = true;
            Add2 = true;
        }
        else
        {
            TileLife[place] = Life;
            TilePosition[place] = pos;
        }
        CommitLife(pos, Life, place,Add2);
    }

    [ClientRpc]
    public void CommitLife(Vector3 pos,double Life,int place,bool Add)
    {
        if (Add==true)
        {
            TileLife.Add(Life);
            TilePosition.Add( pos);
        }
        else
        {
            TileLife[place] = Life;
            TilePosition[place] = pos;
        }
    }

    [TargetRpc]
    public void newlyJoined(NetworkConnection conn,double Life,Vector3 pos)
    { 
            TileLife.Add(Life);
            TilePosition.Add(pos);
    }
    
}
    



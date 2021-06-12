using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum TileDirection
{
    North,
    East,
    South,
    West
}
public enum TileType
{
    Ground,     

    SideN,      
    SideE,      
    SideS,      
    SideW,      

    CornerN,    
    CornerE,    
    CornerS,    
    CornerW,    

    CornerInN,
    CornerInE,
    CornerInS,
    CornerInW

}

public class TileSet
{
    GameObject tile = null;

    public TileSet(GameObject _tile)
    {
        tile = _tile;
    }

    public Vector3 GetPosition()
    {
        if (tile)
        {
            return tile.transform.position;
        }

        Debug.LogError("Tile does not exist");
        return Vector3.zero;
    }

    public void SetParent(Transform _parent)
    {
        if (tile)
        {
            tile.transform.parent = _parent;
        }
    }

    public void SetPosition(Vector2 _position)
    {
        if (tile)
        {
            Vector3 newPos = tile.transform.localPosition;
            newPos.x = _position.x;
            newPos.z = _position.y;
            tile.transform.localPosition = newPos;
        }
    }
    public void SetHeight(float _height)
    {
        if (tile)
        {
            Vector3 newPos = tile.transform.localPosition;
            newPos.y = _height;
            tile.transform.localPosition = newPos;
        }
    }

    public void SetRotation(TileDirection _type)
    {
        float rotationY = 0.0f;

        switch (_type)
        {
            case TileDirection.North:       rotationY = 0.0f;       break;
            case TileDirection.East:        rotationY = 90.0f;      break;
            case TileDirection.South:       rotationY = 180.0f;     break;
            case TileDirection.West:        rotationY = 270.0f;     break;
            default:                                                break;
        }

        if (tile)
        {
            tile.transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }
    }
    public void SetFlipped(bool _flip)
    {
        if (tile && _flip)
        {
            tile.transform.localScale = new Vector3(-tile.transform.localScale.x, tile.transform.localScale.y, tile.transform.localScale.z);
        }
    }
}

public class TerrainGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(loadFile);
        
    }

    #region Tileset Related

    [SerializeField] TextAsset[] levelFiles = null;

    [SerializeField] Transform tilesParent = null;
    [SerializeField] GameObject[] TileSet_Mesh = null;
    [SerializeField] int loadFile = 0;

    public TileSet[,] tileMap { get; private set; }
    public Vector2Int TileSize { get; private set; }
    private int[,] levelMap;

    public Vector3 GetTilePosition(int _Width, int _Height)
    {
        if (tileMap == null)
        {
            if (_Width >= 0 && _Width < TileSize.x &&
                _Height >= 0 && _Height < TileSize.y)
            {
                return tileMap[_Width, _Height].GetPosition();
            }
            Debug.LogError("Out of Bounds");
        }
        Debug.LogError("TileMap does not exist");
        return Vector3.zero;
    }
    public void GenerateLevel()
    {
        if (tileMap == null && TileSize != null && TileSize.magnitude > 0)
        {
            tileMap = new TileSet[TileSize.x, TileSize.y];
           
            transform.position = new Vector3(- TileSize.x / 2, 0, -TileSize.y / 2);

            for (int y = 0; y < TileSize.y; y++)
                for (int x = 0; x < TileSize.x; x++)
                {
                    tileMap[x, y] = GetTileType(CalculateType(x, y));
                    if (tileMap[x, y] != null)
                    {
                        tileMap[x, y].SetParent(tilesParent);
                        tileMap[x, y].SetPosition(new Vector2(x,y));
                        tileMap[x, y].SetHeight(levelMap[x,y] * 0.5f);
                    }
                }
        }
    }

    private TileSet GetTileType(TileType _type)
    {
        int tileID = 0;
        TileDirection tileDirection = TileDirection.North;
        bool flip = false;

        switch (_type)
        {
            case TileType.Ground:   tileID = 0;              break;

            case TileType.SideN:    tileID = 1;  tileDirection = TileDirection.North;   break;
            case TileType.SideE:    tileID = 1;  tileDirection = TileDirection.East;    break;
            case TileType.SideS:    tileID = 1;  tileDirection = TileDirection.South;   break;
            case TileType.SideW:    tileID = 1;  tileDirection = TileDirection.West;    break;

            case TileType.CornerN:  tileID = 2;  tileDirection = TileDirection.North;   break;
            case TileType.CornerE:  tileID = 2;  tileDirection = TileDirection.East;    break;
            case TileType.CornerS:  tileID = 2;  tileDirection = TileDirection.South;   break;
            case TileType.CornerW:  tileID = 2;  tileDirection = TileDirection.West;    break;

            case TileType.CornerInN: tileID = 3; tileDirection = TileDirection.North; break;
            case TileType.CornerInS: tileID = 3; tileDirection = TileDirection.East;  break;
            case TileType.CornerInE: tileID = 3; tileDirection = TileDirection.South; break;
            case TileType.CornerInW: tileID = 3; tileDirection = TileDirection.West;  break;
                                
            default:
                break;
        }

        TileSet result = new TileSet(Instantiate(TileSet_Mesh[tileID]));

        result.SetRotation(tileDirection);
        result.SetFlipped(flip);

        return result;
    }

    private TileType CalculateType(int _Width, int _Height)
    {
        if (levelMap != null)
        {
            if (_Width > 0 && _Width < TileSize.x - 1 && _Height > 0 && _Height < TileSize.y - 1)
            {
                bool North = levelMap[_Width, _Height] <= levelMap[_Width, _Height + 1];
                bool East  = levelMap[_Width, _Height] <= levelMap[_Width + 1, _Height];
                bool South = levelMap[_Width, _Height] <= levelMap[_Width, _Height - 1];
                bool West  = levelMap[_Width, _Height] <= levelMap[_Width - 1, _Height];

                bool NorthEast = levelMap[_Width, _Height] <= levelMap[_Width + 1, _Height + 1];
                bool NorthWest = levelMap[_Width, _Height] <= levelMap[_Width - 1, _Height + 1];
                bool SouthEast = levelMap[_Width, _Height] <= levelMap[_Width + 1, _Height - 1];
                bool SouthWest = levelMap[_Width, _Height] <= levelMap[_Width - 1, _Height - 1];

                if (North && East && South && West)
                {
                    if (!NorthEast && NorthWest && SouthEast && SouthWest) return TileType.CornerInN;
                    if (NorthEast && NorthWest && SouthEast && !SouthWest) return TileType.CornerInE;
                    if (NorthEast && NorthWest && !SouthEast && SouthWest) return TileType.CornerInS;
                    if (NorthEast && !NorthWest && SouthEast && SouthWest) return TileType.CornerInW;

                    return TileType.Ground;
                }

                if (!North && East && South && West) return TileType.SideN;
                if (North && !East && South && West) return TileType.SideE;
                if (North && East && !South && West) return TileType.SideS;
                if (North && East && South && !West) return TileType.SideW;

                if (!North && !East && South && West) return TileType.CornerN;
                if (North && !East && !South && West) return TileType.CornerE;
                if (North && East && !South && !West) return TileType.CornerS;
                if (!North && East && South && !West) return TileType.CornerW;

            }
        }

        return TileType.Ground;
    }

    private void LoadLevel(int _level)
    {

        if (levelFiles != null && levelFiles.Length > _level && _level >= 0)
        {
            TextAsset textAsset = levelFiles[_level];

            string textString = textAsset.text;
            string[] textLines = textString.Split('\n');

            if (textString.Length > 0 && textLines.Length > 0)
            {
                TileSize = new Vector2Int(textLines[0].Length, textLines.Length);
                levelMap = new int[TileSize.x, TileSize.y];
                int tileHeight = 0;

                for (int i = 0; i < textLines.Length; i++)
                {
                    string valueLine = textLines[i];
                    for (int c = 0; c < valueLine.Length; c++)
                    {
                        if (int.TryParse(valueLine[c].ToString(), out tileHeight))
                        {
                            levelMap[c, i] = tileHeight;
                        }
                        else
                        {
                            levelMap[c, i] = 0;
                        }
                    }
                }

                GenerateLevel();
            }
        }

    }

    #endregion

}

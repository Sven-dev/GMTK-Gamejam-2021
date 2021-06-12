using System.Collections;
using System.Collections.Generic;
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

    CornerNF,
    CornerEF,
    CornerSF,
    CornerWF
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
        GenerateLevel(TileSize.x, TileSize.y);
    }

    #region Tileset Related

    [SerializeField] GameObject[] TileSet_Mesh = null;
    [SerializeField] Vector2Int TileSize = Vector2Int.one;

    public TileSet[,] tileMap { get; private set; }
   // public Vector2 TileSize { get; private set; }

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
    public void GenerateLevel(int _Width, int _Height)
    {
        if (tileMap == null && TileSet_Mesh != null && TileSet_Mesh.Length > 0)
        {
            tileMap = new TileSet[_Width, _Height];
           // TileSize = new Vector2Int(_Width, _Height);

            transform.position = new Vector3(- _Width / 2, 0, - _Height / 2);

            for (int y = 0; y < _Width; y++)
                for (int x = 0; x < _Height; x++)
                {
                    tileMap[x, y] = GetTileType(TileType.Ground);
                    if (tileMap[x, y] != null)
                    {
                        tileMap[x, y].SetParent(transform);
                        tileMap[x, y].SetPosition(new Vector2(x,y));
                        tileMap[x, y].SetHeight(0);
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

            case TileType.CornerNF: tileID = 2;  tileDirection = TileDirection.North; flip = true; break;
            case TileType.CornerEF: tileID = 2;  tileDirection = TileDirection.East;  flip = true; break;
            case TileType.CornerSF: tileID = 2;  tileDirection = TileDirection.South; flip = true; break;
            case TileType.CornerWF: tileID = 2;  tileDirection = TileDirection.West;  flip = true; break;
            default:
                break;
        }


        TileSet result = new TileSet(Instantiate(TileSet_Mesh[tileID]));

        result.SetRotation(tileDirection);
        result.SetFlipped(flip);

        return result;
    }

    #endregion

}

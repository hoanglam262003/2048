using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows { get; private set; }
    public TileCell[] cells { get; private set; }
    public int size => cells.Length;
    public int height => rows.Length;
    public int width => size / height;
    private void Awake()
    {
        rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();
    }

    private void Start()
    {
        for (int b = 0; b < rows.Length; b++)
        {
            for (int a = 0; a < rows[b].cells.Length; a++)
            {
                rows[b].cells[a].coordinates = new Vector2Int(a, b);
            }
        }
    }

    public TileCell GetRandomEmptyCell()
    {
        int index = Random.Range(0, cells.Length);
        int startIndex = index; 
        while (cells[index].occupied)
        {
            index++;

            if (index >= cells.Length)
            {
                index = 0;
            }
            if (index == startIndex)
            {
                return null;
            }
        }
        return cells[index];
    }

}

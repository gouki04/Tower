using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMap : MonoBehaviour
{
    public class Layer
    {
        public GameObject[,] Tiles = new GameObject[11, 11];
    }

    public int Row = 11;
    public int Col = 11;
    public List<Layer> Layers = new List<Layer>(1);

    
}

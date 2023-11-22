using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BatuPenutup : Interactable
{

    public bool open = false;
    public TilemapRenderer batuSprite;
    public TilemapCollider2D kolider;

    public void Open()
    {
        batuSprite.enabled = false;
        open = true;
        kolider.enabled = false;
    }

    public void Close()
    {
        batuSprite.enabled = true;
        open = false;
        kolider.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Coin")]
public class Coin : ScriptableObject
{
    public string CoinName;

    public GameObject StartingTile;

    public int row;
    public int column;

    private void FindRowAndColumn()
    {

    }
}

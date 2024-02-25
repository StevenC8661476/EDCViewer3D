using System.Collections;
using System;
using System.Collections.Generic;
using EDCViewer.Messages;
using UnityEngine;


public class MineralFormation : MonoBehaviour
{
    public GameObject IronMine;
    public GameObject GoldMine;
    public GameObject DiamondMine;
    public GameObject DiamondBlock;
    public GameObject IronBlock;
    public GameObject GoldBlock;

    public float BlockScale;
    public float MineScale;

    private void Start()
    {
        DiamondMine = Resources.Load<GameObject>("Prefabs/Diamond/diamond");
        GoldMine = Resources.Load<GameObject>("Prefabs/Gold/gold_ingot");
        IronMine = Resources.Load<GameObject>("Prefabs/Iron/iron_ingot");

        DiamondBlock = Resources.Load<GameObject>("Prefabs/DiamondBlock/DiamondBlock");
        GoldBlock = Resources.Load<GameObject>("Prefabs/GoldBlock/GoldBlock");
        IronBlock = Resources.Load<GameObject>("Prefabs/IronBlock/IronBlock");

        BlockScale = 1.0f;
        MineScale = 1.0f;
    }

    public void OreFormation(CompetitionUpdate.Mine.OreType oreType, string mineId, Vector3 orePosition)
    {
        if (oreType == CompetitionUpdate.Mine.OreType.IronOre)
        {
            IronOreFormation(mineId, orePosition);
        }
        else if (oreType == CompetitionUpdate.Mine.OreType.GoldOre)
        {
            GoldOreFormation(mineId, orePosition);
        }
        else if (oreType == CompetitionUpdate.Mine.OreType.DiamondOre)
        {
            DiamondOreFormation(mineId, orePosition);
        }
    }

    void IronOreFormation(string mineId, Vector3 IronOrePosition)
    {
        if (!Controller.Mines.ContainsKey(mineId))
        {
            
            GameObject ironOre = Instantiate(IronBlock, IronOrePosition+ Vector3.one* UnityEngine.Random.Range(-1.0f, 1.0f), Quaternion.identity);
            ironOre.transform.localScale = Vector3.one* BlockScale;
            Controller.Mines.Add(mineId, ironOre);
        }
    }

    

    void GoldOreFormation(string mineId, Vector3 GoldMinePosition)
    {
        if (!Controller.Mines.ContainsKey(mineId))
        {
            GameObject goldOre = Instantiate(GoldBlock, GoldMinePosition, Quaternion.identity);
            goldOre.transform.localScale = Vector3.one* BlockScale;
            Controller.Mines.Add(mineId, goldOre);
        }
    }

    

    void DiamondOreFormation(string mineId, Vector3 DiamondMinePosition)
    {
        if (!Controller.Mines.ContainsKey(mineId))
        {
            GameObject diamondOre = Instantiate(DiamondBlock, DiamondMinePosition, Quaternion.identity);
            diamondOre.transform.localScale = Vector3.one* BlockScale;
            Controller.Mines.Add(mineId, diamondOre);
        }
    }

    public void OreDestroy(string mineId,int playerId)
    {
        Controller.Mines[mineId].transform.Translate((Controller.PlayerSteve[playerId].transform.position) * Time.deltaTime);
        float distance = Vector3.Distance(Controller.Mines[mineId].transform.position, Controller.PlayerSteve[playerId].transform.position);
        if(distance < 0.5f)
        {
            if (!Controller.Mines.ContainsKey(mineId))
            {
                Destroy(Controller.Mines[mineId]);
                Controller.Mines.Remove(mineId);
            }
        }
    }
}

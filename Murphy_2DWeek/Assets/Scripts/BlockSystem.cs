using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSystem : MonoBehaviour
{
    // arrays for blocks
    [SerializeField]
    private Sprite[] solidBlocks;
    [SerializeField]
    private string[] solidNames;


    // arrays for back solid blocks
    [SerializeField]
    private Sprite[] backingBlocks;
    [SerializeField]
    private string[] backingNames;

    // array to store blocks
    public Block[] allBlocks;

    private void Awake()
    {
      // initialize allblocks
      allBlocks = new Block[solidBlocks.Length + backingBlocks.Length];

      // store BlockID
      int newBlockID = 0;

      for (int i = 0; i < solidBlocks.Length; i++)
      {
        allBlocks[newBlockID] = new Block(newBlockID, solidNames[i],solidBlocks[i], true);
        Debug.Log("Solid block: allBlocks[" + newBlockID + "] = " + solidNames[i]);
        newBlockID++;
      }
      for (int i = 0; i < backingBlocks.Length; i++)
      {
        allBlocks[newBlockID] = new Block(newBlockID, backingNames[i], backingBlocks[i], false);
        Debug.Log("Backing block: allBlocks[" + newBlockID + "] = " + backingNames[i]);
        newBlockID++;
      }
    }
}

public class Block
{
   public int BlockID;
   public string blockName;
   public Sprite blockSprite;
   public bool isSolid;

   public Block(int id, string myName, Sprite mySprite, bool amISolid)
   {
     BlockID = id;
     blockName = myName;
     blockSprite = mySprite;
     isSolid = amISolid;

   }
}

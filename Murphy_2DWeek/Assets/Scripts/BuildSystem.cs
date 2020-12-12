using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    // reference to blocksystem script  
    private BlockSystem blockSys;
    // variables for current block type
    private int currentBlockID = 0;
    private Block currentBlock;

    private int selectableBlocksTotal;

    // variables for block pointer system
    private GameObject blockTemplate;
    private SpriteRenderer currentRend;

    // bools for building system
    private bool buildModeOn = false;
    private bool buildBlocked = false;

    [SerializeField]
    private float blockSizeMod;

    // layer mask 
    [SerializeField]
    private LayerMask solidNoBuildLayer;
    [SerializeField]
    private LayerMask backingNoBuildLayer;
    [SerializeField]
    private LayerMask allBlocksLayer;


    
    private void Awake()
    {
        blockSys = GetComponent<BlockSystem>();

    }

    private void Update()
    {
        // keyboard controls
        if (Input.GetKeyDown("e"))
        {
            buildModeOn = !buildModeOn;


            if (blockTemplate != null)
            {
                Destroy(blockTemplate);
            }

            if (currentBlock == null)
            {
                if (blockSys.allBlocks[currentBlockID] != null)
                {
                    currentBlock = blockSys.allBlocks[currentBlockID];
                }
            }

            if (buildModeOn)
            {
                blockTemplate = new GameObject("CurrentBlockTemplate");
                currentRend = blockTemplate.AddComponent<SpriteRenderer>();
                currentRend.sprite = currentBlock.blockSprite;

            }
        }
        if (buildModeOn && blockTemplate != null)
        {
            float newPosX = Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / blockSizeMod) * blockSizeMod;
            float newPosY = Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / blockSizeMod) * blockSizeMod;
            blockTemplate.transform.position = new Vector2(newPosX,newPosY);


            if (Input.GetMouseButtonDown(0) && buildBlocked == false)
            {
                GameObject newBlock = new GameObject(currentBlock.blockName);
                newBlock.transform.position = blockTemplate.transform.position;
                SpriteRenderer newRend = newBlock.AddComponent<SpriteRenderer>();
                newRend.sprite = currentBlock.blockSprite;

                if (currentBlock.isSolid == true)
                {
                    newBlock.AddComponent<BoxCollider2D>();
                    newBlock.layer = 11;
                    newRend.sortingOrder = -10;
                }
                else
                {
                    newBlock.AddComponent<BoxCollider2D>();
                    newBlock.layer = 12;
                    newRend.sortingOrder = -15;
                }
            }
            
        }
    }
}

using UnityEngine;

public class BillboardSpriteGroup : MonoBehaviour
{
    public string groupName;

    
    [SerializeField]
    private int startingSprite;

    [SerializeField]
    private GameObject[] spriteObjects;

    private int currentSprite = 0;

    void Start()
    {
        SwapSpriteToIndex(startingSprite);
    }

    public void NextSprite()
    {
        currentSprite++;
        if (currentSprite >= spriteObjects.Length) currentSprite = 0;

        SwapSpriteToIndex(currentSprite);
    }

    public void SwapSpriteToIndex(int index = 0)
    {
        Debug.Log (index);
        //Make sure the index is in bounds
        if (index < 0 || index > spriteObjects.Length)
            index = 0;

        for (int i = 0; i < spriteObjects.Length; i++)
        {
            spriteObjects[i].SetActive(false);
        
        }

        spriteObjects[index].SetActive(true);

        currentSprite = index;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Change the mouse sprite in game
 */
public class CursorSprite : MonoBehaviour
{
    public static CursorSprite instance;

    [Header("UI")]
    public Sprite cursorSell;
    public Sprite cursorUpgrade;
    public Sprite cursorNormal;

    Texture2D cursorText;

    /*
     * Before the first step
     */
    void Awake()
    {
        instance = this;
    }

    /*
     * First step, set the cursor to basic
     */
    void Start()
    {
        ChangeCursor("");
    }

    /*
     * Change cursor by type desired
     */
    public void ChangeCursor(string cursor)
    {
        if(cursor == "")
        {
            Change(cursorNormal);
        } else if (cursor == "sell")
        {
            Change(cursorSell);
        }
        else if (cursor == "upgrade")
        {
            Change(cursorUpgrade);
        }
    }

    /*
     * Set of the cursor sprite
     */
    void Change(Sprite sprite)
    {
        cursorText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);


        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                              (int)sprite.textureRect.y,
                                              (int)sprite.textureRect.width,
                                              (int)sprite.textureRect.height);

        cursorText.SetPixels(pixels);
        cursorText.Apply();

        Cursor.SetCursor(cursorText, Vector2.zero, CursorMode.Auto);
    }
}

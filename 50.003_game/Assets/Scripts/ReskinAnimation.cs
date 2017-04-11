using System;
using UnityEngine;

public class ReskinAnimation : MonoBehaviour {
    public string spriteSheetName;

    void LateUpdate()
    {

        var subSprites = Resources.LoadAll<Sprite>("Characthers/"+spriteSheetName);
        Debug.Log("Number of sprites: "+subSprites.Length);
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            string spriteName = renderer.sprite.name;
            Debug.Log(spriteName);
            var newSprite = Array.Find(subSprites, item => item.name == spriteName);
            //Debug.Log(newSprite.name);

            if (newSprite)
            {
                renderer.sprite = newSprite;
            }

        }



    }

}

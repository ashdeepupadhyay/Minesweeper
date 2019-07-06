using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elements : MonoBehaviour {
    public bool mine;
    public Sprite[] emptyTextures;
    public Sprite mineTexture;
	// Use this for initialization
	void Start () {
        mine = Random.value < 0.15;
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        Grid.elements[x, y] = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadTextures(int adjacentCount){
        if (mine){
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        }
        else{
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
        }
    }

    public bool IsCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "unexplored";
    }

    public void OnMouseUpAsButton(){
        if (mine){
            Grid.UncoveredMines();
            print("YOU LOSE");
        }
        else{
            Debug.Log("i am clicked");
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            LoadTextures(Grid.AdjacentMines(x, y));
            Grid.FloodFillUncover(x, y, new bool[Grid.w, Grid.h]);
            Debug.Log("i am clicked" + Grid.AdjacentMines(x, y));

            if (Grid.IsFinished())
            {
                Debug.Log("YOU WIN");
            }

        }
    }
}

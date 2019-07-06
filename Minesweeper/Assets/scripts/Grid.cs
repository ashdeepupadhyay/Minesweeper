using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public static int w = 10;
    public static int h = 13;
    public static Elements[,] elements = new Elements[w, h];

    public static void UncoveredMines(){
        foreach(Elements elem in elements){
            if (elem.mine){
                elem.LoadTextures(0);
            }
        }
    }

    public static bool MineAt(int x,int y){
        if (x >= 0 && y >= 0 && x < w && y < w){
            return elements[x, y].mine;
        }
        return false;
    }

    public static int AdjacentMines(int x,int y){
        int count = 0;

        if (MineAt(x, y + 1)){//top
            ++count;
        }
        if (MineAt(x+1, y + 1)){//topright
            ++count;
        }
        if (MineAt(x+1, y)){//right
            ++count;
        }
        if (MineAt(x+1, y - 1)){//bottomRight
            ++count;
        }
        if (MineAt(x, y - 1)){//bottom
            ++count;
        }
        if (MineAt(x-1, y - 1)){//bottomleft
            ++count;
        }
        if (MineAt(x-1, y)){//left
            ++count;
        }
        if (MineAt(x-1, y + 1)){//topleft
            ++count;
        }
        return count;
    }

    public static void FloodFillUncover(int x, int y, bool[,] visited)
    {
        if (x >= 0 && y >= 0 && x < w && y < w)
        {
            if (visited[x, y])
            {
                return;
            }
            visited[x, y] = true;

            elements[x, y].LoadTextures(AdjacentMines(x, y));
            if (AdjacentMines(x, y) > 0)
            {
                return;
            }
            FloodFillUncover(x - 1, y, visited);
            FloodFillUncover(x + 1, y, visited);
            FloodFillUncover(x, y - 1, visited);
            FloodFillUncover(x, y + 1, visited);
        }
    }

    public static bool IsFinished()
    {
        foreach (Elements elem in elements)
        {
            if (elem.IsCovered() && !elem.mine)
            {
                return false;
            }
        }

        return true;
    }
}

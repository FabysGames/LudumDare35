using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map  {
    public int minType1;
    public int minType2;
    public List<int> layout;

    public Map(int minType1, int minType2, List<int> layout)
    {
        this.minType1 = minType1;
        this.minType2 = minType2;
        this.layout = layout;
    }

}

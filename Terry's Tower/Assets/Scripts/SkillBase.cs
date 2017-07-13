using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillBase {

    public int coolDown= 0;
    public int coolDownTemp = 1;
    public string skillName = "";
    
    public double baseStrength = 0;
    public int calculatedStrength = 0;
    public bool selected;
    public int level = 1;
    public double multiplier;

    public SkillBase(int l, int c, string s, double bs, double m)
    {
        level = l;
        coolDown = c;
        skillName = s;        
        baseStrength = bs;       
        multiplier = m;
    }

    
}

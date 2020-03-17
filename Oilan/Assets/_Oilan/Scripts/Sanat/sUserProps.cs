using UnityEngine;

public class sUserProps : MonoBehaviour
{
    public int userID;
    public int starsTotal;
    public int level;
    public int step;
    public int part;
    //{"userID":18,"starsTotal":95,"level":1,"step":1,"part":1}
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

}

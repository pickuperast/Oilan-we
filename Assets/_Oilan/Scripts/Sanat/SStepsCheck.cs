using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SStepsCheck : MonoBehaviour
{
    public List<Oilan.StepButton> steps;
    public int this_level;
    // Start is called before the first frame update
    void Start()
    {
        //level in save data will be only 
        //  > loaded level's step map
        //  or =  loaded level's step map
        //level in save data couldn't be < than loaded level's step
        if (Oilan.SaveGameManager.Instance.mSaveData.level > this_level)
        {
            //open all steps
            foreach (var step_btn in steps)
            {
                step_btn.isUnlocked = true;
                step_btn.UpdateState();
            }
        }
        else
        {
            for (int i = 0; i < steps.Capacity; i++)
            {
                //server responds step parameter starting from 1, but our steps list begins from 0, so (code) 0 = 1 (db)
                if (i + 1 <= Oilan.SaveGameManager.Instance.mSaveData.step)
                {
                    steps[i].isUnlocked = true;
                    steps[i].UpdateState();
                }
                else
                {
                    break;
                }
            }
        }
    }
}

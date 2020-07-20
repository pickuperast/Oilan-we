using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZLimitCameraView : MonoBehaviour
{
    [Serializable]
    public class CameraLimit
    {
        public bool isActive = false;
        public float topLimitC;
        public float bottomLimitC;
        public float leftLimitC;
        public float rightLimitC;
    }

    [Serializable]
    public class PlayerLimit
    {
        public bool isActive = false;
        public float topLimitP;
        public float bottomLimitP;
        public float leftLimitP;
        public float rightLimitP;
    }

    public Transform player;
    public Transform cameraTransform;

    [Header("Camera limit")]
    public CameraLimit[] cameraLimits;

    [Header("Palyer limit")]
    public PlayerLimit[] playerLimits;

    void Update()
    {
        if (cameraLimits.Length == playerLimits.Length)
        {
            for (int i = 0; i < cameraLimits.Length; i++)
            {
                if (cameraLimits[i].isActive)
                {
                    cameraTransform.transform.position = new Vector3
                    (
                        Mathf.Clamp(cameraTransform.transform.position.x, cameraLimits[i].leftLimitC, cameraLimits[i].rightLimitC),
                        Mathf.Clamp(cameraTransform.transform.position.y, cameraLimits[i].bottomLimitC, cameraLimits[i].topLimitC),
                        cameraTransform.transform.position.z
                    );
                }

                if (playerLimits[i].isActive)
                {
                    player.transform.position = new Vector3
                    (
                        Mathf.Clamp(player.transform.position.x, playerLimits[i].leftLimitP, playerLimits[i].rightLimitP),
                        Mathf.Clamp(player.transform.position.y, playerLimits[i].bottomLimitP, playerLimits[i].topLimitP),
                        player.transform.position.z
                    );
                }
            }
        }
        else
        {
     
                Debug.Log("CameraLimits and PlayerLimits must be equal to each other");
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < cameraLimits.Length; i++)
        {
            Gizmos.DrawLine(new Vector2(cameraLimits[i].leftLimitC, cameraLimits[i].topLimitC),
                new Vector2(cameraLimits[i].rightLimitC, cameraLimits[i].topLimitC));
            Gizmos.DrawLine(new Vector2(cameraLimits[i].rightLimitC, cameraLimits[i].topLimitC),
                new Vector2(cameraLimits[i].rightLimitC, cameraLimits[i].bottomLimitC));
            Gizmos.DrawLine(new Vector2(cameraLimits[i].rightLimitC, cameraLimits[i].bottomLimitC),
                new Vector2(cameraLimits[i].leftLimitC, cameraLimits[i].bottomLimitC));
            Gizmos.DrawLine(new Vector2(cameraLimits[i].leftLimitC, cameraLimits[i].bottomLimitC),
                new Vector2(cameraLimits[i].leftLimitC, cameraLimits[i].topLimitC));


            Gizmos.DrawLine(new Vector2(playerLimits[i].leftLimitP, playerLimits[i].topLimitP),
                new Vector2(playerLimits[i].rightLimitP, playerLimits[i].topLimitP));
            Gizmos.DrawLine(new Vector2(playerLimits[i].rightLimitP, playerLimits[i].topLimitP),
                new Vector2(playerLimits[i].rightLimitP, playerLimits[i].bottomLimitP));
            Gizmos.DrawLine(new Vector2(playerLimits[i].rightLimitP, playerLimits[i].bottomLimitP),
                new Vector2(playerLimits[i].leftLimitP, playerLimits[i].bottomLimitP));
            Gizmos.DrawLine(new Vector2(playerLimits[i].leftLimitP, playerLimits[i].bottomLimitP),
                new Vector2(playerLimits[i].leftLimitP, playerLimits[i].topLimitP));
        }
    }

    IEnumerator ChangeActiveBorderCoroutine()
    {
        yield return new WaitForEndOfFrame();
    }


    public void ChangeActiveBorder(int borderIndex)
    {
        StartCoroutine(ChangeActiveBorderCoroutine());

        for(int i = 0; i < cameraLimits.Length; i++)
        {
            if(i == borderIndex)
            {
                cameraLimits[i].isActive = true;
                playerLimits[i].isActive = true;
                continue;
            }

            cameraLimits[i].isActive = false;
            playerLimits[i].isActive = false;
        }
    }
}

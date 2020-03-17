using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class SCreateAvatar : MonoBehaviour
{
    /*
    string name = "Ali_rigged_avatar";
    void Start()
    {
        GameObject activeGameObject = Selection.activeGameObject;

        if (activeGameObject != null &&
            activeGameObject.GetComponent<Animator>() != null)
        {
            Avatar avatar = AvatarBuilder.BuildGenericAvatar(activeGameObject, "");
            avatar.name = name;
            Debug.Log(avatar.isHuman ? "is human" : "is generic");

            Animator animator = activeGameObject.GetComponent<Animator>() as Animator;
            animator.avatar = avatar;
            AssetDatabase.CreateAsset(avatar, "Assets/" + name + ".asset");
            //CreateAvatar(avatar);
        }
    }

    static void CreateAvatar(Avatar _ava)
    {
        // Create a simple material asset
        
        //Material material = new Material(Shader.Find("Specular"));
        AssetDatabase.CreateAsset(_ava, "Assets/MyMaterial.mask");

        // Print the path of the created asset
        Debug.Log(AssetDatabase.GetAssetPath(_ava));
    }
    */
}
/*
using UnityEditor;
using UnityEngine;

namespace Infrastructure.Editor
{
    public class SCreateAvatar
    {
        [MenuItem("CustomTools/MakeAvatar")]
        private static void MakeAvatarMask()
        {
            GameObject activeGameObject = Selection.activeGameObject;

            if (activeGameObject != null)
            {
                Avatar avatar = AvatarBuilder.BuildGenericAvatar(activeGameObject, "");
                avatar.name = "InsertYourName";
                Debug.Log(avatar.isHuman ? "is human" : "is generic");

                AssetDatabase.CreateAsset(avatar, "Assets/NewAvatar.asset");
            }
        }
    }
}
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Net;
using System.Text;

public class user
{
    public int key;
}

public class TestConnection : MonoBehaviour
{
    string get_link = "https://script.google.com/macros/s/AKfycbwykeHz5VsG8tPh3fs2mTknmKFxVIOQidRLDDhas19qOlvijq8h/exec";
    public Text DebugArea;
    //public InputField inputF;
    public TMP_InputField inputF;

    public void GetData()
    {
        string total_link = get_link + "?" + "action=" + "getData" + "&" + "id=1";
        if (inputF.text != "") {
            total_link = inputF.text;
        }
        StartCoroutine(Get(total_link));
    }

    IEnumerator Get(string link) {
        using (UnityWebRequest www = UnityWebRequest.Get(link))
        {
            www.SendWebRequest();
            while (!www.isDone)
            {
                yield return null;
            }
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Debug: ошибка - " + www.error);
                DebugArea.text = www.error;
            }
            else
            {
                Debug.Log("Debug: получили ответ " + www.downloadHandler.text);
                DebugArea.text = www.downloadHandler.text;
                //decode_double_rows(www.downloadHandler.text);
            }
        }

    }
    IEnumerator GetTest(string link)
    {
        var request = new UnityWebRequest(link, "GET");
        user JSONreq = new user {
            key = 8
        };
        string jsonStr = JsonUtility.ToJson(JSONreq);
        Debug.Log("jsonStr: " + jsonStr);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStr);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        Debug.Log("request: " + request);

        yield return request.Send();
        Debug.Log("Status Code: " + request.responseCode);
        while (!request.isDone)
        {
            yield return null;
        }
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("Debug: ошибка - " + request.error);
            DebugArea.text = request.error;
        }
        else
        {
            Debug.Log("Debug: получили ответ " + request.downloadHandler.text);
            DebugArea.text = request.downloadHandler.text;
            //decode_double_rows(request.downloadHandler.text);
        }
        /*
        using (UnityWebRequest www = UnityWebRequest.Get(link))
        {
            www.SendWebRequest();
            while (!www.isDone) {
                yield return null;
            }
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Debug: ошибка - " + www.error);
                DebugArea.text = www.error;
            }
            else
            {
                Debug.Log("Debug: получили ответ "+www.downloadHandler.text);
                DebugArea.text = www.downloadHandler.text;
                //decode_double_rows(www.downloadHandler.text);
            }
        }
        */
    }
}

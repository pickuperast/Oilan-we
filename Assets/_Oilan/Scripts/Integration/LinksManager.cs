using UnityEngine;

namespace Oilan
{

    public class LinksManager : MonoBehaviour
    {
        public static LinksManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void OpenLink(string linkTarget)
        {
            string newURL = "";

            switch (linkTarget)
            {
                case "url":
                    newURL = GameManager.Instance.InitDataObj.Links.urlAndroid;
                    break;
                case "urlAndroid":
                    newURL = GameManager.Instance.InitDataObj.Links.urlAndroid;
                    break;
                default:
                    break;
            }

            OpenURL(newURL);
        }

        public void RateLink()
        {
            OpenLink("url");
        }

        private void OpenURL(string url)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                OpenURLJSPlugin(url);
            }
            else
            {
                Debug.Log("Try open url: " + url);
                Application.OpenURL(url);
            }
        }

        public void OpenURLJSPlugin(string url)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
		//openWindow(url);
#endif
        }

#if UNITY_WEBGL
        //[DllImport("__Internal")]
        //private static extern void openWindow(string url);
#endif

    }
}
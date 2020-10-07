using UnityEngine;
using System.Collections;
using System;

namespace Oilan
{
    /// <summary>
    /// Holds initial data settings
    /// </summary>
    public class InitData
    {
        private readonly LinksData links;

        /// <summary>
        /// Return LinksData obj
        /// </summary>
        public LinksData Links { get { return links; } }
        
        /// <summary>
        /// Initialize initial settings
        /// </summary>
        public InitData()
        {
            TextAsset txtLinksData = Resources.Load(Constants._pathLinksData) as TextAsset;

            if (txtLinksData != null)
                links = JsonUtility.FromJson<LinksData>(txtLinksData.text);

            else
                Debug.LogError("Couldn't find file at path AppResources/" + Constants._pathLinksData);

        }
    }

    /// <summary>
    /// Holds links data settings
    /// </summary>
    [Serializable]
    public class LinksData
    {
        // Stores links
        /// <summary>
        /// Link to android app
        /// </summary>
        public string urlAndroid;

    }

}
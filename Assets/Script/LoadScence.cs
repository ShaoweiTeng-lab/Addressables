using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class LoadScence : MonoBehaviour
{//只放入單一場景 其餘場景用下載的 減少容量
    public string m_SceneAddressToLoad;

    public void LoadGameplayScene()
    {
        Addressables.LoadSceneAsync(m_SceneAddressToLoad, UnityEngine.SceneManagement.LoadSceneMode.Single).Completed += (obj)=>{ Debug.Log("進入主場景"); };
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=0tFnqBdO7NY
//https://z0935323866.medium.com/unity-%E5%B0%8B%E5%9D%80%E5%BC%8F%E8%B3%87%E6%BA%90%E7%AE%A1%E7%90%86%E7%B3%BB%E7%B5%B1addressable-assets-system-%E4%B8%80-bb1e99014a88
//https://z0935323866.medium.com/unity-%E5%B0%8B%E5%9D%80%E5%BC%8F%E8%B3%87%E6%BA%90%E7%AE%A1%E7%90%86%E7%B3%BB%E7%B5%B1addressable-assets-system-%E4%BA%8C-eb0e302fd4db
//https://zhuanlan.zhihu.com/p/94572467
/*
BuildTarget : 要建立的平台位置，這裡基本上不用到到，它會自動根據我們在BuildSetting設定的平台建立相對應的資料夾。
LocalBuildPath、LocalLoadPath : 在本地端要儲存與載入的路徑。
RemoteBuildPath、RemoteLoadPath : 在遠端儲存與載入的路徑。

使用遠端測試 :PlayModfeScript -> Use Existing Build ,遠端模式必須要先build出一個bundle檔案出來才能測試。
 */

public class Manager : MonoBehaviour
{
    [SerializeField]
    Button LoadObjBtn;
    IList<GameObject> allList;
    [SerializeField]
    string AddressNameStr;
    [SerializeField] AssetReference assetobject;
    [SerializeField] AssetLabelReference assetLabelobject;//所有label一致的物件
    [SerializeField] GameObject assetObj;
    
    bool isLoadSucc;
    //流程 : 異步加載資源 Completed， 
    void Start()
    {
        LoadObjBtn.interactable = false; 
        isLoadSucc = false;
        //Completed代表加載完資源後執行方法
        //使用url方式
        //Addressables.LoadAssetAsync<GameObject>(AddressNameStr).Completed += OnAssetObjLoaded;// 先異步載,addressNameStr則是要填入是剛剛傳到Addressables Gruops的prefab位址名稱。 載完後執行OnAssetObjLoaded

        //使用Reference 方式
        //assetobject.LoadAssetAsync<GameObject>().Completed += OnAssetObjLoaded;//當前資源載完執行 

        //AssetLabelReference 方式
        Addressables.LoadAssetAsync<GameObject>(assetLabelobject).Completed += OnAssetObjLoaded;
        //使用 async
        //AsyncInstanctiate();
        LoadObjBtn.onClick.AddListener(CreateObjBtn);
    }

    void OnAssetObjLoaded(AsyncOperationHandle<GameObject> asyncOperationHandle)//異步加載(一般來說如果在本地載入是不需要異步加載的，但是實際上我們不會知道物件是從本地還是遠端載入，所以乾脆都用異步)
    {   //加載完之後會回傳一個帶有AsyncOperationHandle<GameObject>的事件給OnAssetObjLoaded
        LoadObjBtn.interactable = true;

        isLoadSucc = true;//判斷加載結束
        assetObj = asyncOperationHandle.Result;//將載入好的物件存到AssetObj等待被使用。
    }
    //void OnAssetObjLoaded(AsyncOperationHandle<IList<GameObject>> asyncOperationHandle)//異步加載(一般來說如果在本地載入是不需要異步加載的，但是實際上我們不會知道物件是從本地還是遠端載入，所以乾脆都用異步)
    //{   //加載完之後會回傳一個帶有AsyncOperationHandle<GameObject>的事件給OnAssetObjLoaded
    //    LoadObjBtn.interactable = true;
    //    isLoadSucc = true;//判斷加載結束
    //    allList = asyncOperationHandle.Result;//將載入好的物件存到AssetObj等待被使用。
    //}

    void CreateObjBtn()
    {
        Vector3 pos = new Vector3(Random.Range(5, -5), Random.Range(4, -4), 0);
        var obj= Instantiate(assetObj, pos, Quaternion.identity);//使用url 或label
        obj.GetComponent<Attack>().Atk();
        //assetobject.Instantiate(pos, Quaternion.identity);//AssetReference內部加載
         
    }
    /// <summary>
    /// 使用 async task家載
    /// </summary>
    async void AsyncInstanctiate()
    {
        assetObj = await assetobject.LoadAssetAsync<GameObject>().Task ;
        LoadObjBtn.interactable = true;

        isLoadSucc = true;//判斷加載結束
        


    }
}
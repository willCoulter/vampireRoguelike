using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardManager : MonoBehaviour
{
    public GameObject prefab;
    private List<GraveyardSaveData> saveDatas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Populate()
    {
        saveDatas = SaveSystem.LoadGraveyardSaves();

        GameObject newItem;

        foreach(GraveyardSaveData saveData in saveDatas)
        {
            newItem = Instantiate(prefab, transform);

            GraveyardItem itemScript = newItem.GetComponent<GraveyardItem>();

            itemScript.UpdateSave(saveData);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainScene : MonoBehaviour
{
    public static UIMainScene Instance { get; private set; }
    
    public interface IUIInfoContent
    {
        string GetName();
        string GetData();
        void GetContent(ref List<Building.InventoryEntry> content);
    }
    
    public InfoPopup InfoPopup;
    public ResourceDatabase ResourceDB;

    protected IUIInfoContent m_CurrentContent;
    protected List<Building.InventoryEntry> m_ContentBuffer = new List<Building.InventoryEntry>();


    private void Awake()
    {
        Instance = this;
        InfoPopup.gameObject.SetActive(false);
        ResourceDB.Init();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Update()
    {
        if (m_CurrentContent == null)
            return;
        
        //This is not the most efficient, as we reconstruct everything every time. A more efficient way would check if
        //there was some change since last time (could be made through a IsDirty function in the interface) or smarter
        //update (match an entry content ta type and just update the count) but simplicity in this tutorial we do that
        //every time, this won't be a bottleneck here. 

        InfoPopup.Data.text = m_CurrentContent.GetData();
        
        InfoPopup.ClearContent();
        m_ContentBuffer.Clear();
        
        m_CurrentContent.GetContent(ref m_ContentBuffer);
        foreach (var entry in m_ContentBuffer)
        {
            Sprite icon = null;
            if (ResourceDB != null)
                icon = ResourceDB.GetItem(entry.ResourceId)?.Icone;
            
            InfoPopup.AddToContent(entry.Count, icon);
        }
    }

    public void SetNewInfoContent(IUIInfoContent content)
    {
        if (content == null)
        {
            InfoPopup.gameObject.SetActive(false);
        }
        else
        {
            InfoPopup.gameObject.SetActive(true);
            m_CurrentContent = content;
            InfoPopup.Name.text = content.GetName();
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for building on the map that hold a Resource inventory and that can be interacted with by Unit.
/// This Base class handle modifying the inventory of resources.
/// </summary>
public abstract class Building : MonoBehaviour,
    UIMainScene.IUIInfoContent
{
    //need to be serializable for the save system, so maybe added the attribute just when doing the save system
    [System.Serializable]
    public class InventoryEntry
    {
        public string ResourceId;
        public int Count;
    }

    [Tooltip("-1 is infinite")]
    public int InventorySpace = -1;
    
    protected List<InventoryEntry> m_Inventory = new List<InventoryEntry>();
    public List<InventoryEntry> Inventory => m_Inventory;

    protected int m_CurrentAmount = 0;

    //return 0 if everything fit in the inventory, otherwise return the left over amount
    public int AddItem(string resourceId, int amount)
    {
        //as we use the shortcut -1 = infinite amount, we need to actually set it to max value for computation following
        int maxInventorySpace = InventorySpace == -1 ? Int32.MaxValue : InventorySpace;
        
        if (m_CurrentAmount == maxInventorySpace)
            return amount;

        int found = m_Inventory.FindIndex(item => item.ResourceId == resourceId);
        int addedAmount = Mathf.Min(maxInventorySpace - m_CurrentAmount, amount);
        
        //couldn't find an entry for that resource id so we add a new one.
        if (found == -1)
        {
            m_Inventory.Add(new InventoryEntry()
            {
                Count = addedAmount,
                ResourceId = resourceId
            });
        }
        else
        {
            m_Inventory[found].Count += addedAmount;
        }

        m_CurrentAmount += addedAmount;
        return amount - addedAmount;
    }

    //return how much was actually removed, will be 0 if couldn't get any.
    public int GetItem(string resourceId, int requestAmount)
    {
        int found = m_Inventory.FindIndex(item => item.ResourceId == resourceId);
        
        //couldn't find an entry for that resource id so we add a new one.
        if (found != -1)
        {
            int amount = Mathf.Min(requestAmount, m_Inventory[found].Count);
            m_Inventory[found].Count -= amount;

            if (m_Inventory[found].Count == 0)
            {//no more of that resources, so we remove it
                m_Inventory.RemoveAt(found);
            }

            m_CurrentAmount -= amount;

            return amount;
        }

        return 0;
    }

    public virtual string GetName()
    {
        return gameObject.name;
    }

    public virtual string GetData()
    {
        return "";
    }

    public void GetContent(ref List<InventoryEntry> content)
    {
        content.AddRange(m_Inventory);
    }
}

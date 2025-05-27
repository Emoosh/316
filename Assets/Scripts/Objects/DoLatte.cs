using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoLatte: Functionality, IPutItemFull
{
    [SerializeField] private List<ObjectnType> itemsToHold = new List<ObjectnType>();
    private ItemType currentType;
    private ItemType waitingItem; // Yeni eklendi

    private void Start()
    {
        currentType = ItemType.NONE;
        waitingItem = ItemType.NONE;
        timer.gameObject.SetActive(false);
    }

    public override ItemType Process()
    {
        if (currentType == ItemType.NONE) return ItemType.NONE;
        if (processStarted == true && timer.gameObject.activeSelf == false)
        {
            timer.gameObject.SetActive(true);
        }
        processStarted = true;
        currentTime += Time.deltaTime;
        timer.UpdateClock(currentTime, maxTime);
        if (currentTime >= maxTime)
        {
            currentTime = 0;
            timer.gameObject.SetActive(false);
            processStarted = false;
            timer.UpdateClock(currentTime, maxTime);
            switch (currentType)
            {
                case ItemType.MILKCUP:
                    return ItemType.LATTE;
                case ItemType.MILK:
                    return ItemType.MILKCUP;
                case ItemType.GROUNDCOFFEE:
                    return ItemType.ESPRESSOMODEL;
                case ItemType.COFFEE1:
                    return ItemType.GROUNDCOFFEE;
                case ItemType.TOMATO:
                    return ItemType.SLICEDTOM;
                case ItemType.LETTUCE:
                    return ItemType.SLICEDLET;
                case ItemType.ONION:
                    return ItemType.SLICEDON;
                case ItemType.CHEESE:
                    return ItemType.SLICEDCHE;
                case ItemType.BREAD:
                    return ItemType.SLICEDBREAD;
            }
        }
        return ItemType.NONE;
    }

    public override void ClearObject()
    {
        base.ClearObject();
        currentType = ItemType.NONE;
        waitingItem = ItemType.NONE;
        itemsToHold.ForEach(obj => obj.item.SetActive(false));
    }

    public bool PutItem(ItemType item)
    {
        if (!FilterItem(item)) return false;

        if (waitingItem == ItemType.NONE)
        {
            waitingItem = item;
            ShowItem(item);
            return true;
        }

        if (waitingItem != ItemType.NONE && currentType == ItemType.NONE)
        {
            currentType = CombineItems(waitingItem, item);
            waitingItem = ItemType.NONE;
            ShowItem(currentType);
            return true;
        }

        return false;
    }

    private bool FilterItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.MILKCUP:
            case ItemType.MILK:
            case ItemType.GROUNDCOFFEE:
            case ItemType.COFFEE1:
            case ItemType.TOMATO:
            case ItemType.LETTUCE:
            case ItemType.ONION:
            case ItemType.CHEESE:
            case ItemType.BREAD: return true;
            default: return false;
        }
    }

    private ItemType CombineItems(ItemType first, ItemType second)
    {
        if ((first == ItemType.MILKCUP && second == ItemType.ESPRESSOMODEL) ||
            (first == ItemType.ESPRESSOMODEL && second == ItemType.MILKCUP))
        {
            return ItemType.LATTE;
        }

        return first;
    }

    private void ShowItem(ItemType type)
    {
        foreach (ObjectnType itemHold in itemsToHold)
        {
            itemHold.item.SetActive(itemHold.type == type);
        }
    }
}

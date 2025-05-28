using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoLatte : Functionality, IPutItemFull
{
    [SerializeField] private GameObject espressoObj;
    [SerializeField] private GameObject milkcupObj;

    private ItemType firstItem = ItemType.NONE;
    private ItemType secondItem = ItemType.NONE;
    private bool espressoPlaced = false;
    private bool processStarted = false;

    private void Start()
    {
        firstItem = ItemType.NONE;
        secondItem = ItemType.NONE;
        if (timer != null) timer.gameObject.SetActive(false);

        // Başlangıçta görselleri kapat
        if (espressoObj != null) espressoObj.SetActive(false);
        if (milkcupObj != null) milkcupObj.SetActive(false);
    }

    public bool PutItem(ItemType item)
    {
        if (processStarted) return false;

        if (!espressoPlaced && item == ItemType.ESPRESSOMODEL)
        {
            firstItem = item;
            espressoPlaced = true;
            if (espressoObj != null) espressoObj.SetActive(true);
            return true;
        }
        else if (espressoPlaced && item == ItemType.MILKCUP)
        {
            secondItem = item;
            if (milkcupObj != null) milkcupObj.SetActive(true);
            processStarted = true;
            StartCoroutine(MakeLatte());
            return true;
        }

        return false;
    }

    private IEnumerator MakeLatte()
    {
        if (timer != null) timer.gameObject.SetActive(true);

        float currentTime = 0;
        float maxTime = 2f;
        while (currentTime < maxTime)
        {
            currentTime += Time.deltaTime;
            if (timer != null) timer.UpdateClock(currentTime, maxTime);
            yield return null;
        }

        if (timer != null) timer.gameObject.SetActive(false);

        // Latte oluştur
        if (GameObject.FindObjectOfType<Inventory>() != null)
        {
            Inventory inv = GameObject.FindObjectOfType<Inventory>();
            inv.TakeItem(ItemType.LATTE);
        }

        // Görselleri kapat
        if (espressoObj != null) espressoObj.SetActive(false);
        if (milkcupObj != null) milkcupObj.SetActive(false);

        // Reset
        espressoPlaced = false;
        processStarted = false;
        firstItem = ItemType.NONE;
        secondItem = ItemType.NONE;
    }

    public override ItemType Process()
    {
        return ItemType.NONE; // burada bir şey dönülmeyecek çünkü coroutine ile işlem yapılıyor
    }

    public override void ClearObject()
    {
        // Latte üretildikten sonra çağrılabilir
        firstItem = ItemType.NONE;
        secondItem = ItemType.NONE;
        espressoPlaced = false;
        processStarted = false;

        if (espressoObj != null) espressoObj.SetActive(false);
        if (milkcupObj != null) milkcupObj.SetActive(false);
    }
}

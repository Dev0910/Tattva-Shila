using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] Sprite temp;
    [SerializeField] Resource[] resources;
    void Start()
    {
        foreach (Resource r in resources)
        {
            UpdateButtons(r.element);
            UpdateText(r.element);
        }
    }

    public void AddElement(EElements _element)
    {
        GetResourceOf(_element).amount++;
        UpdateText(_element);
        UpdateButtons(_element);
    }
    public bool TakeElementIfPossible(EElements _element)
    {
        Resource r = GetResourceOf(_element);
        if (r.amount > 0)
        {
            r.amount--;
            UpdateText(_element);
            UpdateButtons(_element);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void GetRandomStone()
    {
        int r = Random.Range(0, resources.Length);
        AddElement(resources[r].element);
    }
    void UpdateButtons(EElements _element)
    {
        Resource r = GetResourceOf(_element);
        if (r.amount <= 0)
        {
            r.button.interactable = false;
        }
        else
        {
            r.button.interactable = true;
        }
    }
    void UpdateText(EElements _element)
    {
        GetResourceOf(_element).displayText.text = GetResourceOf(_element).amount.ToString();
    }
    Resource GetResourceOf(EElements _element)
    {
        foreach (Resource r in resources)
        {
            if (r.element == _element)
            {
                return r;
            }
        }
        return null;
    }
    public Sprite GetStoneSprite(EElements _element)
    {
        return GetResourceOf(_element).sprite;
    }
}
[System.Serializable]
public class Resource
{
    public EElements element;
    public int amount;
    public Button button;
    public TMP_Text displayText;
    public Sprite sprite;
}

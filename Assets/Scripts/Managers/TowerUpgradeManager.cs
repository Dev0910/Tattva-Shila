using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeManager : MonoBehaviour
{
    private EElements elementInHand = EElements.None;
    public CustomCurser customCurser;
    [Header("Buttons")]
    [SerializeField] Button airButton;
    [SerializeField] Button earthButton;
    [SerializeField] Button fireButton;
    [SerializeField] Button waterButton;
    void Start()
    {
        airButton.onClick.AddListener(SelectAirStone);
        earthButton.onClick.AddListener(SelectEarthStone);
        fireButton.onClick.AddListener(SelectFireStone);
        waterButton.onClick.AddListener(SelectWaterStone);
        airButton.transform.parent.DOLocalMoveX(890, 0.5f).SetEase(Ease.OutSine);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && elementInHand != EElements.None)
        {
            // Get the world position of the mouse click
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Perform a raycast at the mouse position
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent<Tower>(out Tower tower))
                {
                    if (CheckIfStoneCombinationPossible(tower) && GameManager.instance.resourceManager.TakeElementIfPossible(elementInHand))
                    {
                        tower.AddElement(elementInHand);
                        DeselectElement();
                    }
                }
            }
        }
        //deselect the Building on Right mouse click
        if (Input.GetMouseButtonDown(1))
        {
            DeselectElement();
        }
    }
    public void SelectFireStone()
    {
        elementInHand = EElements.Fire;
        SelectElement();
    }
    public void SelectAirStone()
    {
        elementInHand = EElements.Air;
        SelectElement();
    }
    public void SelectEarthStone()
    {
        elementInHand = EElements.Earth;
        SelectElement();
    }
    public void SelectWaterStone()
    {
        elementInHand = EElements.Water;
        SelectElement();
    }
    void SelectElement()
    {
        customCurser.gameObject.SetActive(true);

        customCurser.SetSprite(GameManager.instance.resourceManager.GetStoneSprite(elementInHand));
        Cursor.visible = false;
    }
    void DeselectElement()
    {
        elementInHand = EElements.None;
        customCurser.gameObject.SetActive(false);
        Cursor.visible = true;
    }
    bool CheckIfStoneCombinationPossible(Tower tower)
    {
        switch (tower.GetTowerType())
        {
            case ETowerType.Base:
                {
                    return true;
                }

            case ETowerType.Air:
                {
                    if (elementInHand == EElements.Air)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            case ETowerType.Water:
                {
                    if (elementInHand == EElements.Water)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            case ETowerType.Earth:
                {
                    if (elementInHand == EElements.Earth)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            case ETowerType.Fire:
                {
                    if (elementInHand == EElements.Fire)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            default:
                return false;
        }

    }
}

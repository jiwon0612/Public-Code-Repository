using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PotionUI : UIToolkitParents
{
    [SerializeField] List<Sprite> potionSprites;
    [SerializeField] private float potionDuration;
    List<StyleBackground> _potionUIImages;
    private List<VisualElement> _potions;

    private int _maxPotionCount = 3;
    private int _currentPotionCount = 3;

    private Health _playerHealth;

    protected override void OnEnable()
    {
        base.OnEnable();
        _potions = Root.Query<VisualElement>("Potion").ToList();

        _potionUIImages = new List<StyleBackground>();
        foreach (var sprite in potionSprites)
        {
            _potionUIImages.Add(new StyleBackground(sprite));
        }

        _playerHealth = GameObject.Find("Player").GetComponent<Health>();
        _playerHealth.OnHealing += UsePotion;

        ResetPotions();
    }

    private void ResetPotions()
    {
        _currentPotionCount = _maxPotionCount;
        foreach (var potion in _potions)
        {
            potion.style.backgroundImage = _potionUIImages[1];
        }
    }

    private void UsePotion()
    {
        if (_currentPotionCount <= 0) return;

        _currentPotionCount--;
        _potions[_currentPotionCount].style.backgroundImage = _potionUIImages[0];
        StartCoroutine(PotionMove(_potions[_currentPotionCount]));
    }
    IEnumerator PotionMove(VisualElement potion)
    {
        potion.AddToClassList("down");
        yield return new WaitForSeconds(potionDuration);
        potion.RemoveFromClassList("down");
    }
}
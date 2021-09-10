using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controls the Hearts UI 
 */

public class EnemyHeartManager : MonoBehaviour
{
    public SpriteRenderer[] hearts;
    public FloatValue heartContainers;
    public FloatValue healthPerContainer;
    public Enemy enemy;

    // Sprtites for each heart state
    public Sprite fullHeart;
    public Sprite threeQuartersHeart;
    public Sprite halfHeart;
    public Sprite oneQuarterHeart;
    public Sprite emptyHeart;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
        UpdateHearts();
    }

    private void OnEnable()
    {
        UpdateHearts();
    }

    // All hearts[] have to start turned off, they are set active in this function
    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.runtimeValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    // Flexible way to support multiple amount of HP per heart container
    // Currently supports 1, 2 and 4, to add new amounts need to add the sprites and update SetDecimalHeart function
    // No need to update this one :)
    public void UpdateHearts()
    {
        for (int i = 0; i < heartContainers.runtimeValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            if (enemy.health >= (i + 1) * healthPerContainer.runtimeValue)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (enemy.health <= i * healthPerContainer.runtimeValue)
            {
                hearts[i].gameObject.SetActive(false);
            }
            else // "decimal hearts"
            {
                hearts[i].sprite = SetDecimalHeart(enemy.health % healthPerContainer.runtimeValue, healthPerContainer.runtimeValue);
            }
        }
    }

    // Decided to use nested if/else to avoid comparing float point values
    private Sprite SetDecimalHeart(float health, float healthPerContainer)
    {
        // Two HP per heart
        if (healthPerContainer == 2)
        {
            if (health == 1) return halfHeart;
        }

        // Four HP per heart
        else if (healthPerContainer == 4)
        {
            if (health == 1) return oneQuarterHeart;
            if (health == 2) return halfHeart;
            if (health == 3) return threeQuartersHeart;
        }

        // Deafult case, shouldn't get to this point
        throw new System.Exception("Decimal heart value invalid");
    }
}

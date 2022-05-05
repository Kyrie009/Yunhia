using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Used for coins, health, inventory items, and even ammo if you want to create a gun shooting mechanic!*/

public class Collectable : GameBehaviour
{
    enum ItemType { InventoryItem, Health, Mana, Ammo }; //Creates an ItemType category
    [SerializeField] ItemType itemType; //Allows us to select what type of item the gameObject is in the inspector
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bounceSound;
    [SerializeField] private AudioClip[] collectSounds;
    [SerializeField] private int itemAmount;
    [SerializeField] private string itemName; //If an inventory item, what is its name?
    //[SerializeField] private Sprite UIImage; //What image will be displayed if we collect an inventory item?

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Collect();
        }

        //(Expand if there is a fallzone) Collect me if I trigger with an object tagged "Death Zone", aka an area the player can fall to certain death

    }

    public void Collect()
    {
        Debug.Log("1");
        if (itemType == ItemType.Health)
        {
            _P.PlayerGains(itemAmount,0,0);
        }
        else if (itemType == ItemType.Mana)
        {
            _P.PlayerGains(0, itemAmount, 0);
        }

        _UI.audioSource.PlayOneShot(collectSounds[Random.Range(0, collectSounds.Length)], Random.Range(.6f, 1f));

        Destroy(gameObject);

    }
}

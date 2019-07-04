using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private readonly string itemName;

    private int brawnChange;
    private int skillChange;
    private int techChange;
    private int charmChange;

    //Need to add in inventory handling

    public Item()
    {
        itemName = "Default";

        brawnChange = 0;
        skillChange = 0;
        techChange = 0;
        charmChange = 0;
    }

    public Item(string newItemName)
    {
        itemName = newItemName;

        determineItemScores();
    }

    /// <summary>
    /// 
    /// Determines the changes in spec scores for the item based on its name. If the item name does not exist, item will have no change in spec scores assigned
    /// 
    /// </summary>
    private void determineItemScores()
    {
        //Resets spec scores to default of no change
        brawnChange = 0;
        skillChange = 0;
        techChange = 0;
        charmChange = 0;

        switch (itemName)
        {
            case "Boxing Gloves":
                brawnChange = 1;
                break;
            case "Speed Boots":
                skillChange = 1;
                break;
            case "Robo-Brain":
                techChange = 1;
                break;
            case "Dazzling Outfit":
                charmChange = 1;
                break;
            case "Fancy Glasses":
                techChange = 2;
                charmChange = 1;
                break;
            case "Extra Arms":
                brawnChange = 1;
                skillChange = 2;
                break;
            case "Hyper Fuel":
                brawnChange = 2;
                techChange = 1;
                break;
            case "Spy Suit":
                skillChange = 1;
                charmChange = 2;
                break;
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice
{
    public int choiceID;
    //Name which appears at top of choice for player
    public string choiceName;

    //Weighting of choice appearing in randomisation. Default value is 1. Less than 1 means less likely. More than 1 means more likely
    public float weighting;

    //Whether the choice can appear more than once. If true cannot appear more than once. Can otherwise.
    //If mandatory is not 0 and unique is true, then choice cannot appear more than mandatory number of times
    public bool unique;

    //Number of times the choice must occur in the game
    public int mandatory;

    //Whether the choice is disabled after it has been selected. Disabled choices can be reselected if the item is discarded.
    public bool oneOff;
    //Whether the choice has been selected and, if it is a oneoff, has been disabled. Should only be true if oneOff is also true.
    public bool isDisabled;

    //Whether choice is a spec challenge, what type of challenge it is and its associated target score.
    //Spec Challenge is "Null" if not a spec challenge and target score will be 0 (this will throw math errors if passed into spec challenge formula)
    public string specChallenge;
    public int targetScore;

    //Outcomes of the choice. If choice is a spec challenge, these will be the outcomes of a successful spec challenge
    public int scrapChange;
    public int corruptionChange;
    public int powerChange;
    public Item specItem;
    public int lifeChange;
    public bool component;

    //Only required if choice is a spec challenge. 0 otherwise.
    //Outcomes of a failed spec challenge
    public int corruptionFail;
    public int lifeFail;

    //Determination of which rooms choice can appear in
    public bool inBar;
    public bool inDining;
    public bool inEngineering;
    public bool inKitchen;
    public bool inSleeping;
    public bool inSpa;

    public Choice()
    {
        choiceID = 0;
        choiceName = "Default";

        weighting = 1;
        unique = false;
        mandatory = 0;
        oneOff = false;
        isDisabled = false;

        specChallenge = "Null";
        targetScore = 0;

        scrapChange = 0;
        corruptionChange = 0;
        powerChange = 0;
        specItem = new Item();
        lifeChange = 0;
        component = false;

        corruptionFail = 0;
        lifeFail = 0;

        inBar = true;
        inDining = true;
        inEngineering = true;
        inKitchen = true;
        inSleeping = true;
        inSpa = true;
    }

    /// <summary>
    /// 
    /// Determines if the choice is available to be selected based on the general conditions for choices. This is to prevent values for resources which
    /// are not valid
    /// 
    /// </summary>
    /// <param name="playerID">The ID of the player who is trying to select the choice</param>
    /// <returns></returns>
    private bool IsAvailable(int playerID)
    {
        //Need to figure out how player data is going to be stored and accessed so it is possible to determine if choice can be selected or not by a specific player
        //Below logic will work just have to find where the data is stored

        //bool hasComponent =  !(component && if player has component);
        //bool hasScrap = playerScrap + scrapChange < 0;
        //bool hasDamage = playerLifePoints + lifeChange < 0;
        //bool hasCorruption = playerCorruption != 0;
        //bool powerNotAtMax = AIPower != 100;

        //If any of the above conditions are false, then needs to return false, since this means the choice is not availabe
        // return hasComponent && hasScrap && hasDamage && hasCorruption && powerNotAtMax;

        //Currently set at dummy variable
        return true;
    }

    /// <summary>
    /// 
    /// Allocate resources to a particular player based upon their choice selection. Used for choices that are not spec challenges
    /// 
    /// </summary>
    /// <param name="playerID">The ID of the player who selected the choice</param>
    public void SelectChoice(int playerID)
    {
        //Will need to update this to allocate the resource changes from the choice to the relevant player
        //Currently unsure where this information is going to be stored, so need to update this once this is confirmed

        //Example rough Pseudocode

        //scrap(playerID) += scrapChange;
        //corruption(playerID) += corruptionChange;
        //aiPower.powerNextSurge += powerChange;
        //player(playerID).AddItem(specItem);
        //life(playerID) += lifeChange;
        //if(component){
        //player(playerID).hasComponent = true; }

        //if(oneOff) {
        //isDisabled = true; }
    }

    /// <summary>
    /// 
    /// Allocate resources to a particular player based upon their choice selection. Used for choices that are spec challenges
    /// 
    /// </summary>
    /// <param name="playerID">The ID of the player who selected the choice</param>
    /// <param name="wasSuccessful">Whether the player succeeded on the spec challenge</param>
    public void SelectChoice(int playerID, bool wasSuccessful)
    {
        //Used when the choice is a Spec Challenge
        //Again unsure where information is going to stored so will need to update this when that is cofirmed

        //Example Rough Pseudocode

        //if (wasSuccessful){
        //SelectChoice(playerID); }
        //else {
        //corruption(playerID) += scrapFail;
        //life(playerID) += lifeFail; }
    }
}

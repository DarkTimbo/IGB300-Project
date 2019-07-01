using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assigned to a Parent Object which contains all the room objects
public class ChoiceRandomiser : MonoBehaviour
{
    private const int CHOICES_PER_ROOM = 2;
    private const int ESCAPE_ROOM_ID = 9; //Escape Shuttle Bay does not have choices so needs to be ignored

    private const char LINE_SEPERATOR = '\n'; //Line Seperator in Choice List Data file
    private const char FIELD_SEPERATOR = ','; //Field Seperator in Choice List Data file

    public TextAsset choiceListData;

    private Choice[] choiceList;

    //Assign each room in the game into the array in Unity
    public GameObject [] rooms;

    private int numRooms;

    private void Start()
    {
        ReadChoiceData();
        InitialiseRooms();

        numRooms = rooms.Length;

        //Sets the number of components in the game to be equal to the number of players- mandatory for this choice being the number
        //of times the component choice will appear in the game
        //choiceList[0].mandatory = Server.Instance.playersJoined;
        choiceList[0].mandatory = 3; //For Hardcoding when Debugging

        //Getting Caught in an Infinite Loop after this point and hard crashing Unity- to Fix

        AssignMandatoryChoices();

        AssignRandomChoices();
    }

    /// <summary>
    /// 
    /// Initialises the choices in each room so they can be assinged later. 
    /// 
    /// </summary>
    private void InitialiseRooms()
    {
        foreach (GameObject room in rooms)
        {
            room.GetComponent<Room>().InitialiseRoom(CHOICES_PER_ROOM);
        }
    }

    #region Reading Choice Data from File
    /// <summary>
    /// 
    /// Imports Choice Data from Local csv file and sorts it into choiceList array
    /// 
    /// </summary>
    private void ReadChoiceData()
    {
        int counter = 0;

        string[] records = choiceListData.text.Split(LINE_SEPERATOR);
        
        choiceList = new Choice[records.Length - 1]; //Need to not include field headings

        //Loop through each row and as such each available choice
        foreach (string record in records)
        {
            string[] fields = record.Split(FIELD_SEPERATOR);

            //Need to skip the field headings (hardcoded- might be a better way to do this)
            if (fields[0] == "Choice_ID")
            {
                continue;
            }
            else
            {
                //Instantiate the choice list element
                choiceList[counter] = new Choice
                {
                    //Assign each field to its relevant data type in the current choice
                    choiceID = ConvertStringToInt(fields[0]),
                    choiceName = fields[1],

                    weighting = ConvertStringToFloat(fields[2]),
                    unique = ConvertStringToBool(fields[3]),
                    mandatory = ConvertStringToInt(fields[4]),
                    oneOff = ConvertStringToBool(fields[5]),

                    specChallenge = fields[6],
                    targetScore = ConvertStringToInt(fields[7]),

                    scrapChange = ConvertStringToInt(fields[8]),
                    corruptionChange = ConvertStringToInt(fields[9]),
                    powerChange = ConvertStringToInt(fields[10]),
                    specItem = new Item(fields[11]),
                    lifeChange = ConvertStringToInt(fields[12]),
                    component = ConvertStringToBool(fields[13]),

                    corruptionFail = ConvertStringToInt(fields[14]),
                    lifeFail = ConvertStringToInt(fields[15]),

                    inBar = ConvertStringToBool(fields[16]),
                    inDining = ConvertStringToBool(fields[17]),
                    inEngineering = ConvertStringToBool(fields[18]),
                    inKitchen = ConvertStringToBool(fields[19]),
                    inSleeping = ConvertStringToBool(fields[20]),
                    inSpa = ConvertStringToBool(fields[21])
                };

                counter += 1;
            }
        }
    }

    /// <summary>
    /// 
    /// Converts a string integer value to an actual integer value. Returns 0 if conversion fails
    /// 
    /// </summary>
    private int ConvertStringToInt (string value)
    {
        int result;
        int.TryParse(value, out result);
        return result;
    }

    /// <summary>
    /// 
    /// Converts a string floating point value to an actual floating point value. Returns 0 if conversion fails
    /// 
    /// </summary>
    private float ConvertStringToFloat(string value)
    {
        float result;
        float.TryParse(value, out result);
        return result;
    }

    /// <summary>
    /// 
    /// Converts a string boolean value to an actual boolean value
    /// 
    /// </summary>
    private bool ConvertStringToBool (string value)
    {
        if(value.ToLower() == "true")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
    
    #region Mandatory Choice Handling
    /// <summary>
    /// 
    /// Finds all the mandatory choices in the game and assigns them to randomised rooms
    /// 
    /// </summary>
    private void AssignMandatoryChoices()
    {
        List<Choice> mandatoryChoices = FindMandatoryChoices();
        int roomLocation = -1;
        bool roomFull;

        foreach (Choice choice in mandatoryChoices)
        {
            for (int occurence = 0; occurence < choice.mandatory; occurence++)
            {
                do
                {
                    roomLocation = Random.Range(0, numRooms);

                } while (!TestAssignChoice(choice, roomLocation, out roomFull));
            }
        }
    }

    /// <summary>
    /// 
    /// Returns a list of which choices are mandatory
    /// 
    /// </summary>
    /// <returns></returns>
    private List<Choice> FindMandatoryChoices()
    {
        List<Choice> mandatoryChoices = new List<Choice>();

        //Add the component choice- mandatory has to be determined based on the number of players so cannot be stored in the database
        mandatoryChoices.Add(choiceList[0]);

        foreach (Choice choice in choiceList)
        {
            if (choice.mandatory != 0)
            {
                mandatoryChoices.Add(choice);
            }
        }
        return mandatoryChoices;
    }
    #endregion

    #region Random Choice Handling
    /// <summary>
    /// 
    /// Assigns the choices throughout the map which are randomised i.e. choices that are not unique and do not have a mandatory number of occurences (since these choices can only appear the given number of times)
    /// 
    /// </summary>
    private void AssignRandomChoices()
    {
        //Total Weighting is used in the process of randomly selecting the choices
        float totalWeighting;
        int roomCounter = 0;
        //Room Full determines if the room which choices are being placed in already has enough choices placed in it
        bool roomFull = false;

        //Obtain the relevant choices
        List<Choice> randomChoices = FindRandomChoices(out totalWeighting);
        Choice selectedChoice = new Choice();

        //Loop through all the rooms
        foreach (GameObject room in rooms)
        {
            //First loop determines if the room is full, breaking out of the loop if it is and moving to the next room (will always determine if it is full first though)
            //The loop will continue until all choices have a relevant choice in them
            do
            {
                //Second loop determines if the randomly selected choice can be placed in that room or not, assigning it if it can be and finding a new choice if it cannot
                do
                {
                    selectedChoice = RandomChoice(randomChoices, totalWeighting);

                } while (!TestAssignChoice(selectedChoice, roomCounter, out roomFull));

                //If the item is unique and is successfully placed in the loop above, then can no longer place that choice, so removes it from the available list
                //This needs to happen whether the room is full or not, so is placed inside the roomFull loop
                if (selectedChoice.unique)
                {
                    randomChoices.Remove(selectedChoice);
                }

            } while (!roomFull);
            
            //Sets up variables for next room loop
            roomCounter += 1;
            roomFull = false;
        }
    }

    /// <summary>
    /// 
    /// Picks a random choice out of a list of choices based on their weighting
    /// 
    /// </summary>
    /// <param name="randomChoices">The list of choices which the random choice can be selected from</param>
    /// <param name="totalWeighting">The sum of the weightings of the choices in the random list</param>
    /// <returns>The randomly selected choice</returns>
    private Choice RandomChoice(List<Choice> randomChoices, float totalWeighting)
    {
        //Finds a position within the sum of the weighting
        float randomChoicePos = Random.Range(0, totalWeighting);

        float weightingCounter = 0;

        //Determines which choice the random position falls into based on their weighting. If the random position falls between the lowest weighting value and the highest weighting value
        //of a choice, then that choice is selected
        foreach (Choice choice in randomChoices)
        {
            weightingCounter += choice.weighting;
            if (randomChoicePos <= weightingCounter)
            {
                return choice;
            }
        }

        //Should never reach this point, however provides the default choice if it does
        return new Choice();
    }

    /// <summary>
    /// 
    /// Returns the choices which are to be randomised on the map. Choices which are exempt from this are those that are unique and have a mandatory value, as these choices can only appear on the map
    /// a certain number of times, which was already handled in mandatory choice handling
    /// 
    /// </summary>
    /// <param name="totalWeighting">The sum of all the weightings for the available choices. Used for randomisation selection</param>
    /// <returns>The choices which are available to be randomised</returns>
    private List<Choice> FindRandomChoices(out float totalWeighting)
    {
        List<Choice> randomChoices = new List<Choice>();
        totalWeighting = 0;

        foreach (Choice choice in choiceList)
        {
            //If a choice is unique and has a mandatory value, it has already been assigned the maximum number of times it can be, so must be excluded from the random choice list
            if (!choice.unique || choice.mandatory == 0)
            {
                randomChoices.Add(choice);
                //Sum up the total weighting of all the randomised choices to be utilised
                totalWeighting += choice.weighting;
            }
        }

        return randomChoices;
    }
    #endregion

    #region Choice Assignment
    /// <summary>
    /// 
    /// Tests if a choice can be assigned to a choice position in a given room, assigning it there if it can.
    /// Function will test both choice positions in the given room, assinging it to a later position if the earlier are occupied
    /// If the choice cannot appear in the given room's room type, assignment will be unsuccessful
    /// 
    /// </summary>
    /// <param name="choice">The choice to be assingned</param>
    /// <param name="roomLocation">The ID of the given room</param>
    /// <param name="roomFull">Whether the function returned false because the room was fully occupied or not</param>
    /// <returns>Returns whether the assignment was successful or not</returns>
    private bool TestAssignChoice(Choice choice, int roomLocation, out bool roomFull)
    {
        roomFull = false;
        //Tests if the given choice is available to be placed in the room of the given type
        if (RoomTypeViable(choice, rooms[roomLocation].GetComponent<Room>().roomType))
        {
            Choice[] locationChoices = rooms[roomLocation].GetComponent<Room>().roomChoices;

            //Loop through each of the choice positions in the given room i.e. how many choices can possibly exist in a room
            for (int choicePos = 0; choicePos < CHOICES_PER_ROOM; choicePos++)
            {
                //Tests if any of the other choices already in the room are the same as the given choice, since a choice cannot appear more than once in a given room
                if (locationChoices[choicePos].choiceID != choice.choiceID)
                {
                    //If the selected choice is not currently occupied (default constructor will have an ID of 0), will assign choice there and return a success
                    if (locationChoices[choicePos].choiceID == 0)
                    {
                        rooms[roomLocation].GetComponent<Room>().roomChoices[choicePos] = choice;
                        return true;
                    }
                    // If the current choice position is occupied, will continue to test later choice positions
                    else if (choicePos != CHOICES_PER_ROOM - 1)
                    {
                        continue;
                    }
                    //If there are no possible spots for the choice to occupy, will return false
                    else
                    {
                        roomFull = true;
                        return false;
                    }
                }
                //If the choice cannot be assigned here because it already has been, returns false
                else
                {
                    return false;
                }
            }

            //Should never reach this point, however provides a dummy case
            return false;
        }
        //If the choice cannot be assigned in the given room, returns false
        else
        {
            return false;
        }
        
    }

    /// <summary>
    /// 
    /// Tests if a choice can appear in a room of the given room type
    /// 
    /// </summary>
    /// <param name="choice">The choice to be tested</param>
    /// <param name="roomType">The type of room being tested</param>
    /// <returns>Whether the choice can be assigned</returns>
    private bool RoomTypeViable(Choice choice, string roomType)
    {
        switch (roomType)
        {
            case "Bar":
                return choice.inBar;
            case "Dining":
                return choice.inDining;
            case "Engineering":
                return choice.inEngineering;
            case "Escape": //Escape Room does not have any choices in it so will always return false
                return false;
            case "Kitchen":
                return choice.inKitchen;
            case "Sleeping":
                return choice.inSleeping;
            case "Spa":
                return choice.inSpa;
            default:
                return false;
        }
    }
    #endregion
}

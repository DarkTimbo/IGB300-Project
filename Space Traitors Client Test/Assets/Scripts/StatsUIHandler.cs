using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUIHandler : MonoBehaviour
{

    public Text BrawnText;
    public Text SkillText;
    public Text TechText;
    public Text CharmText;
    public Text ScrapTotal;
    public Text Componants;

    public Slider CorruptionSlider;
    public Canvas InventoryCanvas;

    //public Slider CorruptionSlider;
    private GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        BrawnText.text =  Player.GetComponent<Player>().Brawn.ToString();
        SkillText.text = Player.GetComponent<Player>().Skill.ToString();
        TechText.text = Player.GetComponent<Player>().Tech.ToString();
        CharmText.text = Player.GetComponent<Player>().Charm.ToString();

        Componants.text = Player.GetComponent<Player>().Components.ToString();
        ScrapTotal.text = Player.GetComponent<Player>().scrapTotal.ToString();

        CorruptionSlider.value = Player.GetComponent<Player>().Corruption;

    }
    public void OnImageClick() {

        InventoryCanvas.enabled = true;

    }


    public void OnClickExitButton() {

        InventoryCanvas.enabled = false;

    }
}

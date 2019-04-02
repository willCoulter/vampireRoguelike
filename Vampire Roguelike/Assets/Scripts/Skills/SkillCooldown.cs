using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldown : MonoBehaviour
{

    public SkillCooldown instance;
    public GameObject pickupPrefab;

    public string abilityButtonAxisName = "Fire1";
    public Image darkMask;
    public Text cooldownTextDisplay;

    public Skill skill;

    private Image buttonImage;
    private AudioSource abilitySource;
    private float cooldownDuration;
    private float nextReadyTime;
    private float cooldownTimeLeft;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(skill != null)
        {
            //Check if cooldown is complete
            bool cooldownComplete = (Time.time > nextReadyTime);
            if (cooldownComplete)
            {
                AbilityReady();
                if (Input.GetButtonDown(abilityButtonAxisName))
                {
                    ButtonTriggered();
                }
            }
            //If not, run cooldown
            else
            {
                CoolDown();
            }
        }
    }

    public void Initialize(Skill selectedSkill, GameObject player){
        skill = selectedSkill;
        buttonImage = GetComponent<Image>();
        abilitySource = GetComponent<AudioSource>();

        buttonImage.sprite = skill.skillSprite;
        darkMask.sprite = skill.skillSprite;
        cooldownDuration = skill.baseCD;
        skill.Initialize(player);

        //Ability ready
        AbilityReady();
    }

    public void DropSkill()
    {
        //Create new skillpickup at player position
        GameObject droppedSkill = Instantiate(pickupPrefab, PlayerController.instance.gameObject.transform.position, PlayerController.instance.gameObject.transform.rotation);
        SkillPickup droppedSkillScript = droppedSkill.GetComponent<SkillPickup>();

        //Set skill of skillpickup
        droppedSkillScript.skill = skill;

        //Remove from inventory
        SkillInventory.instance.Remove(skill);

        //Clear properties
        skill = null;
        buttonImage.sprite = null;
        darkMask.sprite = null;
    }

    private void AbilityReady(){
        cooldownTextDisplay.enabled = false;
        darkMask.enabled = false;
    }

    private void CoolDown(){
        cooldownTimeLeft -= Time.deltaTime;
        float roundedCD = Mathf.Round(cooldownTimeLeft);

        cooldownTextDisplay.text = roundedCD.ToString();
        darkMask.fillAmount = (cooldownTimeLeft / cooldownDuration);
    }

    private void ButtonTriggered(){
        //If pause menu is not open, trigger skill
        if (!UIManager.instance.pauseMenuOpen)
        {
            PlayerController.instance.anim.SetTrigger("Casting");

            nextReadyTime = cooldownDuration + Time.time;
            cooldownTimeLeft = cooldownDuration;
            darkMask.enabled = true;
            cooldownTextDisplay.enabled = true;

            //if(skill.skillSound != null){
            //    abilitySource.clip = skill.skillSound;
            //    abilitySource.Play();
            //}

            skill.TriggerSkill();
        }
        
    }
}

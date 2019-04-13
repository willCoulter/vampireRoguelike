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



                KeyCode skillKey = PlayerController.instance.playerControls[abilityButtonAxisName];

                if (Input.GetKeyDown(skillKey) && PlayerController.instance.blood >= skill.baseCost)
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

    public void Initialize(Skill selectedSkill)
    {
        skill = selectedSkill;
        buttonImage = GetComponent<Image>();

        buttonImage.sprite = skill.skillSprite;
        darkMask.sprite = skill.skillSprite;
    }

    public void DropSkill()
    {
        if(skill != null)
        {
            float radius = 1.5f;

            //Create new skillpickup near player position
            GameObject droppedSkill = Instantiate(pickupPrefab, Random.insideUnitSphere * radius + PlayerController.instance.gameObject.transform.position, PlayerController.instance.gameObject.transform.rotation);
            SkillPickup droppedSkillScript = droppedSkill.GetComponent<SkillPickup>();

            //Set skill of skillpickup
            droppedSkillScript.skill = skill;
            droppedSkillScript.skill.skillPrice = 0;

            //Clear properties
            skill = null;
            buttonImage.sprite = null;
            darkMask.sprite = null;

            //Refresh pause ui
            UIManager.instance.RefreshPauseSkills();
        }
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
            PlayerController.instance.blood -= skill.baseCost;
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

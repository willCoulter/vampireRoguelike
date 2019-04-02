using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldown : MonoBehaviour
{

    public SkillCooldown instance;

    public string abilityButtonAxisName = "Fire1";
    public Image darkMask;
    public Text cooldownTextDisplay;

    public Skill skill;
    [SerializeField] private GameObject player;

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
        //Check if cooldown is complete
        bool cooldownComplete = (Time.time > nextReadyTime);
        if (cooldownComplete){
            AbilityReady();
            if(Input.GetButtonDown(abilityButtonAxisName)){
                PlayerController.instance.anim.SetTrigger("Casting");
                ButtonTriggered();
            }
        }
        //If not, run cooldown
        else
        {
            CoolDown();
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

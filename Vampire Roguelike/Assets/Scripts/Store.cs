using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{

    public GameObject storeSkill1;
    public GameObject storeSkill2;
    public GameObject storeSkill3;

    public List<Skill> allSkills = new List<Skill>();


    // Start is called before the first frame update
    void Start()
    {
        storeSkill1.GetComponent<SkillPickup>().skill = allSkills[0];
        storeSkill2.GetComponent<SkillPickup>().skill = allSkills[1];
        storeSkill3.GetComponent<SkillPickup>().skill = allSkills[2];

        storeSkill1.GetComponent<SkillPickup>().skill.skillPrice = 200;
        storeSkill2.GetComponent<SkillPickup>().skill.skillPrice = 100;
        storeSkill3.GetComponent<SkillPickup>().skill.skillPrice = 100;
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}

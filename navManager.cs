using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navManager : MonoBehaviour//if youve found this then well done! this is a WIP from someone who cant code very well
{

    [Header("mission shit")]
    public int current_level;
    public int current_mission;
    public int current_target;
    public int target_box_number;//the number of the box collider script
    public bool checkGreater;//set through missions!!

    [Header("scripts")]
    [SerializeField] private nav_script[] scripts;
    [SerializeField] private CharacterMovement player_movement;
    [SerializeField] private navManager otherManager;

    //I have removed some code specific to my project so you may have to create other scripts or add
    //functions to other scripts to call these in specific situation

    public void change_level(int new_level, int mission_to_start)
    {
        current_level = new_level;
        current_mission = mission_to_start;
    }

    public void change_mision(int new_mission_number, int first_target)
    {
        current_mission = new_mission_number;
        current_target = first_target;
    }

    public void change_target(int target_position_number, bool check)
    {
        current_target = target_position_number;
        checkGreater = check;
    }

    void Update()
    {
        if(otherManager != null)
        {
            checkGreater = !otherManager.checkGreater;
        }
    }
}

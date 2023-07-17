using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nav_script : MonoBehaviour//add this to the parent of the two different arrows
{
    public int this_position;
    public int player_position;//these are both based on the collider positioning!
    [SerializeField] private navManager nav_manager;
    [SerializeField] private CharacterMovement player_movement;

    [Header("target stuff")]
    public int target_box_number;


    [Header("arrow parenting stuff")]
    [SerializeField] private bool should_enable;//should this enable or disable the next/previous sections arrows
    [SerializeField] private GameObject previous_parent;
    [SerializeField] private GameObject next_parent;
    [SerializeField] private Transform parent;

    [Header("these arrows")]
    [SerializeField] private GameObject greenArrows;
    [SerializeField] private GameObject redArrows;

    [Header("specifics")]
    [SerializeField] private bool has_special_arrows;//towards specific destination
    [SerializeField] private GameObject special_arrows;

    [Header("Rotation")]
    [SerializeField] private Quaternion originalRot;//original yRot
    [SerializeField] private Quaternion reverseRot;//when reversed
    [SerializeField] private bool adjustx;
    [SerializeField] private bool adjusty;
    [SerializeField] private bool adjustz;

    [Header("checks")]
    public bool check_greaterThan;//check if the target position is greater than this position? else check less than!

    void Awake()
    {
        originalRot = parent.localRotation;
        float x = originalRot.x;
        float y = originalRot.y;
        float z = originalRot.z;
        Vector3 adjusted = new Vector3(x, y, z);

        if(adjustx == true)//there is probably a better way of doing this but oh well, it works
        {
            adjusted.x = adjusted.x + 180f;
        }
        if(adjusty == true)
        { 
            adjusted.y = adjusted.y + 180f;   
        }
        if(adjustz == true)
        {
            adjusted.z = adjusted.z + 180f;
        }

        reverseRot = Quaternion.Euler(adjusted);
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "vehicle")
        {
            enable_disable();
        }
        else if(collider.tag == "Player")
        {
            enable_disable();
        }
    }

    void enable_disable()
    {
        //player_movement.current_box_position = this_position;
        //you don't need the player movement current box (if you use it that is) but it doesnt hurt to have
        if (should_enable)
        {
            previous_parent.SetActive(false);
            next_parent.SetActive(true);
        }
    }

    void Update()
    {
        target_box_number = nav_manager.target_box_number;
        check_greaterThan = nav_manager.checkGreater;

        if (check_greaterThan == true)
        {

            if (target_box_number > this_position)
            {
                parent.localRotation = originalRot;
                green();
            }
            else if(target_box_number < this_position)
            {
                parent.localRotation = reverseRot;
                red();
            }
            else if (target_box_number == this_position && has_special_arrows)
            {
                special();
            }
        }
        else if(check_greaterThan == false)
        {

            if (target_box_number > this_position)
            {
                parent.localRotation = originalRot;
                red();
            }
            else if (target_box_number < this_position)
            {
                parent.localRotation = reverseRot;
                green();
            }
            else if( target_box_number == this_position && has_special_arrows)
            {
                special();
            }
        }
    }

    void green()
    {
        if(has_special_arrows == true)
        {
            special_arrows.SetActive(false);
        }
        greenArrows.SetActive(true);
        redArrows.SetActive(false);
    }

    void red()
    {
        if (has_special_arrows == true)
        {
            special_arrows.SetActive(false);
        }
        greenArrows.SetActive(false);
        redArrows.SetActive(true);
    }

    void special()
    {
        redArrows.SetActive(false);
        greenArrows.SetActive(false);
        special_arrows.SetActive(true);
    }
    
}

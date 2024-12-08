using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    // script for handling superspeed
    private Superspeed superspeed;
    // script for teleportation
    private Teleportation teleportation;
    // script for the shield dome ability
    private ShieldDome shieldDome;

    // keeping track of which ability is active, if any
    private enum ActiveAbility { None, Superspeed, Teleportation, ShieldDome }
    private ActiveAbility currentAbility = ActiveAbility.None;

    void Start()
    {
        // grabbing all the needed ability scripts
        superspeed = GetComponent<Superspeed>();
        teleportation = GetComponent<Teleportation>();
        shieldDome = GetComponent<ShieldDome>();
    }

    void Update()
    {
        // check if the player presses Key 1 for superspeed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // toggle superspeed on/off
            if (currentAbility == ActiveAbility.Superspeed)
            {
                DeactivateSuperspeed();
            }
            else
            {
                ActivateSuperspeed();
            }
        }

        // check if the player presses Key 2 for teleportation
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateTeleportation();
        }

        // check if the player presses Key 3 for shield dome
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // toggle shield dome on/off
            if (currentAbility == ActiveAbility.ShieldDome)
            {
                DeactivateShieldDome();
            }
            else
            {
                ActivateShieldDome();
            }
        }
    }

    private void ActivateSuperspeed()
    {
        DeactivateCurrentAbility();
        superspeed.Activate();
        currentAbility = ActiveAbility.Superspeed;
    }

    private void DeactivateSuperspeed()
    {
        superspeed.Deactivate();
        currentAbility = ActiveAbility.None;
    }

    private void ActivateTeleportation()
    {
        DeactivateCurrentAbility();
        teleportation.Activate();
        currentAbility = ActiveAbility.Teleportation;
    }

    private void ActivateShieldDome()
    {
        DeactivateCurrentAbility();
        shieldDome.Activate();
        currentAbility = ActiveAbility.ShieldDome;
    }

    private void DeactivateShieldDome()
    {
        shieldDome.Deactivate();
        currentAbility = ActiveAbility.None;
    }

    private void DeactivateCurrentAbility()
    {
        // check which ability is currently active and deactivate it
        switch (currentAbility)
        {
            case ActiveAbility.Superspeed:
                DeactivateSuperspeed();
                break;
            case ActiveAbility.ShieldDome:
                DeactivateShieldDome();
                break;
        }
    }
}

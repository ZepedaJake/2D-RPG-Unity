using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour {

    /*
    PROBLEMS
    need to recode skills so that they can easily be saved
    need to find a way to destroy ALL objects when selecting quit from pause menu
     
      TODO:
balance enemies
interaction with Robes on 02-10-11   
balance skills
quit dialouge
death
dialouge
secrets
add more to Debug mode
other pedestals, and a seperate script for each


    anything not seen below was prior to first log OR im an idiot and forgot to log it.

    Done: 1/6/2017

    fixed collisions
    all menus are keyboard only. will use mouse for secrets  ( ͡° ͜ʖ ͡°)
    fixed cooldowns for battle skills. 
    added greying out skills that are not available.
    heal shows +healed in green now


    Done: 1/10/2017

    options menu: can change volumes in there
    icons for enemy hitting you
    sounds for enemy hitting you
    fixed some menu cycling
    changed heal to scale from def
    added skill leveling
    fixed broken defend function
    

    Done: 3/5/2017
    been forgetting to log =_=

    can save!
    majority of tower done
    fixed battles
        enemy defend -> attack bug,
        enemies not healing or power attacking
        dealing - damage and healing enemies
    added inventory support for key items
    added song for fountain
    changed loading code. more reliable and faster
    changed corner sprites
    added pillar sprites, can pass through mid pieces.
    added in reset hands. need to program them


    Done: 3/7/2017

    More Sound effects
    changed all volumes to be higher
    added boss room
    inventory working
    bosses can have custom songs
    debug now shows current floor


    Done: 3/12/2107
    
    Pedstal Designed and working
        can take and place orb as much as you please
    display messages for items gained and xp on enemy kill
    display messages available for other use
    yes/no box for pedestal working.
    yes/no should work in other cases, feeding a provided game object with a bool. game object must have a Decision Function tho
    fixed code causing doors to double spawn
    portals now use a goToPoint vector 2, this is useful for maps with multiple green or red portals
    there is no longer a mapSave json. its all done with goToPoint
    added "sprite" (no name yet)
        moves around you constantly, follows, glows :D

    Done: 3/18/2017

    changed bool box. now you input 2 strings, what you want the box to say, and which function to send to.
    pedestal now checks state of related orb to tell itself to turn on or off
    will need seperate pedestal scripts i guess...
    Added first overworld map.
    added over world tiles for Sand, Grass, Water, Trees, Paths, and the Light House
    added transparent portals
    new flag for first time the red orb is placed in pedestal
    wall on floor one collapses after new flag and you can leave the light house.
    FIGURED  OUT GOD DAMN SPRITE PROBLEM AND SPEND 2 HOURS FIXING EACH MAP!
    sprites needed to be set to 32 pixels per unit, and reset light house sprites to pivot on center instead of top left.
    now player goes straight to 0,0 and does not need the .5 offsets
    did same with fountain and light house in overworld.
    menu was also changed but seems unaffected

    everything will lay out cleanly now U.U ahh sweet maths


    Done: 3/20/2017

    added function for messages / dialouge, works completely... so far....
    created resources folder.. bout time.
    changed boolbox to scale with inner text
    thinking of making a color filter tyep thign to make the overworld dark looking
    tried to make boolbox like i wanted. failed and wasted 2 hours =_=

*/
}

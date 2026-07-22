using UnityEngine;

public class enums
{
}
public enum ETowerType
{
    Base,//bullet shoot
    Fire,//single fire ball  with burn effect
    Earth,//single rock with aoe damage with stun effect
    Water,//single waterBubble with slow effect
    Air,//single throw back
    Steam,//tower AOE with burn effect
    Flamethrower,//tower Aoe steam thrower damage over time
    SandStorm,//tower AOE with slow effect with damage
    Ice,//single ice ball with damage and enemy freeze
    Mud,//tower AOE with stun effect and damage
    Lava//tower AOE with burn effect and slow effect
}
public enum BulletType
{
    Single,
    AOE
}
public enum EElements
{
    None,
    Fire,
    Water,
    Earth,
    Air
}

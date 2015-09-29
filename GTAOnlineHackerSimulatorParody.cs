using System;
using GTA;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using GTA.Native;
using System.Drawing;
using GTA.Math;
using System.Collections.Generic;

public class GTAOSim : Script
{
    int ExampleTimer;
    bool onmode3 = false;
    bool onmode4 = false;
    bool onmode5 = false;
    bool onmode6 = false;
    bool onmode8 = false;
    bool onmode9 = false;
    int time = 60000;
    List<Prop> todelete = new List<Prop>();

    public GTAOSim()
    {
        Tick += OnTick;
        ExampleTimer = Game.GameTime + time;
    }

    void OnTick(object sender, EventArgs e)
    {
        try
        {
            if (onmode3)
            {
                Game.Player.Character.Health = Game.Player.Character.MaxHealth;
                World.AddExplosion(Game.Player.Character.Position, ExplosionType.WaterHydrant, 1f, 0f);
            }

            if (onmode6)
            {
                Game.Player.Character.Health = Game.Player.Character.MaxHealth;
                World.AddExplosion(Game.Player.Character.Position, ExplosionType.Barrel, 1f, 0f);
            }

            if (onmode5)
            {
                Game.Player.Character.Health = Game.Player.Character.MaxHealth;
                Vector3 Mouth = Function.Call<Vector3>(Hash.GET_PED_BONE_COORDS, Game.Player.Character, (int)Bone.SKEL_Head, 0.1f, 0.0f, 0.0f);
                Function.Call(Hash._ADD_SPECFX_EXPLOSION, Mouth.X, Mouth.Y, Mouth.Z, 12, 12, 1.0f, true, true, 0.0f);
            }

            if (onmode4)
            {
                World.CreateAmbientPickup(PickupType.MoneyCase, Game.Player.Character.Position + new Vector3(0, 0, 1), 289396019, (new Random()).Next(5000, 40000));
            }

            if (onmode8)
            {
                todelete.Add(World.CreateProp(1952396163, Game.Player.Character.Position, false, true));
                if (todelete.Count >= 10)
                {
                    foreach (Prop prp in todelete)
                    {
                        prp.Delete();
                    }
                }
            }

            if (onmode9)
            {
                todelete.Add(World.CreateProp(-1268267712, Game.Player.Character.Position, false, false));
                if (todelete.Count >= 10)
                {
                    foreach (Prop prp in todelete)
                    {
                        prp.Delete();
                    }
                }
            }

            if (Game.GameTime > ExampleTimer)
            {
                ExampleTimer = Game.GameTime + time;
                UI.Notify("It is time for random events!");
                int roll = new Random().Next(1, 10);
                UI.Notify("Rolled: " + roll);

                switch (roll)
                {
                    case 1:
                        ClearModes();
                        UI.Notify("1: GET SWATTED!");
                        NewSpawnCopArrest(Game.Player.Character);
                        break;
                    case 2:
                        ClearModes();
                        UI.Notify("2: GET TAZED!");
                        NewSpawnTaserGuy(Game.Player.Character);
                        break;
                    case 3:
                        ClearModes();
                        onmode3 = true;
                        UI.Notify("3: Avatar Book 1: Git water nub!");
                        break;
                    case 4:
                        ClearModes();
                        onmode4 = true;
                        UI.Notify("4: U GOT SUM NIECE LUX M8! MONEY RAIN FOR 1MIN");
                        break;
                    case 5:
                        ClearModes();
                        onmode5 = true;
                        UI.Notify("5: Avatar Book 3: Git fire nub!");
                        break;
                    case 6:
                        ClearModes();
                        onmode6 = true;
                        UI.Notify("6: Laggy explosions!");
                        break;
                    case 7:
                        ClearModes();
                        UI.Notify("7: You got lucky! No hackers on this lobby!");
                        break;
                    case 8:
                        ClearModes();
                        onmode8 = true;
                        UI.Notify("8: Windmills!"); //Bugged
                        break;
                    case 9:
                        ClearModes();
                        onmode9 = true;
                        UI.Notify("9: UFOs"); //bugged
                        break;

                }
            }
        }
        catch
        {

        }
    }

    void ClearModes()
    {
        onmode3 = false;
        onmode4 = false;
        onmode5 = false;
        onmode6 = false;
        onmode8 = false;
        onmode9 = false;
    }

    void NewSpawnTaserGuy(Ped player)
    {
        //So is this from your mod? Provide proof and I'll credit you. Someone sent me this code without any source.
        //I also edited it a bit
        Random r = new Random();
        GTA.Math.Vector3 spawnLoc = player.Position + new Vector3((r.Next(0, 30) / 10), (r.Next(0, 30) / 10), 0);

        List<string> model_names = new List<string>();

        model_names.Add("a_m_m_tramp_01");
        model_names.Add("a_f_m_trampbeac_01");
        model_names.Add("a_m_m_trampbeac_01");
        model_names.Add("s_m_y_robber_01");
        model_names.Add("a_f_m_beach_01");
        model_names.Add("a_m_m_beach_01");
        model_names.Add("a_m_m_beach_02");

        Ped peds = GTA.World.CreatePed(model_names[r.Next(0, model_names.Count)], spawnLoc);

        peds.Weapons.Give(WeaponHash.StunGun, 1, true, true);

        peds.CanRagdoll = false;
        peds.Task.FightAgainst(player);

        peds.Armor = 90;
    }

    void NewSpawnCopArrest(Ped player)
    {
        //So is this from your mod? Provide proof and I'll credit you. Someone sent me this code without any source.
        //I also edited it a bit

        Random r = new Random();
        GTA.Math.Vector3 spawnLoc = player.Position + new Vector3((r.Next(0, 30) / 10), (r.Next(0, 30) / 10), 0);

        List<string> model_names = new List<string>();

        model_names.Add("s_f_y_cop_01");
        model_names.Add("s_m_m_snowcop_01");
        model_names.Add("s_m_y_cop_01");
        model_names.Add("s_m_y_hwaycop_01");
        model_names.Add("csb_cop");

        Ped peds = GTA.World.CreatePed(model_names[r.Next(0, model_names.Count)], spawnLoc);

        peds.Task.ClearAllImmediately();

        peds.Weapons.Give(WeaponHash.Pistol, 9999, true, true);
        peds.Weapons.Give(WeaponHash.Nightstick, 9999, true, true);

        peds.CanRagdoll = false;
        peds.Task.FightAgainst(player);

        peds.Armor = 90;
    }
}

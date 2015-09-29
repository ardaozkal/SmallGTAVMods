using System;
using GTA;
using System.Net;
using System.IO;
using GTA.Native;
using System.Drawing;
using GTA.Math;

public class BahamaMamas : Script
{
	Blip asd;
	bool firstrun = true;
	bool debugmode = false;
    public BahamaMamas()
    {
        Tick += OnTick;
        asd = World.CreateBlip(new Vector3(-1387.975f, -587.7377f, 30.21593f));
        asd.Sprite = BlipSprite.Bar;
        asd.Color = BlipColor.Blue; //You can change this color to Blue, Green, White, Yellow or Red. Case sensitive.
    }


    void OnTick(object sender, EventArgs e)
    {
		if (firstrun && asd.IsOnMinimap)
		{
			firstrun = false;
			UI.Notify("Don't mind the loading online screen if you're seeing it, it just loads the map.");
		    // KUDOS TO ISOFX FOR THE NATIVE \/
            Function.Call(Hash._LOAD_MP_DLC_MAPS);
		    // KUDOS TO ISOFX FOR THE IPL \/
            Function.Call(Hash.REQUEST_IPL, "hei_sm_16_interior_v_bahama_milo_");
			UI.Notify("After it loads, just walk to the door of Bahama Mamas to be teleported inside.");
		}
	   bool xf = Game.Player.Character.Position.X < -1386.75f && Game.Player.Character.Position.X > -1389f;
	   bool yf = Game.Player.Character.Position.Y > -589.1f && Game.Player.Character.Position.Y < -587.35f;
	   bool zf = Game.Player.Character.Position.Z > 29f && Game.Player.Character.Position.Z < 35f; 
       
       if (xf && yf && zf)
	   {
		   if (Game.Player.Character.IsInVehicle())
		   {
			   Game.Player.Character.CurrentVehicle.Position = new Vector3(-1387.975f, -584.7377f, 30.35f);
		   }
		   else
		   {
               Game.Player.Character.Position = new Vector3(-1387.975f, -584.7377f, 30.35f); 
		   }
			UI.Notify("Teleported you outside, walk to the door to be teleported inside.");
	   }
	   
	   bool xf2 = Game.Player.Character.Position.X < -1387.7f && Game.Player.Character.Position.X > -1389f;
	   bool yf2 = Game.Player.Character.Position.Y > -586.9f && Game.Player.Character.Position.Y < -585.35f;
       
	   if (xf2 && yf2 && zf)
	   {
		   if (Game.Player.Character.IsInVehicle())
		   {
			   Game.Player.Character.CurrentVehicle.Position = new Vector3(-1391f, -591.54f, 30.35f);
		   }
		   else
		   {
            Game.Player.Character.Position = new Vector3(-1391f, -591.54f, 30.35f); 
		   }
			UI.Notify("Teleported you inside, walk to the door to be teleported outside.");
	   }
	   
	   if (debugmode)
	   {
           UIText uIText = new UIText(xf2.ToString() + yf2.ToString() + " - " +xf.ToString() + yf.ToString() + zf.ToString() + " - " + Game.FPS.ToString() + " - " + (Game.Player.Character.Position.ToString()), new Point(10, 10), 0.4f, Color.Red);
           uIText.Draw();
	   }
    }
}

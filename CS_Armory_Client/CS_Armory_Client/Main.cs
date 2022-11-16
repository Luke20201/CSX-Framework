using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core;
using NativeUI;
using System.Drawing;

namespace CS_Armory_Client
{
    public class Main : BaseScript
    {
        /* How to edit
         * 
         * Editing this has been made extremely easy. Firstly, go to the category that your gun falls under. EG: natoPistols
         * Then, simply create a new gun with a name like :         public Gun testGun = new Gun();
         * This create the gun as a new object within the script.
         * Then, navigate to void SetupGuns() and find the type you want to use again. EG: 
         *  glock17.itemName = new UIMenuItem("Glock 17");
         *  glock17.spawncode = "WEAPON_G17";
         *  glock17.attachments = new string[]{ "mm_g17_flash4" };
         *   natoPistols.Add(glock17);
         *   allGuns.Add(glock17);
         * 
         * I will now break down what all of that means
         * glock17.itemName = new UIMenuItem("Glock 17"); This means that the Gun will show up on the UIMenu as "Glock 17"
         * glock17.spawncode = "WEAPON_G17"; This means that the Gun's spawncode is "WEAPON_G17"
         * glock17.attachments = new string[]{ "mm_g17_flash4", "attacment2" }; This adds a list of attachments the gun should spawn with.
         * natoPistols.Add(glock17); This specfies that the gun should be used by nato. Should the gun need to be used by the taliban, you would use taliPistols.Add(glock17) NOTE: this refers to what you named the item as first.
         * allGuns.Add(glock17); Same as above. This must be done for all guns for them to be detected.
         * 
         * And that is it! It is really easy to do, as the rest is automated.
         */
        private List<WeaponHash> currentPedWeapons = new List<WeaponHash>();
        MenuPool MenuPool;
        Ped playerPed => Game.PlayerPed;
        UIMenu usMenu;
        UIMenu ifMenu;

        private List<Vector3> USSpawnPoints = new List<Vector3>() { new Vector3(-1862.9f, 3084.9f, 32.81f), new Vector3(3094f, -4711f, 16f), new Vector3(364f, 4432f, 63f), new Vector3(51.55f, 3341, 37.5f), new Vector3(-1147.75f, -2802f, 40f), new Vector3(1731.3142f, 3236.9194f, 42.130f), new Vector3(1990.3926f, 3045.3071f, 47.2151f), new Vector3(-1059f, 4909f, 212f) };
        private List<Vector3> IFSpawnPoints = new List<Vector3>() { new Vector3(-221f, -1999f, 28f), new Vector3(-449.2337f, 6011.9072f, 32.4566f), new Vector3(-579.3627f, 5343.8628f, 71.2261f), new Vector3(1608.6385f, 3573.9229f, 38.7752f), new Vector3(1394.2538f, 3610.3188f, 38.9419f), new Vector3(2428.5596f, 4970.2217f, 42.347f) };

        public List<Gun> allGuns = new List<Gun>();

        public List<Gun> natoPistols = new List<Gun>();
        public Gun glock17 = new Gun();

        public List<Gun> taliPistols = new List<Gun>();
        public Gun pistol = new Gun();

        public List<Gun> natoShotguns = new List<Gun>();
        public Gun remington = new Gun();
        public Gun breaching = new Gun();

        public List<Gun> taliShotguns = new List<Gun>();
        public Gun shotgun = new Gun();

        public List<Gun> natoARs = new List<Gun>();
        public Gun m4 = new Gun();
        public Gun mk18 = new Gun();
        public Gun sa80 = new Gun();

        public List<Gun> taliARs = new List<Gun>();
        public Gun ak47 = new Gun();
        public Gun fal = new Gun();

        public List<Gun> natoHeavy = new List<Gun>();
        public Gun stinger = new Gun();
        public Gun lmg = new Gun();

        public List<Gun> taliHeavy = new List<Gun>();
        public Gun rpg = new Gun();
        public Gun pkm = new Gun();

        public List<Gun> natoSnipers = new List<Gun>();
        public Gun sniper = new Gun();

        public List<Gun> taliSnipers = new List<Gun>();
        public Gun dragnov = new Gun();

        public List<Gun> natoThrow = new List<Gun>();
        public Gun grenade = new Gun();
        public Gun flashbang = new Gun();
        public Gun flareGun = new Gun();

        public List<Gun> taliThrow = new List<Gun>();
        public Gun bomb = new Gun();

        public List<Gun> ammos = new List<Gun>();
        public Gun pistolAmmo = new Gun();
        public Gun shotgunAmmo = new Gun();
        public Gun arAmmo = new Gun();
        public Gun heavyAmmo = new Gun();
        public Gun sniperAmmo = new Gun();
        public Main()
        {
            MenuPool = new MenuPool();
            usMenu = new UIMenu("Weapon's Menu", "~b~Get all of your weaponory here!");
            usMenu.OnMenuStateChanged += Menu_OnMenuStateChanged;
            usMenu.OnItemSelect += Menu_OnItemSelect;
            ifMenu = new UIMenu("Weapon's Menu", "~b~Get all of your weaponory here!");
            ifMenu.OnMenuStateChanged += Menu_OnMenuStateChanged;
            ifMenu.OnItemSelect += Menu_OnItemSelect;
            MenuPool.Add(usMenu);
            MenuPool.Add(ifMenu);

            SetupGuns();

            RegisterCommand("parachute", new Action<int, List<object>, string>((source, args, raw) =>
            {
                GiveWeaponToPed(PlayerPedId(), (uint)GetHashKey("gadget_parachute"), 2, false, false);
                SetPlayerParachuteTintIndex(PlayerPedId(), 12);
            }), false);

            List<int> Googles = new List<int>(){ 142, 120, 118, 93 };
            RegisterCommand("nvg", new Action<int, List<object>, string>((source,args,raw) =>
            {
                bool hasHelmet = false;
                foreach(int google in Googles)
                {
                    Debug.WriteLine(GetPedPropIndex(playerPed.Handle, 0).ToString());
                    if (GetPedPropIndex(playerPed.Handle, 0) == google)
                    {
                        hasHelmet = true;
                        Debug.WriteLine("this player has a helmet");
                        break;
                    }
                    else
                    {
                        hasHelmet = false;
                    }
                }
                if (args[0].ToString() == "on" && hasHelmet)
                {
                    SetNightvision(true);
                }else if (args[0].ToString() == "on" && !hasHelmet)
                {
                    TriggerEvent("noticeme:Info", "You do not have the correct helmet for this!");
                }
                else 
                {
                    SetNightvision(false);
                }
            }), false);

            RegisterCommand("restoreloadout", new Action<int, List<object>, string>((source, args, raw) =>
            {
                foreach(WeaponHash gun in currentPedWeapons)
                {
                    foreach (Gun weapon in allGuns)
                    {
                        if ((WeaponHash)GetHashKey(weapon.spawncode) == gun)
                        {
                            GiveWeaponToPed(Game.PlayerPed.Handle, (uint)gun, weapon.ammo, false, false);
                            TriggerServerEvent("CSX:DiscordLog", Game.Player.Name + " has restored their loadout");
                        }
                    }
                }
            }), false);

             SetupItems();

            foreach (Vector3 spawnPoint in USSpawnPoints)
            {
                Blip blip = new Blip(AddBlipForCoord(spawnPoint.X, spawnPoint.Y, spawnPoint.Z));
                blip.Sprite = BlipSprite.Pistol;
                blip.Name = "NATO Armory";
                blip.IsShortRange = true;
                blip.Color = BlipColor.Blue;
            }
            foreach (Vector3 spawnPoint in IFSpawnPoints)
            {
                Blip blip = new Blip(AddBlipForCoord(spawnPoint.X, spawnPoint.Y, spawnPoint.Z));
                blip.Sprite = BlipSprite.AssaultRifle;
                blip.Name = "Taliban Armory";
                blip.IsShortRange = true;
                blip.Color = BlipColor.Red;
            }

            Tick += GenerateArmoriesTick;
            Tick += OnMenuDisplayTick;
        }

        void SetupGuns()
        {
            pistol.itemName = new UIMenuItem("Pistol");
            pistol.spawncode = "WEAPON_PISTOL";
            pistol.attachments = new string[] { "COMPONENT_AT_PI_FLSH" };
            taliPistols.Add(pistol);
            allGuns.Add(pistol);

            glock17.itemName = new UIMenuItem("Glock 17");
            glock17.spawncode = "WEAPON_PISTOL";
            glock17.attachments = new string[]{ "mm_g17_flash4", "mm_g17_frame01", "mm_g17_mag1", "mm_g17_barrel1", "mm_g17_slide1" };
            natoPistols.Add(glock17);
            allGuns.Add(glock17);

            remington.itemName = new UIMenuItem("M870 Remington");
            remington.spawncode = "WEAPON_PUMPSHOTGUN";
            natoShotguns.Add(remington);
            allGuns.Add(remington);
            breaching.itemName = new UIMenuItem("Breaching Shotgun");
            breaching.spawncode = "WEAPON_SAWNOFFSHOTGUN";
            natoShotguns.Add(breaching);
            allGuns.Add(breaching);

            shotgun.itemName = new UIMenuItem("Shotgun");
            shotgun.spawncode = "WEAPON_PUMPSHOTGUN_MK2";
            taliShotguns.Add(shotgun);
            allGuns.Add(shotgun);

            m4.itemName = new UIMenuItem("M4");
            m4.spawncode = "WEAPON_MM4";
            //natoARs.Add(m4);
            //allGuns.Add(m4);
            mk18.itemName = new UIMenuItem("MK18");
            mk18.spawncode = "WEAPON_MK18";
            mk18.attachments = new string[] { "mk18_flash4", "mk18_grip4", "mk18_mag4", "mk18_scope4" };
            //natoARs.Add(mk18);
            //allGuns.Add(mk18);
            sa80.itemName = new UIMenuItem("SA80");
            sa80.spawncode = "WEAPON_BULLPUPRIFLE_MK2";
            natoARs.Add(sa80);
            allGuns.Add(sa80);

            ak47.itemName = new UIMenuItem("AK47");
            ak47.spawncode = "WEAPON_ASSAULTRIFLE";
            taliARs.Add(ak47);
            allGuns.Add(ak47);
            fal.itemName = new UIMenuItem("FAL");
            fal.spawncode = "WEAPON_SPECIALCARBINE";
            taliARs.Add(fal);
            allGuns.Add(fal);

            stinger.itemName = new UIMenuItem("Stinger");
            stinger.spawncode = "WEAPON_HOMINGLAUNCHER";
            stinger.ammo = 10;
            natoHeavy.Add(stinger);
            allGuns.Add(stinger);
            lmg.itemName = new UIMenuItem("LMG");
            lmg.spawncode = "WEAPON_COMBATMG";
            lmg.ammo = 200;
            natoHeavy.Add(lmg);
            allGuns.Add(lmg);

            rpg.itemName = new UIMenuItem("RPG");
            rpg.spawncode = "WEAPON_RPG";
            rpg.ammo = 4;
            taliHeavy.Add(rpg);
            allGuns.Add(rpg);
            pkm.itemName = new UIMenuItem("PKM");
            pkm.spawncode = "WEAPON_MG";
            pkm.ammo = 90;
            taliHeavy.Add(pkm);
            allGuns.Add(pkm);

            sniper.itemName = new UIMenuItem("KAC M110");
            sniper.spawncode = "WEAPON_MARKSMANRIFLE";
            natoSnipers.Add(sniper);
            allGuns.Add(sniper);

            dragnov.itemName = new UIMenuItem("Dragnov");
            dragnov.spawncode = "WEAPON_RUSSIANSNIPER";
            dragnov.ammo = 20;
            taliSnipers.Add(dragnov);
            allGuns.Add(dragnov);

            grenade.itemName = new UIMenuItem("Grenade");
            grenade.spawncode = "WEAPON_GRENADE";
            natoThrow.Add(grenade);
            allGuns.Add(grenade);
            flashbang.itemName = new UIMenuItem("Flashbang");
            flashbang.spawncode = "WEAPON_FLASHBANG";
            natoThrow.Add(flashbang);
            allGuns.Add(flashbang);
            flareGun.itemName = new UIMenuItem("Flare Gun");
            flareGun.spawncode = "WEAPON_FLAREGUN";
            natoThrow.Add(flareGun);
            allGuns.Add(flareGun);

            bomb.itemName = new UIMenuItem("Sticky Bomb");
            bomb.spawncode = "WEAPON_STICKYBOMB";
            taliThrow.Add(bomb);
            allGuns.Add(bomb);

            pistolAmmo.itemName = new UIMenuItem("Pistol Ammo");
            pistolAmmo.spawncode = "WEAPON_PISTOL";
            pistolAmmo.ammo = 50;
            shotgunAmmo.itemName = new UIMenuItem("Shotgun Ammo");
            shotgunAmmo.ammo = 20;
            shotgunAmmo.spawncode = "WEAPON_PUMPSHOTGUN_MK2";
            arAmmo.itemName = new UIMenuItem("Rifle Ammo");
            arAmmo.ammo = 120;
            arAmmo.spawncode = "WEAPON_ASSAULTRIFLE";
            heavyAmmo.itemName = new UIMenuItem("Heavy Ammo/Rockets");
            heavyAmmo.ammo = 200;
            heavyAmmo.spawncode = "WEAPON_COMBATMG";
            sniperAmmo.itemName = new UIMenuItem("Sniper Ammo");
            sniperAmmo.ammo = 44;
            sniperAmmo.spawncode = "WEAPON_SNIPERRIFLE";
            ammos.Add(pistolAmmo);
            ammos.Add(shotgunAmmo);
            ammos.Add(arAmmo);
            ammos.Add(heavyAmmo);
            ammos.Add(sniperAmmo);

            foreach (Gun gun in natoPistols)
            {
                gun.ammo = 50;
            }
            foreach (Gun gun in taliPistols)
            {
                gun.ammo = 50;
            }
            foreach (Gun gun in natoShotguns)
            {
                gun.ammo = 24;
            }
            foreach (Gun gun in taliShotguns)
            {
                gun.ammo = 24;
            }
            foreach (Gun gun in natoARs)
            {
                gun.ammo = 120;
            }
            foreach (Gun gun in taliARs)
            {
                gun.ammo = 120;
            }
            foreach (Gun gun in natoSnipers)
            {
                gun.ammo = 44;
            }
            foreach (Gun gun in taliSnipers)
            {
                gun.ammo = 44;
            }
            foreach (Gun gun in natoThrow)
            {
                gun.ammo = 3;
            }
            foreach (Gun gun in taliThrow)
            {
                gun.ammo = 5;
            }
        }
        void SetupItems()
        {
            foreach (Gun gun in natoPistols)
            {
                usMenu.AddItem(gun.itemName);
            }
            foreach (Gun gun in natoShotguns)
            {
                usMenu.AddItem(gun.itemName);
            }
            foreach (Gun gun in natoARs)
            {
                usMenu.AddItem(gun.itemName);
            }
            foreach (Gun gun in natoSnipers)
            {
                usMenu.AddItem(gun.itemName);
            }
            foreach (Gun gun in natoHeavy)
            {
                usMenu.AddItem(gun.itemName);
            }
            foreach (Gun gun in natoThrow)
            {
                usMenu.AddItem(gun.itemName);
            }
            foreach (Gun gun in taliPistols)
            {
                ifMenu.AddItem(gun.itemName);
            }
            foreach (Gun gun in taliShotguns)
            {
                ifMenu.AddItem(gun.itemName);
            }
            foreach (Gun gun in taliARs)
            {
                ifMenu.AddItem(gun.itemName);
            }
            foreach (Gun gun in taliHeavy)
            {
                ifMenu.AddItem(gun.itemName);
            }
            foreach (Gun gun in taliSnipers)
            {
                ifMenu.AddItem(gun.itemName);
            }
            foreach (Gun gun in taliThrow)
            {
                ifMenu.AddItem(gun.itemName);
            }
            foreach (Gun gun in ammos)
            {
                ifMenu.AddItem(gun.itemName);
                usMenu.AddItem(gun.itemName);
            }
        }
        void Menu_OnMenuStateChanged(UIMenu oldMenu, UIMenu newMenu, MenuState state)
        {
            if (state == MenuState.Opened || state == MenuState.ChangeBackward || state == MenuState.ChangeForward)
            {
                foreach (Gun gun in allGuns)
                {
                    if (playerPed.Weapons.HasWeapon((WeaponHash)GetHashKey(gun.spawncode)))
                    {
                        gun.itemName.Enabled = false;
                    }
                    else
                    {
                        gun.itemName.Enabled = true;
                    }
                }
                foreach(Gun gun in ammos)
                {
                    if (GetAmmoInPedWeapon(PlayerPedId(), (uint)GetHashKey(gun.spawncode)) >= gun.ammo)
                    {
                        gun.itemName.Enabled = false;
                    }
                    else
                    {
                        gun.itemName.Enabled = true;
                    }
                }
            }
        }
        public void Menu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            string message = "~r~Unknown Selection";

            if (!selectedItem.Text.Contains("Ammo"))
            {
                Debug.WriteLine("This is a Gun");
                foreach (Gun gun in allGuns)
                {
                    if (selectedItem == gun.itemName && gun.spawncode != null)
                    {
                        if (GetAmmoInPedWeapon(PlayerPedId(), (uint)GetHashKey(gun.spawncode)) >= gun.ammo)
                        {
                            gun.ammo = 0;
                        }
                        GiveWeaponToPed(playerPed.Handle, (uint)GetHashKey(gun.spawncode), gun.ammo, false, false);

                        if (gun.attachments != null)
                        {
                            foreach (string attachment in gun.attachments)
                            {
                                Debug.WriteLine("This gun has attachments");
                                GiveWeaponComponentToPed(PlayerPedId(), (uint)GetHashKey(gun.spawncode), (uint)GetHashKey(attachment));
                            }
                        }
                        currentPedWeapons.Add((WeaponHash)GetHashKey(gun.spawncode));
                        message = gun.itemName.Text + " equipped!";
                        TriggerEvent("noticeme:Info", message);
                    }
                }
            }
            else
            {
                Debug.WriteLine("This is ammo");
                foreach (Gun gun in ammos)
                {
                    if (selectedItem == gun.itemName)
                    {
                        int ammoType = 0;
                        if (!playerPed.Weapons.HasWeapon((WeaponHash)GetHashKey(gun.spawncode)))
                        {
                            Debug.WriteLine("Player doesn't have the default weapon");
                            GiveWeaponToPed(playerPed.Handle, (uint)GetHashKey(gun.spawncode), 0, false, false);
                            ammoType = GetPedAmmoTypeFromWeapon(PlayerPedId(), (uint)GetHashKey(gun.spawncode));
                            Debug.WriteLine(ammoType.ToString() + " is the current type of ammo");
                            RemoveWeaponFromPed(PlayerPedId(), (uint)GetHashKey(gun.spawncode));
                            Debug.WriteLine("Removing the placeholder");
                        }
                        else
                        {
                            Debug.WriteLine("Player has the default weapon");
                            ammoType = GetPedAmmoTypeFromWeapon(PlayerPedId(), (uint)GetHashKey(gun.spawncode));
                        }
                        int ammoNeeded = GetPedAmmoByType(PlayerPedId(), ammoType);
                        if (ammoNeeded <= gun.ammo)
                        {
                            int ammoToGive = gun.ammo - ammoNeeded;
                            Debug.WriteLine("Giving the player the ammo");
                            AddAmmoToPedByType(PlayerPedId(), ammoType, ammoToGive);
                            TriggerEvent("noticeme:Info", gun.itemName.Text + " Equipped!");
                        }
                    }
                }
            }
        }
        private async Task GenerateArmoriesTick()
        {
            await BaseScript.Delay(1000);
            foreach (Vector3 spawnPoint in USSpawnPoints)
            {
                while (playerPed.IsInRangeOf(spawnPoint, 10f))
                {
                    await BaseScript.Delay(0);
                    World.DrawMarker(MarkerType.ThickChevronUp, spawnPoint, Vector3.Zero, Vector3.Zero, new Vector3(2f), Color.FromArgb(255, 255, 255, 255), true);
                    if (playerPed.IsInRangeOf(spawnPoint, 1f))
                    {
                        usMenu.Visible = true;
                        await BaseScript.Delay(500);
                    }
                }
            }

            foreach (Vector3 spawnPoint in IFSpawnPoints)
            {
                while (playerPed.IsInRangeOf(spawnPoint, 10f))
                {
                    await BaseScript.Delay(0);
                    World.DrawMarker(MarkerType.ThickChevronUp, spawnPoint, Vector3.Zero, Vector3.Zero, new Vector3(2f), Color.FromArgb(255, 255, 255, 255), true);
                    if (playerPed.IsInRangeOf(spawnPoint, 1f))
                    {
                        ifMenu.Visible = true;
                        await BaseScript.Delay(500);
                    }
                }
            }
        }
        private async Task OnMenuDisplayTick()
        {
            MenuPool.ProcessMenus();
            MenuPool.ProcessMouse();
        }
    }
}
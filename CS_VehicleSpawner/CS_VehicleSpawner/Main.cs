using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using NativeUI;
using System.Drawing;

namespace CS_VehicleSpawner
{
    public class Main : BaseScript
    {
        public List<Vector3> planeSpawns = new List<Vector3> { new Vector3(-1925.0131f, 3024.2686f, 32.8103f), new Vector3(-1989.5455f, 3060.1343f, 32.8103f), new Vector3(-2054.5549f, 3100.1824f, 32.8103f), new Vector3(-2117.3972f, 3141.7234f, 32.8101f), new Vector3(-2185.7915f, 3175.3875f, 32.8101f), new Vector3(-974.8336f, -3000.3665f, 13.9479f), new Vector3(-1271.4540f, -3381.1445f, 13.9401f), new Vector3(-1652.9960f, -3144.1394f, 13.9920f), new Vector3(3112.3462f, -4757.9512f, 15.2626f) };
        public List<Vector3> heliSpawns = new List<Vector3> { new Vector3(-1877.2827f, 2805.1323f, 32.8065f), new Vector3(-1859.7893f, 2795.1638f, 32.8065f), new Vector3(1611.8728f, 3266.5723f, 50.6501f), new Vector3(1628.6283f, 3272.0955f, 50.6502f), new Vector3(1645.3604f, 3276.7666f, 50.6502f), new Vector3(1714.4346f, 3231.5906f, 50.7001f), new Vector3(1730.9645f, 3235.9512f, 50.7001f) };
        public List<Vector3> laSpawns = new List<Vector3> { new Vector3(-1131.3867f, 4925.9814f, 219.6214f), new Vector3(1742.6345f, 3282.9468f, 41.0900f), new Vector3(1960.7603f, 3050.1287f, 46.8100f), new Vector3(2404.3232f, 3108.0789f, 48.1246f), new Vector3(-1156.0641f, -2890.2246f, 13.9456f) };
        public List<Vector3> haSpawns = new List<Vector3> { new Vector3(-1900.8583f, 3011.9294f, 32.8105f), new Vector3(-1925.0905f, 3129.9170f, 32.7116f), new Vector3(-2262.3596f, 3221.8708f, 32.8102f), new Vector3(-2348.1509f, 3367.1768f, 32.8329f) };
        public List<Vector3> taSpawns = new List<Vector3> { new Vector3(1373.1686f, 3618.9214f, 34.8898f), new Vector3(1602.9900f, 3610.8484f, 35.1453f), new Vector3(2507.8193f, 4976.7163f, 44.5562f) };
        public List<Vector3> thaSpawns = new List<Vector3> { new Vector3(130.9175f, 6602.9287f, 31.8539f), new Vector3(1935.7378f, 3837.0886f, 32.2363f), new Vector3(1123.5747f, 2651.3159f, 37.9958f) };
        public List<Vector3> civ = new List<Vector3> { new Vector3(-229.9979f, 6258.0718f, 31.4843f), new Vector3(1650.1332f, 4824.5923f, 42.0018f), new Vector3(2529.6189f, 4210.3071f, 40.0335f), new Vector3(1957.9351f, 3767.9265f, 32.1235f), new Vector3(1710.6144f, 3769.6567f, 34.3501f), new Vector3(886.6194f, 3652.9199f, 32.8488f), new Vector3(375.2812f, 2641.4387f, 44.4924f), new Vector3(1459.8257f, 1117.3448f, 114.3331f), new Vector3(1119.8237f, 243.2959f, 80.8556f), new Vector3(899.1345f, -57.4814f, 78.7573f), new Vector3(268.1894f, 74.3706f, 99.8912f), new Vector3(-50.9476f, 215.8350f, 106.5520f), new Vector3(230.6160f, -795.7734f, 30.5928f), new Vector3(-15.9609f, -1104.2322f, 26.6703f), new Vector3(-1567.7786f, -1018.6423f, 13.0168f), new Vector3(22.0440f, -1726.4426f, 29.3026f) };


        MenuPool MenuPool;
        Ped playerPed => Game.PlayerPed;
        UIMenu planeMenu;
        UIMenuItem tornado = new UIMenuItem("Tornado G.R.4");
        UIMenuItem atlas = new UIMenuItem("Atlas A400M");
        UIMenuItem voyager = new UIMenuItem("Voyager");
        UIMenuItem eurofighter = new UIMenuItem("Eurofighter Typhoon FGR4");
        UIMenuItem rc135w = new UIMenuItem("RC-135W");
        UIMenuItem trainingaircraft = new UIMenuItem("Hwk T2");

        UIMenu heliMenu = new UIMenu("Helicopter Menu", "~b~Select your Helicopters here!");
        UIMenuItem foxch47 = new UIMenuItem("Chinook");
        UIMenuItem mh65 = new UIMenuItem("Eurocopter MH65");
        UIMenuItem as332 = new UIMenuItem("Puma");
        UIMenuItem globe = new UIMenuItem("Globemaster");
        UIMenuItem maverick = new UIMenuItem("Bell 212");


        UIMenu BaLightArmorMenu = new UIMenu("Vehicle Menu", "~b~Select your Vehicles here!");
        UIMenuItem hmv = new UIMenuItem("Armored Humvee");
        UIMenuItem mrap = new UIMenuItem("MRAP");
        UIMenuItem foxhound = new UIMenuItem("Foxhound");
        UIMenuItem m9395 = new UIMenuItem("M9395");
        UIMenuItem cstfo2 = new UIMenuItem("Land Rover");

        UIMenu BAHeavyArmorMenu = new UIMenu("Vehicle Menu", "~b~Select your Vehicles here!");
        UIMenuItem wolfhound = new UIMenuItem("Wolfhound");
        UIMenuItem foxash = new UIMenuItem("Foxash");
        UIMenuItem fueltruck = new UIMenuItem("Fuel truck"); // m977hl
        UIMenuItem boxergtk = new UIMenuItem("Boxer");
        UIMenuItem rhino = new UIMenuItem("Tank");
        UIMenuItem mbt = new UIMenuItem("Challenger 2");
        UIMenuItem sesranger = new UIMenuItem("Medical Vehicle"); 
        UIMenuItem insurgent2 = new UIMenuItem("Medical Vehicle 2");
        UIMenuItem firetruck = new UIMenuItem("Firetruck");
        UIMenuItem sandcatmx = new UIMenuItem("Military Police Tactical Vehicle");
        UIMenuItem polqui = new UIMenuItem("Military Police Patrol Vehicle");

        UIMenu TALightMenu = new UIMenu("Vehicle Menu", "~b~Select your Vehicles here!");
        UIMenuItem insurgent = new UIMenuItem("Fahd");
        UIMenuItem hmvs = new UIMenuItem("HMV");
        UIMenuItem technical = new UIMenuItem("Technical");

        UIMenu TAHvMenu = new UIMenu("Vehicle Menu", "~b~Select your Vehicles here!");
        UIMenuItem ratel = new UIMenuItem("Ratel");
        UIMenuItem grad = new UIMenuItem("Grad");
        UIMenuItem bmp2 = new UIMenuItem("BMP-2");

        UIMenu CivMenu = new UIMenu("Vehicle Menu", "~b~Select your Vehicles here!");
        UIMenuItem caravan = new UIMenuItem("Caravan");
        UIMenuItem bison = new UIMenuItem("Bison");
        UIMenuItem bodhi = new UIMenuItem("Bodhi");
        UIMenuItem cavalcade = new UIMenuItem("Cavalcade");
        UIMenuItem corolla = new UIMenuItem("Corolla");
        UIMenuItem mulef = new UIMenuItem("Mulef");
        UIMenuItem rancherxl = new UIMenuItem("Rancher");
        UIMenuItem rebel2 = new UIMenuItem("Rebel");
        UIMenuItem sadler = new UIMenuItem("Sadler");
        UIMenuItem semiole = new UIMenuItem("Semiole");


        public Main()
        {
            MenuPool = new MenuPool();
            planeMenu = new UIMenu("Aircraft Menu", "~b~Collect your aircraft here!");
            planeMenu.OnItemSelect += planeMenu_OnItemSelect;
            heliMenu.OnItemSelect += planeMenu_OnItemSelect;
            BaLightArmorMenu.OnItemSelect += planeMenu_OnItemSelect;
            BAHeavyArmorMenu.OnItemSelect += planeMenu_OnItemSelect;
            TALightMenu.OnItemSelect += planeMenu_OnItemSelect;
            TAHvMenu.OnItemSelect += planeMenu_OnItemSelect;
            CivMenu.OnItemSelect += planeMenu_OnItemSelect;
            MenuPool.Add(BaLightArmorMenu);
            MenuPool.Add(BAHeavyArmorMenu);
            MenuPool.Add(planeMenu);
            MenuPool.Add(heliMenu);
            MenuPool.Add(TALightMenu);
            MenuPool.Add(TAHvMenu);
            MenuPool.Add(CivMenu);

            SetupItems();
            foreach (Vector3 spawnPoint in planeSpawns)
            {
                Blip blip = new Blip(AddBlipForCoord(spawnPoint.X, spawnPoint.Y, spawnPoint.Z));
                blip.Sprite = BlipSprite.Plane;
                blip.Name = "Aircraft Storage";
                blip.IsShortRange = true;
                blip.Color = BlipColor.Blue;
            }
            foreach (Vector3 spawnPoint in heliSpawns)
            {
                Blip blip = new Blip(AddBlipForCoord(spawnPoint.X, spawnPoint.Y, spawnPoint.Z));
                blip.Sprite = BlipSprite.Helicopter;
                blip.Name = "Helicotper Storage";
                blip.IsShortRange = true;
                blip.Color = BlipColor.Blue;
            }
            foreach (Vector3 spawnPoint in laSpawns)
            {
                Blip blip = new Blip(AddBlipForCoord(spawnPoint.X, spawnPoint.Y, spawnPoint.Z));
                blip.Sprite = BlipSprite.PersonalVehicleCar;
                blip.Name = "British Army Light Depot";
                blip.IsShortRange = true;
                blip.Color = BlipColor.Blue;
            }
            foreach (Vector3 spawnPoint in haSpawns)
            {
                Blip blip = new Blip(AddBlipForCoord(spawnPoint.X, spawnPoint.Y, spawnPoint.Z));
                blip.Sprite = BlipSprite.Tank;
                blip.Name = "British Army Heavy Depot";
                blip.IsShortRange = true;
                blip.Color = BlipColor.Blue;
            }
            foreach (Vector3 spawnPoint in taSpawns)
            {
                Blip blip = new Blip(AddBlipForCoord(spawnPoint.X, spawnPoint.Y, spawnPoint.Z));
                blip.Sprite = BlipSprite.Tank;
                blip.Name = "Taliban Vehicle Depot";
                blip.IsShortRange = true;
                blip.Color = BlipColor.Red;
            }
            foreach (Vector3 spawnPoint in thaSpawns)
            {
                Blip blip = new Blip(AddBlipForCoord(spawnPoint.X, spawnPoint.Y, spawnPoint.Z));
                blip.Sprite = BlipSprite.Tank;
                blip.Name = "Taliban Heavy Vehicle Depot";
                blip.IsShortRange = true;
                blip.Color = BlipColor.Red;
            }
            foreach (Vector3 spawnPoint in civ)
            {
                Blip blip = new Blip(AddBlipForCoord(spawnPoint.X, spawnPoint.Y, spawnPoint.Z));
                blip.Sprite = BlipSprite.CarWash;
                blip.Name = "Civillan Vehicle Depot";
                blip.IsShortRange = true;
                blip.Color = BlipColor.White;
            }
            Tick += GenerateSpawnsTick;
            Tick += OnMenuDisplayTick;

        }

        void SetupItems()
        {
            planeMenu.AddItem(tornado);
            planeMenu.AddItem(atlas);
            planeMenu.AddItem(voyager);
            planeMenu.AddItem(eurofighter);
            planeMenu.AddItem(rc135w);
            planeMenu.AddItem(trainingaircraft);

            heliMenu.AddItem(foxch47);
            heliMenu.AddItem(mh65);
            heliMenu.AddItem(as332);
            heliMenu.AddItem(globe);
            heliMenu.AddItem(maverick);

            BaLightArmorMenu.AddItem(hmv);
            BaLightArmorMenu.AddItem(mrap);
            BaLightArmorMenu.AddItem(foxhound);
            BaLightArmorMenu.AddItem(m9395);
            BaLightArmorMenu.AddItem(cstfo2);
            
            BAHeavyArmorMenu.AddItem(hmv);
            BAHeavyArmorMenu.AddItem(mrap);
            BAHeavyArmorMenu.AddItem(foxhound);
            BAHeavyArmorMenu.AddItem(m9395);
            BAHeavyArmorMenu.AddItem(cstfo2);
            BAHeavyArmorMenu.AddItem(sesranger);
            BAHeavyArmorMenu.AddItem(insurgent2);
            BAHeavyArmorMenu.AddItem(sandcatmx);
            BAHeavyArmorMenu.AddItem(polqui);
            BAHeavyArmorMenu.AddItem(firetruck);
            BAHeavyArmorMenu.AddItem(wolfhound);
            BAHeavyArmorMenu.AddItem(foxash);
            BAHeavyArmorMenu.AddItem(fueltruck);
            BAHeavyArmorMenu.AddItem(boxergtk);
            BAHeavyArmorMenu.AddItem(rhino);
            BAHeavyArmorMenu.AddItem(mbt);

            TALightMenu.AddItem(hmvs);
            TALightMenu.AddItem(technical);
            TALightMenu.AddItem(insurgent);

            TAHvMenu.AddItem(hmvs);
            TAHvMenu.AddItem(technical);
            TAHvMenu.AddItem(insurgent);
            TAHvMenu.AddItem(grad);
            TAHvMenu.AddItem(ratel);
            TAHvMenu.AddItem(bmp2);

            CivMenu.AddItem(caravan);
            CivMenu.AddItem(bison);
            CivMenu.AddItem(bodhi);
            CivMenu.AddItem(cavalcade);
            CivMenu.AddItem(corolla);
            CivMenu.AddItem(mulef);
            CivMenu.AddItem(rancherxl);
            CivMenu.AddItem(rebel2);
            CivMenu.AddItem(sadler);
            CivMenu.AddItem(semiole);
        }

        private async Task GenerateSpawnsTick()
        {
            await BaseScript.Delay(0);
            if(playerPed.IsInVehicle())
            {
                return;
            }
            foreach (Vector3 spawnPoint in planeSpawns)
            {
                while (playerPed.IsInRangeOf(spawnPoint, 10f))
                {
                    await BaseScript.Delay(0);
                    World.DrawMarker(MarkerType.PlaneModel, spawnPoint, Vector3.Zero, Vector3.Zero, new Vector3(2f), Color.FromArgb(255, 255, 255, 255), true);
                    if (playerPed.IsInRangeOf(spawnPoint, 1f))
                    {
                        planeMenu.Visible = true;
                        await BaseScript.Delay(500);
                    }
                }
            }
            foreach (Vector3 spawnPoint in heliSpawns)
            {
                while (playerPed.IsInRangeOf(spawnPoint, 10f))
                {
                    await BaseScript.Delay(0);
                    World.DrawMarker(MarkerType.HelicopterSymbol, spawnPoint, Vector3.Zero, Vector3.Zero, new Vector3(2f), Color.FromArgb(255, 255, 255, 255), true);
                    if (playerPed.IsInRangeOf(spawnPoint, 1f))
                    {
                        heliMenu.Visible = true;
                        await BaseScript.Delay(500);
                    }
                }
            }
            foreach (Vector3 spawnPoint in laSpawns)
            {
                while (playerPed.IsInRangeOf(spawnPoint, 10f))
                {
                    await BaseScript.Delay(0);
                    World.DrawMarker(MarkerType.CarSymbol, spawnPoint, Vector3.Zero, Vector3.Zero, new Vector3(2f), Color.FromArgb(255, 255, 255, 255), true);
                    if (playerPed.IsInRangeOf(spawnPoint, 1f))
                    {
                        BaLightArmorMenu.Visible = true;
                        await BaseScript.Delay(500);
                    }
                }
            }
            foreach (Vector3 spawnPoint in haSpawns)
            {
                while (playerPed.IsInRangeOf(spawnPoint, 10f))
                {
                    await BaseScript.Delay(0);
                    World.DrawMarker(MarkerType.CarSymbol, spawnPoint, Vector3.Zero, Vector3.Zero, new Vector3(2f), Color.FromArgb(255, 255, 255, 255), true);
                    if (playerPed.IsInRangeOf(spawnPoint, 1f))
                    {
                        BAHeavyArmorMenu.Visible = true;
                        await BaseScript.Delay(500);
                    }
                }
            }
            foreach (Vector3 spawnPoint in taSpawns)
            {
                while (playerPed.IsInRangeOf(spawnPoint, 10f))
                {
                    await BaseScript.Delay(0);
                    World.DrawMarker(MarkerType.CarSymbol, spawnPoint, Vector3.Zero, Vector3.Zero, new Vector3(2f), Color.FromArgb(255, 255, 255, 255), true);
                    if (playerPed.IsInRangeOf(spawnPoint, 1f))
                    {
                        TALightMenu.Visible = true;
                        await BaseScript.Delay(500);
                    }
                }
            }
            foreach (Vector3 spawnPoint in thaSpawns)
            {
                while (playerPed.IsInRangeOf(spawnPoint, 10f))
                {
                    await BaseScript.Delay(0);
                    World.DrawMarker(MarkerType.CarSymbol, spawnPoint, Vector3.Zero, Vector3.Zero, new Vector3(2f), Color.FromArgb(255, 255, 255, 255), true);
                    if (playerPed.IsInRangeOf(spawnPoint, 1f))
                    {
                        TAHvMenu.Visible = true;
                        await BaseScript.Delay(500);
                    }
                }
            }
            foreach (Vector3 spawnPoint in civ)
            {
                while (playerPed.IsInRangeOf(spawnPoint, 10f))
                {
                    await BaseScript.Delay(0);
                    World.DrawMarker(MarkerType.CarSymbol, spawnPoint, Vector3.Zero, Vector3.Zero, new Vector3(2f), Color.FromArgb(255, 255, 255, 255), true);
                    if (playerPed.IsInRangeOf(spawnPoint, 1f))
                    {
                        CivMenu.Visible = true;
                        await BaseScript.Delay(500);
                    }
                }
            }
        }
        public void planeMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (selectedItem == tornado)
            {
                SpawnPlane("tonadofra");
            }
            else if (selectedItem == atlas)
            {
                SpawnPlane("a400m");
            }
            else if (selectedItem == voyager)
            {
                SpawnPlane("voyager");
            }
            else if (selectedItem == eurofighter)
            {
                SpawnPlane("typhoon");
            }
            else if (selectedItem == rc135w)
            {
                SpawnPlane("rc135w");
            }
            else if (selectedItem == trainingaircraft)
            {
                SpawnPlane("hawkt2");
            }
            else if (selectedItem == foxch47)
            {
                SpawnPlane("foxch47");
            }
            else if (selectedItem == mh65)
            {
                SpawnPlane("mh65");
            }
            else if (selectedItem == as332)
            {
                SpawnPlane("as332");
            }
            else if (selectedItem == globe)
            {
                SpawnPlane("globe");
            }
            else if (selectedItem == maverick)
            {
                SpawnPlane("maverick");
            }
            else if (selectedItem == hmv)
            {
                SpawnPlane("uparmorhmvdes");
            }
            else if (selectedItem == mrap)
            {
                SpawnPlane("mrap");
            }
            else if (selectedItem == foxhound)
            {
                SpawnPlane("foxhound");
            }
            else if (selectedItem == m9395)
            {
                SpawnPlane("m9395");
            }
            else if (selectedItem == cstfo2)
            {
                SpawnPlane("ctsfo2");
            }
            else if (selectedItem == firetruck)
            {
                SpawnPlane("mtfft");
            }
            else if (selectedItem == sesranger)
            {
                SpawnPlane("sesranger");
            }
            else if (selectedItem == insurgent2)
            {
                SpawnPlane("insurgent2");
            }
            else if (selectedItem == sandcatmx)
            {
                SpawnPlane("sandcatmx");
            }
            else if (selectedItem == polqui)
            {
                SpawnPlane("polqui");
            }
            else if (selectedItem == foxash)
            {
                SpawnPlane("foxosh");
            }
            else if (selectedItem == fueltruck)
            {
                SpawnPlane("m977hl");
            }
            else if (selectedItem == wolfhound)
            {
                SpawnPlane("Wolfhound");
            }
            else if (selectedItem == boxergtk)
            {
                SpawnPlane("boxergtk");
            }
            else if (selectedItem == rhino)
            {
                SpawnPlane("rhino");
            }
            else if (selectedItem == mbt)
            {
                SpawnPlane("chal2mtkhaki");
            }
            else if (selectedItem == hmvs)
            {
                SpawnPlane("hmvspecial");
            }
            else if (selectedItem == technical)
            {
                SpawnPlane("technical");
            }
            else if (selectedItem == insurgent)
            {
                SpawnPlane("insurgent");
            }
            else if (selectedItem == grad)
            {
                SpawnPlane("grad");
            }
            else if (selectedItem == ratel)
            {
                SpawnPlane("ratel");
            }
            else if (selectedItem == bmp2)
            {
                SpawnPlane("bmp2");
            }

            else if (selectedItem == caravan)
            {
                SpawnPlane("2015caravanbb");
            }
            else if (selectedItem == bison)
            {
                SpawnPlane("bison3");
            }
            else if (selectedItem == bodhi)
            {
                SpawnPlane("bodhi2");
            }
            else if (selectedItem == cavalcade)
            {
                SpawnPlane("cavalcade");
            }
            else if (selectedItem == corolla)
            {
                SpawnPlane("corolla");
            }
            else if (selectedItem == mulef)
            {
                SpawnPlane("mulef");
            }
            else if (selectedItem == rancherxl)
            {
                SpawnPlane("rancherxl");
            }
            else if (selectedItem == rebel2)
            {
                SpawnPlane("rebel2");
            }
            else if (selectedItem == sadler)
            {
                SpawnPlane("sadler");
            }
            else if (selectedItem == semiole)
            {
                SpawnPlane("semiole");
            }
        }
        private async Task OnMenuDisplayTick()
        {
            MenuPool.ProcessMenus();
            MenuPool.ProcessMouse();
        }

        private async Task SpawnPlane(string modelName)
        {
            Model vehicle = modelName;

            while (!vehicle.IsLoaded)
            {
                await vehicle.Request(1000);
                await BaseScript.Delay(1);
            }

            if(Game.PlayerPed.IsInVehicle())
            {
                Game.PlayerPed.CurrentVehicle.Delete();
            }

            Vehicle car = await World.CreateVehicle(vehicle, playerPed.Position, 0);
            
            TaskWarpPedIntoVehicle(playerPed.Handle, car.Handle, -1);
        }
    }
}

using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;



namespace Sub2
{
    [BepInPlugin("null", "null", "1.0")]
    public class CheatMain : BaseUnityPlugin
    {
        public Color menuColor = Color.white;

        public bool setFood;
        public bool setWater;
        public bool setHealth;
        public bool setBattery;
        public bool setdayNightCycle;
        public bool setKnifeDamage;
        public bool setSpeedMode;

        public bool SpawnItems;
        // Меню
        public bool MenuState = true;
        public bool themesMenu = false;
        public bool TimeMenu = false;
        public bool SpawnMenu = false;
        public bool FlashlightColorMenu = false;

        public string menuMode = "Main";

        //Main
        Rect rectMenu = new Rect(10f, 10f, 330f, 210f);
        Rect dragMenu = new Rect(0f, 0f, 300f, 20f);
        //Themes
        Rect rectMenuThemes = new Rect(400f, 10f, 225f, 150f);
        Rect dragMenuThemes = new Rect(0f, 0f, 300f, 20f);
        //FlashlightColor
        Rect rectMenuFlashlight = new Rect(500f, 100f, 225f, 150f);
        Rect dragMenuFlashlight = new Rect(0f, 0f, 300f, 20f);
        //FlashlightState
        Rect rectMenuFlashlightState = new Rect(500f, 100f, 225f, 150f);
        Rect dragMenuFlashlightState = new Rect(0f, 0f, 300f, 20f);
        //Spawn
        Rect rectMenuSpawn = new Rect(10f, 280f, 225f, 180f);
        Rect dragMenuSpawn = new Rect(0f, 0f, 300f, 20f);


        public static bool setOxygen;

        public Oxygen PlayerOxygen;
        public Survival PlayerSurvival;
        public Player Igrok;
        public LiveMixin PlayerHealth;
        public GameObject[] prefabs;
        public PlayerMotor PlayerNoclip;
        public PlayerController PC;
        public PlayerTool ToolPlayer;
        public Knife knife;
        public Battery battery;
        public PlayerMotor Speed;
        public GameObject exosuitPrefab;
        public FlashLight fls;
        public CoffeeVendingMachine CoffeeMachine;
        public EnergyMixin Emixin;
        public DayNightCycle Time;
        public VendingMachine vendingMachine;
        public HeatBlade heatblade;

        public string discordLink = "https://discord.gg/gq85DuaqFW";

        public Vector2 scrollPosition;

        void OnGUI()
        {
            if (MenuState)
            {
                rectMenu = GUI.Window(0, rectMenu, (GUI.WindowFunction)Main, "SanProject - free");


                if (themesMenu)
                {
                    rectMenuThemes = GUI.Window(1, rectMenuThemes, (GUI.WindowFunction)Themes, "Themes");
                }

                if (FlashlightColorMenu)
                {
                    rectMenuFlashlight = GUI.Window(2, rectMenuFlashlight, (GUI.WindowFunction)Flashlight, "Flashlight color");
                }

                if (FlashlightColorMenu)
                {
                    rectMenuFlashlightState = GUI.Window(3, rectMenuFlashlightState, (GUI.WindowFunction)FlashlightState, "Flashlight state");
                }

                if (SpawnMenu)
                {
                    rectMenuSpawn = GUI.Window(4, rectMenuSpawn, (GUI.WindowFunction)Spawn, "Spawn");
                }
            }
        }

        void Main(int ID)
        {
            if (MenuState)
            {
                GUI.color = menuColor;

                GUILayout.BeginVertical(GUI.skin.box);

                if (GUI.Button(new Rect(115f, 30f, 100f, 25f), "Themes"))
                {
                    themesMenu = !themesMenu;
                }  

                if (GUI.Button(new Rect(10f, 150f, 310f, 25f), "Copy discord link"))
                {
                    GUIUtility.systemCopyBuffer = discordLink;
                }

                var foodActive = setFood ? "ON" : "OFF";
                if (GUI.Button(new Rect(10f, 30f, 100f, 25f), $"Food: {foodActive}"))
                {
                    setFood = !setFood;
                    PlayerSurvival = FindObjectOfType<Survival>();
                }

                var oxygenActive = setOxygen ? "ON" : "OFF";
                if (GUI.Button(new Rect(10f, 60f, 100f, 25f), $"Oxygen: {oxygenActive}"))
                {
                    setOxygen = !setOxygen;
                    Igrok = FindObjectOfType<Player>();
                    PlayerOxygen = Igrok.GetComponent<Oxygen>();
                }

                var waterActive = setWater ? "ON" : "OFF";
                if (GUI.Button(new Rect(10f, 90f, 100f, 25f), $"Water: {waterActive}"))
                {
                    setWater = !setWater;
                    PlayerSurvival = FindObjectOfType<Survival>();
                }

                var healthActive = setHealth ? "ON" : "OFF";
                if (GUI.Button(new Rect(10f, 120f, 100f, 25f), $"Health: {healthActive}"))
                {
                    setHealth = !setHealth;
                    Igrok = FindObjectOfType<Player>();
                    PlayerHealth = Igrok.GetComponent<LiveMixin>();
                }

                if (GUI.Button(new Rect(220f, 120f, 100f, 25f), "Coffee"))
                {
                    CoffeeMachine = FindObjectOfType<CoffeeVendingMachine>();
                    CoffeeMachine.useInterval = 0f;
                    CoffeeMachine.spawnDelay = 0f;
                }

                if (GUI.Button(new Rect(220f, 90f, 100f, 25f), "Vending"))
                {
                    vendingMachine = FindObjectOfType<VendingMachine>();
                    vendingMachine.useInterval = 0f;
                }

                if (GUI.Button(new Rect(115f, 60f, 100f, 25f), "Blade damage"))
                {
                    heatblade = FindObjectOfType<HeatBlade>();
                    heatblade.damage = heatblade.damage == 20f ? 999f : 20f;
                }

                var speedModeActive = setSpeedMode ? "ON" : "OFF";
                if (GUI.Button(new Rect(115f, 120f, 100f, 25f), $"Speed: {speedModeActive}"))
                {
                    setSpeedMode = !setSpeedMode;
                    Igrok = FindObjectOfType<Player>();
                    Speed = Igrok.GetComponent<PlayerMotor>();
                }

                if (GUI.Button(new Rect(220f, 30f, 100f, 25f), "Times"))
                {
                    TimeMenu = !TimeMenu;
                }

                if (TimeMenu)
                {
                    GUI.Box(new Rect(280f, 305f, 215f, 55f), "Time Menu");

                    if (GUI.Button(new Rect(285f, 330f, 100f, 25f), "Day"))
                    {
                        Time = FindObjectOfType<DayNightCycle>();
                        Time.sunSetTime = Time.sunSetTime == 0.875f ? 2f : 0.875f;
                        Time.sunRiseTime = 0.125f;
                    }

                    if (GUI.Button(new Rect(390f, 330f, 100f, 25f), "Night"))
                    {
                        Time = FindObjectOfType<DayNightCycle>();
                        Time.sunSetTime = Time.sunSetTime == 0.875f ? 0f : 0.875f;
                        Time.sunRiseTime = 0.125f;
                    }
                }

                if (GUI.Button(new Rect(220f, 60f, 100f, 25f), "Spawn"))
                {
                    SpawnMenu = !SpawnMenu;
                }            

                if (GUI.Button(new Rect(115f, 90f, 100f, 25f), "Flashlight"))
                {
                    FlashlightColorMenu = !FlashlightColorMenu;
                }        
            }
            GUILayout.EndVertical();
            GUI.DragWindow(dragMenu);
        }

        void Themes(int ID)
        {
            GUILayout.BeginVertical(GUI.skin.box);
            GUI.color = menuColor;

            if (GUI.Button(new Rect(10f, 30f, 100f, 25f), "Red"))
            {
                menuColor = new Color(207f / 255f, 10f / 255f, 10f / 255f);
            }

            if (GUI.Button(new Rect(10f, 60f, 100f, 25f), "Green"))
            {
                menuColor = new Color(12f / 255f, 250f / 255f, 72f / 255f);
            }

            if (GUI.Button(new Rect(10f, 90f, 100f, 25f), "Orange"))
            {
                menuColor = new Color(217f / 255f, 141f / 255f, 11f / 255f);
            }

            if (GUI.Button(new Rect(10f, 120f, 100f, 25f), "Purple"))
            {
                menuColor = new Color(91f / 255f, 8f / 255f, 168f / 255f);
            }
            //Второй столбик
            if (GUI.Button(new Rect(115f, 30f, 100f, 25f), "White"))
            {
                menuColor = new Color(250f / 255f, 250f / 255f, 250f / 255f);
            }

            if (GUI.Button(new Rect(115f, 60f, 100f, 25f), "Yellow"))
            {
                menuColor = new Color(252f / 255f, 236f / 255f, 8f / 255f);
            }

            if (GUI.Button(new Rect(115f, 90f, 100f, 25f), "Artic"))
            {
                menuColor = new Color(66f / 255f, 208f / 255f, 255f / 255f);
            }

            if (GUI.Button(new Rect(115f, 120f, 100f, 25f), "Pink"))
            {
                menuColor = new Color(209f / 255f, 113f / 255f, 204f / 255f);
            }

            GUILayout.EndVertical();
            GUI.DragWindow(dragMenuThemes);
        }

        void Flashlight(int ID)
        {
            GUI.color = menuColor;
            GUILayout.BeginVertical(GUI.skin.box);


            if (FlashlightColorMenu)
            {
                if (GUI.Button(new Rect(10f, 30f, 100f, 25f), "Red"))
                {
                    fls = FindObjectOfType<FlashLight>();
                    fls.flashLight.color = Color.red;
                }

                if (GUI.Button(new Rect(10f, 60f, 100f, 25f), "Blue"))
                {
                    fls = FindObjectOfType<FlashLight>();
                    fls.flashLight.color = Color.blue;
                }

                if (GUI.Button(new Rect(10f, 90f, 100f, 25f), "Yellow"))
                {
                    fls = FindObjectOfType<FlashLight>();
                    fls.flashLight.color = Color.yellow;
                }

                if (GUI.Button(new Rect(10f, 120f, 100f, 25f), "Green"))
                {
                    fls = FindObjectOfType<FlashLight>();
                    fls.flashLight.color = new Color(12f / 255f, 250f / 255f, 72f / 255f);
                }

                if (GUI.Button(new Rect(115f, 30f, 100f, 25f), "White"))
                {
                    fls = FindObjectOfType<FlashLight>();
                    fls.flashLight.color = Color.white;
                }

                if (GUI.Button(new Rect(115f, 60f, 100f, 25f), "Purple"))
                {
                    fls = FindObjectOfType<FlashLight>();
                    fls.flashLight.color = new Color(91f / 255f, 8f / 255f, 168f / 255f);
                }

                if (GUI.Button(new Rect(115f, 90f, 100f, 25f), "Pink"))
                {
                    fls = FindObjectOfType<FlashLight>();
                    fls.flashLight.color = new Color(209f / 255f, 113f / 255f, 204f / 255f);
                }

                if (GUI.Button(new Rect(115f, 120f, 100f, 25f), "Orange"))
                {
                    fls = FindObjectOfType<FlashLight>();
                    fls.flashLight.color = new Color(255f / 255f, 140f / 255f, 0f / 255f);
                }

                GUILayout.EndVertical();
                GUI.DragWindow(dragMenu);
            }
        }

        void FlashlightState(int ID)
        {
            GUI.color = menuColor;
            GUILayout.BeginVertical(GUI.skin.box);

            if (GUI.Button(new Rect(10f, 30f, 100f, 25f), "Intensity"))
            {
                fls = FindObjectOfType<FlashLight>();
                fls.flashLight.intensity = fls.flashLight.intensity == 1f ? 2f : 1f;
            }

            if (GUI.Button(new Rect(115f, 30f, 100f, 25f), "Range"))
            {
                fls = FindObjectOfType<FlashLight>();
                fls.flashLight.range = fls.flashLight.range == 50f ? 150f : 50f;
            }

            GUILayout.EndVertical();
            GUI.DragWindow(dragMenu);
        }

        void Spawn(int ID)
        {
            GUI.color = menuColor;
            GUILayout.BeginVertical(GUI.skin.box);
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            if (GUILayout.Button("Accumulator", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.Accumulator);
            }

            if (GUILayout.Button("Gold", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.Gold);
            }

            if (GUILayout.Button("AcidMushroom", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.AcidMushroom);
            }

            if (GUILayout.Button("AcidMushroomSpore", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.AcidMushroomSpore);
            }

            if (GUILayout.Button("AcidOld", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.AcidOld);
            }

            if (GUILayout.Button("AdvancedWiringKit", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.AdvancedWiringKit);
            }

            if (GUILayout.Button("Aerogel", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.Aerogel);
            }

            if (GUILayout.Button("AirBladder", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.AirBladder);
            }

            if (GUILayout.Button("AluminumOxide", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.AluminumOxide);
            }

            if (GUILayout.Button("AminoAcids", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.AminoAcids);
            }

            if (GUILayout.Button("AnalysisTreeOld", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.AnalysisTreeOld);
            }

            if (GUILayout.Button("Aquarium", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.Aquarium);
            }

            if (GUILayout.Button("AquariumBlueprint", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.AquariumBlueprint);
            }

            if (GUILayout.Button("AquariumFragment", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.AquariumFragment);
            }

            if (GUILayout.Button("ArcadeGorgetoy", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.ArcadeGorgetoy);
            }

            if (GUILayout.Button("AramidFibers", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.AramidFibers);
            }

            if (GUILayout.Button("Audiolog", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.Audiolog);
            }

            if (GUILayout.Button("BallClusters", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.BallClusters);
            }

            if (GUILayout.Button("BarnacleSuckers", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.BarnacleSuckers);
            }

            if (GUILayout.Button("BarTable", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.BarTable);
            }

            if (GUILayout.Button("BasaltChunk", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.BasaltChunk);
            }

            if (GUILayout.Button("BaseBioReactor", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.BaseBioReactor);
            }

            if (GUILayout.Button("BaseBioReactorFragment", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.BaseBioReactorFragment);
            }

            if (GUILayout.Button("BarnacleSuckers", GUILayout.Width(100f), GUILayout.Height(25f)))
            {
                CraftData.AddToInventory(TechType.BarnacleSuckers);
            }


            GUILayout.EndScrollView();

            GUILayout.EndVertical();
            GUI.DragWindow(dragMenuSpawn);
        }

        void Start()
        {
            var harmony = new Harmony("Text");
            harmony.PatchAll();
        }

        public bool noclip;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                MenuState = !MenuState;
            }

            if (setFood)
            {
                PlayerSurvival.food = 999f;
            }

            if (setWater)
            {
                PlayerSurvival.water = 999f;
            }

            if (setHealth)
            {
                PlayerHealth.health = 999f;
            }

            if (setSpeedMode)
            {
                Speed.forwardSprintModifier = 15f;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                noclip = !noclip;
                Igrok = FindObjectOfType<Player>();
                Igrok.GetComponent<Collider>().enabled = !noclip;
                PlayerNoclip = Igrok.GetComponent<PlayerMotor>();
                PlayerNoclip.gravity = noclip ? 0f : 12f;
                PlayerNoclip.usingGravity = !noclip;
            }

            if (noclip)
            {
                var speed = 30f;
                PlayerNoclip.playerController.transform.position += Input.GetAxis("Vertical") * Camera.main.transform.forward * Time.deltaTime * speed + Input.GetAxis("Horizontal") * Camera.main.transform.right * Time.deltaTime * speed;
            }
        }
    }
}

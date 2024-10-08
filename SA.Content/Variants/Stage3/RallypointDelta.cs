﻿using RoR2;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage3
{
    internal class RallypointDelta
    {
        public static void VanillaChanges()
        {
            DisableRallypointSnow();
            AddSnow(SnowType.Moderate);
        }

        public static void Overcast(RampFog fog, PostProcessVolume volume)
        {
            fog.fogColorEnd.value = new Color(0.3272f, 0.3711f, 0.4057f, 0.95f);
            fog.fogColorMid.value = new Color(0.2864f, 0.2667f, 0.3216f, 0.55f);
            fog.fogColorStart.value = new Color(0.2471f, 0.2471f, 0.2471f, 0.05f);
            fog.fogPower.value = 2f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.025f;
            fog.skyboxStrength.value = 0f;
            fog.fogIntensity.value = 1f;

            var sun = GameObject.Find("Directional Light (SUN)");
            var sunLight = Object.Instantiate(GameObject.Find("Directional Light (SUN)")).GetComponent<Light>();
            sun.SetActive(false);
            sun.name = "Shitty Not Working Sun";
            var water = Main.rpdTitanicWaterMat;
            GameObject.Find("HOLDER: Skybox").transform.Find("Water").localPosition = new Vector3(-1260, -66, 0);
            GameObject.Find("HOLDER: Skybox").transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = water;
            sunLight.color = Color.gray;
            sunLight.intensity = 1.3f;
            sunLight.name = "Directional Light (SUN)";

            DisableRallypointSnow();
            AddSnow(SnowType.Gigachad, 250f);
            var wind = GameObject.Find("WindZone");
            wind.transform.eulerAngles = new Vector3(30, 20, 0);
            var windZone = wind.GetComponent<WindZone>();
            windZone.windMain = 1;
            windZone.windTurbulence = 1;
            windZone.windPulseFrequency = 0.5f;
            windZone.windPulseMagnitude = 5f;
            windZone.mode = WindZoneMode.Directional;
            windZone.radius = 100;

            var bloom = volume.profile.GetSetting<Bloom>();
            bloom.intensity.value = 0.7f;
            bloom.threshold.value = 0.39f;
            bloom.softKnee.value = 0.7f;
        }

        public static void Night(RampFog fog, ColorGrading cgrade)
        {
            var sun = GameObject.Find("Directional Light (SUN)");
            sun.name = "Shitty Not Working Sun";
            sun.SetActive(false);
            Skybox.NightSky();

            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null && !lightBase.name.Contains("Light (SUN)"))
                {
                    light.type = LightType.Point;
                    light.shape = LightShape.Cone;
                    light.color = new Color32(233, 233, 190, 255);
                    light.intensity = 0.25f;
                    light.range = 65f;
                    if (lightBase.GetComponent<FlickerLight>() != null)
                    {
                        lightBase.GetComponent<FlickerLight>().enabled = false;
                    }
                    if (lightBase.GetComponent<LightIntensityCurve>() != null)
                    {
                        lightBase.GetComponent<LightIntensityCurve>().enabled = false;
                    }
                }
            }
            DisableRallypointSnow();
            AddSnow(SnowType.Gigachad, 250f);
            NightMaterials();
        }

        public static void Sunset(RampFog fog, PostProcessVolume volume)
        {
            Skybox.SunsetSky();
            fog.fogColorStart.value = new Color32(66, 66, 66, 50);
            fog.fogColorMid.value = new Color32(62, 18, 28, 150);
            fog.fogColorEnd.value = new Color32(160, 74, 61, 255);
            fog.skyboxStrength.value = 0.1f;
            fog.fogOne.value = 0.12f;
            fog.fogIntensity.overrideState = true;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.8f;

            var sun = GameObject.Find("Directional Light (SUN)");
            sun.SetActive(false);
            sun.name = "Shitty Not Working Sun";
            AddSnow(SnowType.Light, 250f);

            var bloom = volume.profile.GetSetting<Bloom>();
            bloom.active = false;
            SunsetMaterials();
        }

        public static void Titanic(RampFog fog, ColorGrading cgrade, PostProcessVolume volume)
        {
            Skybox.DaySky();
            fog.fogColorStart.value = new Color32(116, 153, 173, 4);
            fog.fogColorMid.value = new Color32(88, 130, 153, 40);
            fog.fogColorEnd.value = new Color32(77, 127, 152, 255);
            fog.skyboxStrength.value = 0.52f;
            // 0.75 1 0.6 1
            // cgrade.colorFilter.value = new Color32(178, 255, 230, 255);
            // cgrade.colorFilter.overrideState = true;
            var sun = GameObject.Find("Directional Light (SUN)");
            var sunLight = Object.Instantiate(GameObject.Find("Directional Light (SUN)")).GetComponent<Light>();
            sun.SetActive(false);
            sun.name = "Shitty Not Working Sun";
            sunLight.name = "Directional Light (SUN)";
            sunLight.color = new Color32(191, 255, 153, 255);
            sunLight.intensity = 1f;
            sunLight.shadowStrength = 0.7f;
            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null && !lightBase.name.Contains("Light (SUN)"))
                {
                    light.color = new Color32(255, 185, 0, 255);
                    light.intensity = 0.08f;
                    light.range = 4f;
                }
            }
            GameObject.Find("CAMERA PARTICLES: SnowParticles").SetActive(false);
            GameObject.Find("STATIC PARTICLES: Cave Entrance System").SetActive(false);
            GameObject.Find("HOLDER: ShippingCenter").transform.GetChild(3).gameObject.SetActive(false);

            var bloom = volume.profile.GetSetting<Bloom>();
            bloom.intensity.value = 0f;

            TitanicMaterials();
        }

        public static void TitanicMaterials()
        {
            var terrainMat = Main.rpdTitanicTerrainMat;
            var detailMat = Main.rpdTitanicDetailMat;
            var detailMat2 = Main.rpdTitanicDetailMat2;
            var water = Main.rpdTitanicWaterMat;

            if (terrainMat && detailMat && detailMat2 && water)
            {
                GameObject.Find("HOLDER: Skybox").transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = water;
                var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("Snow"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = terrainMat;
                            }
                        }
                        if (meshBase.name.Contains("Glacier") || meshBase.name.Contains("Stalagmite") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("CavePillar"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat;
                            }
                        }
                        if (meshBase.name.Contains("GroundMesh") || meshBase.name.Contains("GroundStairs") || meshBase.name.Contains("VerticalPillar") || meshBase.name.Contains("Human") || meshBase.name.Contains("Barrier"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat2;
                            }
                        }
                        if (meshBase.name.Contains("HumanChainLink"))
                        {
                            meshBase.SetActive(false);
                        }
                        // too early to change shrine/enemy skins/detail
                    }
                }
            }
        }

        public static void DisableRallypointSnow()
        {
            if (!Config.Config.WeatherEffects.Value)
            {
                return;
            }
            var snowParticles = GameObject.Find("CAMERA PARTICLES: SnowParticles").gameObject;
            snowParticles.SetActive(false);
        }

        public static void SunsetMaterials()
        {
            var waterMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/sulfurpools/matSPWaterYellow.mat").WaitForCompletion();

            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + waterMat);

            if (waterMat)
            {
                GameObject.Find("HOLDER: Skybox").transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = waterMat;
            }
        }

        public static void NightMaterials()
        {
            var terrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainVerySnowy.mat").WaitForCompletion();
            var waterMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/goldshores/matGSWater.mat").WaitForCompletion();
            var iceMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/snowyforest/matSFIce.mat").WaitForCompletion());
            iceMat.color = new Color32(242, 237, 254, 216);

            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + terrainMat);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + waterMat);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + iceMat);

            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            var water = GameObject.Find("HOLDER: Skybox").transform.GetChild(0);
            var ice = Object.Instantiate(water);
            ice.transform.position = new Vector3(-1260, -115, 0);
            if (terrainMat && waterMat && iceMat)
            {
                ice.GetComponent<MeshRenderer>().sharedMaterial = iceMat;
                water.GetComponent<MeshRenderer>().sharedMaterial = waterMat;
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("Snow"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = terrainMat;
                            }
                        }
                        if (meshBase.name.Contains("Stalagmite") && meshBase.GetComponent<Light>() == null)
                        {
                            meshBase.AddComponent<Light>();
                        }
                    }
                }
            }
        }
    }
}
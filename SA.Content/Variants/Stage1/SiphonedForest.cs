﻿using HG;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage1
{
    internal class SiphonedForest
    {
        public static void VanillaForest()
        {
            DisableSiphonedSnow();
            AddSnow(SnowType.Moderate);
            VanillaFoliage();
        }

        public static void ExtraSnowyForest(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(140, 140, 215, 25);
            fog.fogColorMid.value = new Color32(185, 185, 235, 200);
            fog.fogColorEnd.value = new Color32(255, 255, 255, 255);
            // fog.fogZero.value = 0;
            // fog.fogOne.value = 0.142f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(255, 255, 255, 255);
            sunLight.intensity = 1.3f;
            sunLight.shadowStrength = 0.7f;
            cgrade.colorFilter.value = new Color32(245, 245, 255, 30);
            cgrade.colorFilter.overrideState = true;

            DisableSiphonedSnow();
            AddSnow(SnowType.Gigachad);
            VanillaFoliage();
        }

        public static void NightForest(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(110, 110, 140, 45);
            fog.fogColorMid.value = new Color32(80, 80, 110, 85);
            fog.fogColorEnd.value = new Color32(20, 20, 35, 225);
            fog.skyboxStrength.value = 0.3f;
            fog.fogPower.value = 0.35f;
            fog.fogOne.value = 0.108f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var aurora = GameObject.Find("mdlSnowyForestAurora");
            aurora.SetActive(false);
            var godrays = GameObject.Find("Godrays");
            godrays.SetActive(false);
            sunLight.color = new Color32(110, 110, 180, 255);
            sunLight.intensity = 2.5f;
            sunLight.shadowStrength = 0.5f;
            cgrade.colorFilter.value = new Color32(110, 110, 140, 25);
            cgrade.colorFilter.overrideState = true;

            DisableSiphonedSnow();
            AddSnow(SnowType.Heavy);
            VanillaFoliage();
        }

        public static void CrimsonForest(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(180, 50, 50, 0);
            fog.fogColorMid.value = new Color32(50, 30, 30, 120);
            fog.fogColorEnd.value = new Color32(90, 30, 30, 225);
            fog.skyboxStrength.value = 0.08f;
            fog.fogPower.value = 0.35f;
            fog.fogOne.value = 0.108f;
            fog.fogZero.value = -0.007f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var aurora = GameObject.Find("mdlSnowyForestAurora");

            aurora.SetActive(false);
            sunLight.color = new Color32(180, 110, 110, 255);
            sunLight.intensity = 1.5f;
            sunLight.shadowStrength = 0.5f;
            cgrade.colorFilter.value = new Color32(255, 255, 255, 23);
            cgrade.colorFilter.overrideState = true;

            var skybox = GameObject.Find("HOLDER: Skybox").transform;
            var godrays = skybox.Find("Godrays").gameObject;
            godrays.SetActive(false);

            var cavePP1 = skybox.Find("SFPortalCard").gameObject;
            cavePP1.SetActive(false);

            var cavePP2 = skybox.Find("SFPortalCard (1)").gameObject;
            cavePP2.SetActive(false);

            DisableSiphonedSnow();
            AddRain(RainType.Typhoon, true);
            VanillaFoliage();
        }

        public static void MorningForest(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(117, 154, 255, 7);
            fog.fogColorMid.value = new Color32(111, 196, 248, 45);
            fog.fogColorEnd.value = new Color32(117, 154, 255, 255);
            fog.skyboxStrength.value = 0.1f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var aurora = GameObject.Find("mdlSnowyForestAurora");

            aurora.SetActive(false);
            sunLight.color = new Color32(205, 158, 90, 255);
            sunLight.intensity = 6f;
            sunLight.shadowStrength = 0.88f;
            cgrade.colorFilter.value = new Color32(111, 196, 248, 17);
            cgrade.colorFilter.overrideState = true;
            sunLight.transform.localEulerAngles = new Vector3(40, 153.0076f, 50f);

            DisableSiphonedSnow();
            AddSnow(SnowType.Light);
            VanillaFoliage();
        }

        public static void DesolateForest(RampFog fog, ColorGrading cgrade)
        {
            try { ApplyRoostMaterials(); } catch { SwapVariants.AesLog.LogError("Desolate Forest: Failed to change materials, trying again..."); } finally { ApplyRoostMaterials(); }
            fog.fogColorStart.value = new Color32(206, 117, 255, 5);
            fog.fogColorMid.value = new Color32(228, 144, 255, 40);
            fog.fogColorEnd.value = new Color32(178, 209, 255, 255);
            fog.fogOne.value = 2.4f;
            fog.skyboxStrength.value = 0.1f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var aurora = GameObject.Find("mdlSnowyForestAurora");

            aurora.SetActive(false);
            sunLight.color = new Color32(175, 146, 255, 255);
            sunLight.intensity = 13f;
            sunLight.shadowStrength = 0.3f;
            cgrade.colorFilter.value = new Color32(197, 233, 255, 255);
            cgrade.colorFilter.overrideState = true;
            sunLight.transform.localEulerAngles = new Vector3(48f, 333.0076f, 230f);

            var foliage = SceneManager.GetActiveScene().GetRootGameObjects()[3];
            if (foliage)
            {
                var icicles = foliage.transform.GetChild(5);

                icicles.gameObject.SetActive(false);
            }

            DisableSiphonedSnow();
            AddRain(RainType.Rainstorm);
            RoostFoliage();
        }

        public static void ApplyRoostMaterials()
        {
            var normal = Addressables.LoadAssetAsync<Texture2D>("RoR2/Base/Common/texNormalBumpyRock.jpg").WaitForCompletion();
            var side = Main.stageaesthetic.LoadAsset<Texture2D>("Assets/StageAesthetic/texRockSide.png");
            var top = Main.stageaesthetic.LoadAsset<Texture2D>("Assets/StageAesthetic/texRockTop.png");

            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/blackbeach/matBbTerrain.mat").WaitForCompletion());
            terrainMat.color = new Color32(174, 153, 129, 255);
            terrainMat.SetFloat("_RedChannelBias", 1.2f);
            terrainMat.SetFloat("_BlueChannelBias", 1.3f);
            terrainMat.SetFloat("_GreenChannelBias", 1.87f);
            terrainMat.SetFloat("_TextureFactor", 0.06f);
            terrainMat.SetFloat("_NormalStrength", 0.3f);
            terrainMat.SetFloat("_GreenChannelSpecularStrength", 0.002f);
            terrainMat.SetFloat("_Depth", 0.1f);
            terrainMat.SetInt("_RampInfo", 5);
            terrainMat.SetTexture("_RedChannelSideTex", side);
            terrainMat.SetTexture("_RedChannelTopTex", top);
            terrainMat.SetTexture("_NormalTex", normal);

            var detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/blackbeach/matBbBoulder.mat").WaitForCompletion();
            var detailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Temple.mat").WaitForCompletion());
            detailMat2.color = new Color32(18, 79, 40, 255);
            var water = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonWaterBridge.mat").WaitForCompletion();
            var detailMat4 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTree.mat").WaitForCompletion());
            detailMat4.color = new Color32(205, 104, 12, 128);
            detailMat4.SetFloat("_Depth", 0.714f);
            detailMat4.SetFloat("_NormalStrength", 0.25f);
            detailMat4.SetFloat("_RedChannelBias", 0.17f);
            detailMat4.SetFloat("_RedChannelSpecularStrength", 0.0338f);
            detailMat4.SetFloat("_GreenChannelBias", 0f);
            detailMat4.SetTextureScale("_NormalTex", new Vector2(0.3f, 0.3f));
            var detailMat5 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTree.mat").WaitForCompletion());
            detailMat5.color = new Color32(255, 255, 255, 255);
            var detailMat6 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Captain/matCaptainSupplyDropEquipmentRestock.mat").WaitForCompletion());
            detailMat6.color = new Color32(80, 162, 90, 255);

            if (terrainMat && detailMat && detailMat2 && water && detailMat4 && detailMat5 && detailMat6)
            {
                var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                var particleList = Object.FindObjectsOfType(typeof(ParticleSystem)) as ParticleSystem[];
                var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
                foreach (Light light in lightList)
                {
                    var lightBase = light.gameObject;
                    if (lightBase)
                    {
                        light.color = new Color32(53, 56, 148, 255);
                        light.intensity = 5f;
                        light.range = 120f;
                        var flickerLight = light.GetComponent<FlickerLight>();
                        if (flickerLight)
                            flickerLight.enabled = false;
                    }
                }
                foreach (ParticleSystem ps in particleList)
                {
                    var particleBase = ps.gameObject;
                    if (particleBase)
                    {
                        if (particleBase.name.Contains("Fire") || particleBase.name.Contains("HeatGas"))
                        {
                            particleBase.SetActive(false);
                        }
                    }
                }
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("SnowPile"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = terrainMat;
                            }
                        }
                        if (meshBase.name == "meshSnowyForestGiantTreesTops")
                        {
                            meshBase.gameObject.SetActive(false);
                        }
                        if (meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rock") || meshBase.name.Contains("mdlSFCeilingSpikes"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat;
                            }
                        }
                        if (meshBase.name.Contains("RuinGate") || meshBase.name.Contains("meshSnowyForestAqueduct") || meshBase.name == "meshSnowyForestFirepitFloor" || meshBase.name.Contains("meshSnowyForestFirepitRing") || meshBase.name.Contains("meshSnowyForestFirepitJar") || (meshBase.name.Contains("meshSnowyForestPot") && meshBase.name != "meshSnowyForestPotSap") || meshBase.name.Contains("mdlSFHangingLantern") || meshBase.name.Contains("mdlSFBrokenLantern") || meshBase.name.Contains("meshSnowyForestCrate"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat2;
                            }
                        }
                        if (meshBase.name.Contains("meshSnowyForestTreeLog") || meshBase.name.Contains("meshSnowyForestTreeTrunk") || meshBase.name.Contains("meshSnowyForestGiantTrees") || meshBase.name.Contains("meshSnowyForestSurroundingTrees"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat4;
                            }
                        }
                        if (meshBase.name.Contains("mdlSnowyForestTreeStump"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat5;
                                mr.sharedMaterials[1] = detailMat5;
                            }
                        }
                        if (meshBase.name.Contains("mdlSFHangingLanternRope") || meshBase.name.Contains("mdlSFLanternRope"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat6;
                            }
                        }
                        if (meshBase.name == "meshSnowyForestFirepitFloor (1)" || meshBase.name.Contains("meshSnowyForestSap") || meshBase.name.Contains("goo"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = water;
                            }
                        }
                        if (meshBase.name == "meshSnowyForestPotSap")
                        {
                            meshBase.SetActive(false);
                        }
                    }
                }
            }
        }

        public static void DisableSiphonedSnow()
        {
            if (!SwapVariants.WeatherEffects.Value)
            {
                return;
            }
            var skybox = GameObject.Find("HOLDER: Skybox").transform;
            var snowParticles = skybox.Find("CAMERA PARTICLES: SnowParticles").gameObject;
            snowParticles.SetActive(false);
        }

        public static void VanillaFoliage()
        {
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("spmGPGrass_LOD0"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial.color = new Color32(168, 168, 141, 255);
                        }
                    }
                    if (meshBase.name.Contains("spmBbDryBush_LOD0"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial.color = new Color32(125, 125, 128, 255);
                            if (mr.sharedMaterials.Length >= 2)
                            {
                                mr.sharedMaterials[1].color = new Color32(125, 125, 128, 255);
                            }
                        }
                    }
                }
            }
        }

        public static void RoostFoliage()
        {
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("spmGPGrass_LOD0"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial.color = new Color32(43, 66, 48, 255);
                        }
                        meshBase.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    }
                    if (meshBase.name.Contains("spmBbDryBush_LOD0"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial.color = new Color32(81, 55, 101, 255);
                            if (mr.sharedMaterials.Length >= 2)
                            {
                                mr.sharedMaterials[1].color = new Color32(25, 87, 71, 255);
                            }
                        }
                        meshBase.transform.localScale = new Vector3(2f, 2f, 2f);
                    }
                }
            }
        }
    }
}
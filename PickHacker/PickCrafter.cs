using HarmonyLib;

using MelonLoader;

using System.Reflection;

using UnityEngine;

namespace PickCrafter
{
    public class PickCrafter : MelonMod
    {
        private static bool _disableExplsionEffect = false;

        public override void OnInitializeMelon()
        {
            var explosionMethod = typeof(BlockLevelManager).GetMethod("BlockExplosionEffect");

            var explosionMethodPatch = typeof(PickCrafter).GetMethod(nameof(BlockExplosionEffectPatch), BindingFlags.NonPublic | BindingFlags.Static);

            HarmonyInstance.Patch(explosionMethod, new HarmonyMethod(explosionMethodPatch));
        }

        public override void OnUpdate()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                RunicDust.Award(999_999_999, RunicDust.RunicRewardOrigins.FreeChest);
            }

            if(Input.GetKeyDown(KeyCode.O)) 
            {
                PickaxePowerController.ResetCoolDown();
            }

            if(Input.GetKeyDown(KeyCode.G)) 
            {
                for(int i = 0; i < 10000; i++)
                {
                    BlockLevelManager.Instance.BlockLevelHit(-1.0, passiveHit: true);
                }
            }

            if(Input.GetKeyDown(KeyCode.B)) 
            {
                GameData.EarnPicks(99_999_999_999_999_999_999d);
            }

            if(Input.GetKeyDown(KeyCode.F))
            {
                PrestigeController.ExecutePrestige();
            }

            if(Input.GetKeyDown(KeyCode.V))
            {
                EnderPearls.Award(100_000);
            }

            if(Input.GetKeyDown(KeyCode.M))
            {
                FurnaceController.Instance.CompleteCurrentOperation(FurnaceController.CraftQueueSize);
            }

            if(Input.GetKeyDown(KeyCode.N))
            {
                _disableExplsionEffect = !_disableExplsionEffect;
            }
        }

        private static bool BlockExplosionEffectPatch()
        {
            return !_disableExplsionEffect;
        }

    }
}

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessAdjustments : MonoBehaviour
{
    public Volume volume;


    private LensDistortion lensDistortion;

    private void Awake()
    {
        if (volume.profile.TryGet<LensDistortion>(out LensDistortion distortion))
        {
            lensDistortion = distortion;
        }
    }

    public void SetLensDistortion(float value)
    {
        lensDistortion.intensity.value = value;
    }
}

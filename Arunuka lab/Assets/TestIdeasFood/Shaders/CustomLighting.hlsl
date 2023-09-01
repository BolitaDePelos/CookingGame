#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

void MainLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out float DistanceAtten, out float ShadowAtten)
{
#if SHADERGRAPH_PREVIEW
    Direction = float3(0.5, 0.5, 0);
    Color = 1;
    DistanceAtten = 1;
    ShadowAtten = 1;
#else
#if SHADOWS_SCREEN
    float4 clipPos = TransformWorldToHClip(WorldPos);
    float4 shadowCoord = ComputeScreenPos(clipPos);
#else
    float4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
    Light mainLight = GetMainLight(shadowCoord);
    Direction = mainLight.direction;
    Color = mainLight.color;
    DistanceAtten = mainLight.distanceAttenuation;
    ShadowSamplingData shadowSamplingData = GetMainLightShadowSamplingData();
    float shadowStrength = GetMainLightShadowStrength();
    ShadowAtten = SampleShadowmap(shadowCoord, TEXTURE2D_ARGS(_MainLightShadowmapTexture, sampler_MainLightShadowmapTexture), shadowSamplingData, shadowStrength, false); 
#endif
}

void MainLight_half(float3 WorldPos, out half3 Direction, out half3 Color, out half DistanceAtten, out half ShadowAtten)
{
#if SHADERGRAPH_PREVIEW
    Direction = half3(0.5, 0.5, 0);
    Color = 1;
    DistanceAtten = 1;
    ShadowAtten = 1;
#else
#if SHADOWS_SCREEN
    half4 clipPos = TransformWorldToHClip(WorldPos);
    half4 shadowCoord = ComputeScreenPos(clipPos);
#else
    half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
    Light mainLight = GetMainLight(shadowCoord);
    Direction = mainLight.direction;
    Color = mainLight.color;
    DistanceAtten = mainLight.distanceAttenuation;
    ShadowSamplingData shadowSamplingData = GetMainLightShadowSamplingData();
    float shadowStrength = GetMainLightShadowStrength();
    ShadowAtten = SampleShadowmap(shadowCoord, TEXTURE2D_ARGS(_MainLightShadowmapTexture, sampler_MainLightShadowmapTexture), shadowSamplingData, shadowStrength, false); 
#endif
}

void DirectSpecular_float(float3 Specular, float Smoothness, float3 Direction, float3 Color, float3 WorldNormal, float3 WorldView, out float3 Out)
{
#if SHADERGRAPH_PREVIEW
    Out = 0;
#else
    Smoothness = exp2(10 * Smoothness + 1);
    WorldNormal = normalize(WorldNormal);
    WorldView = SafeNormalize(WorldView);
    Out = LightingSpecular(Color, Direction, WorldNormal, WorldView, float4(Specular, 0), Smoothness);
#endif
}

void DirectSpecular_half(half3 Specular, half Smoothness, half3 Direction, half3 Color, half3 WorldNormal, half3 WorldView, out half3 Out)
{
#if SHADERGRAPH_PREVIEW
    Out = 0;
#else
    Smoothness = exp2(10 * Smoothness + 1);
    WorldNormal = normalize(WorldNormal);
    WorldView = SafeNormalize(WorldView);
    Out = LightingSpecular(Color, Direction, WorldNormal, WorldView,half4(Specular, 0), Smoothness);
#endif
}

void AdditionalLights_float(float3 SpecColor, float Smoothness, float3 WorldPosition, float3 WorldNormal, float3 WorldView, out float3 Diffuse, out float3 Specular)
{
    float3 diffuseColor = 0;
    float3 specularColor = 0;

#ifndef SHADERGRAPH_PREVIEW
    Smoothness = exp2(10 * Smoothness + 1);
    WorldNormal = normalize(WorldNormal);
    WorldView = SafeNormalize(WorldView);
    int pixelLightCount = GetAdditionalLightsCount();
    for (int i = 0; i < pixelLightCount; ++i)
    {
        Light light = GetAdditionalLight(i, WorldPosition);
        half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
        diffuseColor += LightingLambert(attenuatedLightColor, light.direction, WorldNormal);
        specularColor += LightingSpecular(attenuatedLightColor, light.direction, WorldNormal, WorldView, float4(SpecColor, 0), Smoothness);
    }
#endif

    Diffuse = diffuseColor;
    Specular = specularColor;
}

void AdditionalLights_half(half3 SpecColor, half Smoothness, half3 WorldPosition, half3 WorldNormal, half3 WorldView, out half3 Diffuse, out half3 Specular)
{
    half3 diffuseColor = 0;
    half3 specularColor = 0;

#ifndef SHADERGRAPH_PREVIEW
    Smoothness = exp2(10 * Smoothness + 1);
    WorldNormal = normalize(WorldNormal);
    WorldView = SafeNormalize(WorldView);
    int pixelLightCount = GetAdditionalLightsCount();
    for (int i = 0; i < pixelLightCount; ++i)
    {
        Light light = GetAdditionalLight(i, WorldPosition);
        half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
        diffuseColor += LightingLambert(attenuatedLightColor, light.direction, WorldNormal);
        specularColor += LightingSpecular(attenuatedLightColor, light.direction, WorldNormal, WorldView, half4(SpecColor, 0), Smoothness);
    }
#endif

    Diffuse = diffuseColor;
    Specular = specularColor;
}


float2 Rotate(float2 UV, float2 Center, float Rotation)
{
    //rotation matrix
    Rotation = Rotation * (3.1415926f / 180.0f);
    UV -= Center;
    float s = sin(Rotation);
    float c = cos(Rotation);

    //center rotation matrix
    float2x2 rMatrix = float2x2(c, -s, s, c);
    rMatrix *= 0.5;
    rMatrix += 0.5;
    rMatrix = rMatrix * 2 - 1;

    //multiply the UVs by the rotation matrix
    UV.xy = mul(UV.xy, rMatrix);
    UV += Center;

    return UV;
}

void ProceduralCrossHatching_half(Texture2D hatchMap, SamplerState state, half2 scale, float2 uv, half ndotl, half hatchingRotation, half hatchingDrawStrength, half hatchingSmoothness, half hatchingDensity, out half Out)
{
    half hatching = 1.0;
    half p = 1.0;
    half realNdotL = 1 - ndotl;
    float2 uv1 = Rotate(uv.xy, float2(0.5, 0.5), hatchingRotation);
    float2 uv2 = Rotate(uv1.xy, float2(0.5, 0.5), 90);
    float2 currentUV = uv1;
    float currentScale = 1.0;
    half sizeUpper = ndotl * hatchingDrawStrength * 0.1;

    const int count = 15;
    for (int i = 0; i < count; i++)
    {
        currentUV = lerp(uv1, uv2, i % 2);
        float g = SAMPLE_TEXTURE2D_LOD(hatchMap, state, scale * currentUV * currentScale, 0).r;
        g = 1.0 - smoothstep(0.5 - hatchingSmoothness, 0.5 + hatchingSmoothness + 0.1, sizeUpper - g);
        hatching = min(g, hatching);
        currentScale *= 1.2;

        if ((half)i > (smoothstep(0.5, 0.5 + (2 - hatchingDensity), ndotl) * hatchingDrawStrength))
        {
            break;
        }
    }
    Out = hatching;
}

void ProceduralCrossHatching_float(Texture2D hatchMap, SamplerState state, half2 scale, float2 uv, half ndotl, half hatchingRotation, half hatchingDrawStrength, half hatchingSmoothness, half hatchingDensity, out half Out)
{
    half hatching = 1.0;
    half p = 1.0;
    half realNdotL = 1 - ndotl;
    float2 uv1 = Rotate(uv.xy, float2(0.5, 0.5), hatchingRotation);
    float2 uv2 = Rotate(uv1.xy, float2(0.5, 0.5), 90);
    float2 currentUV = uv1;
    float currentScale = 1.0;
    half sizeUpper = ndotl * hatchingDrawStrength * 0.1;

    const int count = 15;
    for (int i = 0; i < count; i++)
    {
        currentUV = lerp(uv1, uv2, i % 2);
        float g = SAMPLE_TEXTURE2D_LOD(hatchMap, state, scale * currentUV * currentScale, 0).r;
        g = 1.0 - smoothstep(0.5 - hatchingSmoothness, 0.5 + hatchingSmoothness + 0.1, sizeUpper - g);
        hatching = min(g, hatching);
        currentScale *= 1.2;

        if ((half)i > (smoothstep(0.5, 0.5 + (2 - hatchingDensity), ndotl) * hatchingDrawStrength))
        {
            break;
        }
    }
    Out = hatching;
}


#endif
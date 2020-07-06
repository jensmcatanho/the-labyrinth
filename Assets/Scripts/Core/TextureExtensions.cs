using UnityEngine;
using UnityEngine.Networking;

public static class TextureExtensions {

    public static Texture2D Blend(this Texture2D texture, Texture2D textureToBlend) {
        var finalTexture = new Texture2D(texture.width, texture.height);

        for (int u = 0; u < texture.width; u++)
            for (int v = 0; v < texture.height; v++) {
                var currentColor = texture.GetPixel(u, v);
                var incomingColor = textureToBlend.GetPixel(u, v);
                var finalColor = currentColor;

//                finalTexture.SetPixel(u, v, finalColor);
                finalTexture.SetPixel(u, v, Color.Lerp(texture.GetPixel(u, v), textureToBlend.GetPixel(u, v), 0.5f));
            }
        
        finalTexture.Apply();
        return finalTexture;
    }

    public static Texture2D ToTexture2D(this RenderTexture renderTexture) {
        RenderTexture.active = renderTexture;
        
        var texture = new Texture2D(renderTexture.width, renderTexture.height);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        return texture;
    }
}
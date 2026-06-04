using UnityEngine;
using TMPro;

public class RGBText : MonoBehaviour
{
    private TextMeshProUGUI texto;
    
    [Header("Configurações do Efeito")]
    [Tooltip("Velocidade da mudança de cores. Quanto maior, mais rápido vibra.")]
    public float velocidade = 1f;

    void Start()
    {
        // Pega o componente de texto do próprio objeto
        texto = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        
        if (this == null || texto == null) return;

        float matiz = (Time.unscaledTime * velocidade) % 1f;
        Color corRGB = Color.HSVToRGB(matiz, 1f, 1f);

        if (texto != null && texto.gameObject != null)
        {
            texto.color = corRGB;
        }
    }
}
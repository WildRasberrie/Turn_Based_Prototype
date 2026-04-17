using UnityEngine;
using UnityEngine.UI;
public class HealthColorCHanger : MonoBehaviour
{
    [SerializeField] Slider healthBar;

    [SerializeField] Image healthColor;

    void Update()
    {
        if (healthBar.value > 75) healthColor.color = Color.green;
        if (healthBar.value < 75) healthColor.color = Color.orange;
        if (healthBar.value < 25) healthColor.color = Color.red;

    }
}

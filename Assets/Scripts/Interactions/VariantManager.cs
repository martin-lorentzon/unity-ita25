using UnityEngine;
using System;

public class VariantManager : MonoBehaviour
{

    public GameObject[] variants;

    int active_index = 0;

    public void SwapVariant()
    {
        active_index += 1;

        if (active_index > variants.Length - 1)  // Index (0, 1, 2) Längd 3 finns inte som index, därav tar vi bort 1
            active_index = 0;


        foreach (GameObject variant in variants)
        {
            int idx = Array.IndexOf(variants, variant); // Letar reda på objektets plats i arrayen. Kräver "using System;"

            if (idx == active_index)
                variant.SetActive(true);
            else
                variant.SetActive(false);
        }
    }
}

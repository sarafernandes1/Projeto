using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzBastao : MonoBehaviour
{
    public InputController inputController;
    public Material luzbranca, luzazul, luzvermelha, luzamarela, luzlaranja, luzvento, luzverde;
    Material luz;
    public Material material;
    public SkinnedMeshRenderer skinned;

    public static int numero_feitico;

    void Start()
    {
        luz = gameObject.GetComponent<Material>();
        numero_feitico = -1;
    }

    void Update()
    {
        MudarLuzBastao();
    }

    public void MudarLuzBastao()
    {
        switch (numero_feitico)
        {
            case -2:
                TempoLuz();
                numero_feitico = -1;
                break;
            case -1:
                TempoLuz();
                break;
            case 0:
               //material = luzazul;
                skinned.materials[1].CopyPropertiesFromMaterial(luzazul);
                break;
            case 1:
                //  material = luzvermelha;
                skinned.materials[1].CopyPropertiesFromMaterial(luzvermelha);
                break;
            case 2:
                //material = luzamarela;
                skinned.materials[1].CopyPropertiesFromMaterial(luzamarela);
                break;
            case 3:
                //material = luzlaranja;
                skinned.materials[1].CopyPropertiesFromMaterial(luzlaranja);
                break;
            case 4:
                //material = luzvento;
                skinned.materials[1].CopyPropertiesFromMaterial(luzvento);
                break;
            case 5:
                //material = luzverde;
                skinned.materials[1].CopyPropertiesFromMaterial(luzverde);
                break;
        }
    }

    //Passar da cor dos feitiços para a cor normal (cor branca)
    public void TempoLuz()
    {
        //material = luzbranca;
        skinned.materials[1].CopyPropertiesFromMaterial(luzbranca);
    }
}

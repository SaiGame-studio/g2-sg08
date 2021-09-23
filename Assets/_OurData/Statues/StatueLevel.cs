using UnityEngine;

public class StatueLevel : Level
{
    [Header("Statue")]
    public StatueCtrl statueCtrl;

    protected override void LoadComponents()
    {
        this.LoadStatueCtrl();
    }

    protected virtual void LoadStatueCtrl()
    {
        if (this.statueCtrl != null) return;
        this.statueCtrl = GetComponent<StatueCtrl>();

        Debug.Log(transform.name + ": LoadStatueCtrl");
    }

    private void OnTriggerStay(Collider other)
    {
        this.CheckActor(other.gameObject, true);
    }

    private void OnTriggerExit(Collider other)
    {
        this.CheckActor(other.gameObject, false);
    }

    protected virtual void CheckActor(GameObject gameObject, bool status)
    {
        if (this.canLevelUp == status) return;

        int layer = gameObject.layer;
        if (layer != MyLayerManager.instance.layerHero) return;

        this.canLevelUp = status;

        PlayerManager playerManager = PlayerManager.instance;
        if (status) playerManager.playerInput.interactable = this.statueCtrl.statueInteractable;
        else playerManager.playerInput.interactable = null;

        //Debug.Log(transform.name + ": Level Up "+ status.ToString());
    }
}

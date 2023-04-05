using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class FoxCharacterWinScreenAddCrystals : FoxCharacterWinScreen {
    protected override void OnWin() {
        int crystalsCount = this.FoxCharacterInventory.jewelsCount;
        var request = new AddUserVirtualCurrencyRequest { Amount = crystalsCount, VirtualCurrency = "CR" };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddUserVirtualCurrencySuccess, OnAddUserVirtualCurrencyError);
        base.OnWin();
    }

    private void OnAddUserVirtualCurrencySuccess(ModifyUserVirtualCurrencyResult result) {
        Debug.Log("Virtual currency increased successfully.");
    }

    private void OnAddUserVirtualCurrencyError(PlayFabError error) {
        Debug.LogError("Failed to increase virtual currency: " + error.ErrorMessage);
    }
}
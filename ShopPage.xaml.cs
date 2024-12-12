using ArdeleanSebastianLab7.Models;
using Plugin.LocalNotification;

namespace ArdeleanSebastianLab7;

public partial class ShopPage : ContentPage
{
    public ShopPage()
    {
        InitializeComponent();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }
    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        var address = shop.Adress;
        //var locations = await Geocoding.GetLocationsAsync(address);

        var options = new MapLaunchOptions
        {
            Name = "Magazinul meu preferat"
        };
        //var shoplocation = locations?.FirstOrDefault();
        var shoplocation= new Location(46.7492379, 23.5745597);

        await Map.OpenAsync(shoplocation, options);
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;

        if (shop == null)
        {
            await DisplayAlert("Eroare", "Magazinul nu a fost gãsit.", "OK");
            return;
        }

        bool confirm = await DisplayAlert("Confirmare",
            "E?ti sigur cã vrei sã ?tergi acest magazin?", "Da", "Nu");

        if (confirm)
        {
            await App.Database.DeleteShopAsync(shop);
            await Navigation.PopAsync();
        }
    }

}
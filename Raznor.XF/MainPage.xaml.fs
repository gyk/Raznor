namespace Raznor.XF

open System
open Xamarin.Forms
open Xamarin.Forms.Xaml

type MainPage() =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<MainPage>)

    let songList = base.FindByName<ListView>("songList")
    let mediaElement = base.FindByName<MediaElement>("mediaElement")
    let openButton = base.FindByName<Button>("openButton")

    do songList.ItemsSource <- [|
        "https://www2.cs.uic.edu/~i101/SoundFiles/CantinaBand3.wav";
        "https://www2.cs.uic.edu/~i101/SoundFiles/StarWars3.wav";
        "https://upload.wikimedia.org/wikipedia/commons/b/b8/K545_andante.ogg";
    |]

    member this.OnItemTapped(sender: Object, args: ItemTappedEventArgs) =
        do
            mediaElement.Source <- MediaSource.op_Implicit(args.Item :?> string)
            mediaElement.Play()

namespace Raznor.XF

open System
open Xamarin.Forms
open Xamarin.Forms.Xaml

type MainPage() =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<MainPage>)

    let songList = base.FindByName<ListView>("songList")
    let progressBar = base.FindByName<ProgressBar>("progressBar")
    let openButton = base.FindByName<Button>("openButton")
    let playButton = base.FindByName<Button>("playButton")

    member this.OnSongTapped(sender: Object, args: EventArgs) =
        ()

    member this.OnOpenButtonClicked(sender: Object, args: EventArgs) =
        async {
            do songList.ItemsSource <- [| "foo"; "bar" |]
            do playButton.IsEnabled <- true
            do progressBar.Progress <- 0.0
        } |> Async.StartImmediate

    member this.OnPlayButtonClicked(sender: Object, args: EventArgs) =
        async {
            do playButton.IsEnabled <- false
            do progressBar.Progress <- 1.0
        } |> Async.StartImmediate

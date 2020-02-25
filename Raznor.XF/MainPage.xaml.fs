namespace Raznor.XF

open System
open System.Threading.Tasks

open Xamarin.Forms
open Xamarin.Forms.Xaml

open Plugin.FilePicker

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

    // TODO: Should be able to open multiple files but the FilePicker plugin does not support it for now.
    // See https://github.com/jfversluis/FilePicker-Plugin-for-Xamarin-and-Windows/issues/25.
    // TODO: Extension filters. FilePicker's `allowedTypes` has no cross-platform support.
    let openFileAsync () =
        async {
            let tcs = new TaskCompletionSource<Abstractions.FileData>()
            Device.BeginInvokeOnMainThread(fun () ->
                async {
                    let! fileResult = CrossFilePicker.Current.PickFile() |> Async.AwaitTask
                    tcs.SetResult(fileResult)
                } |> Async.StartImmediate
            )
            return! tcs.Task |> Async.AwaitTask
        }

    member this.OnItemTapped(sender: Object, args: ItemTappedEventArgs) =
        do
            mediaElement.Source <- MediaSource.op_Implicit(args.Item :?> string)
            mediaElement.Play()

    member this.OnOpenButtonClicked(sender: Object, args: EventArgs) =
        async {
            let! fileData = openFileAsync()
            do songList.ItemsSource <- [| fileData.FilePath |]
        } |> Async.StartImmediate

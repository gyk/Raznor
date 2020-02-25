namespace Raznor.XF

open Xamarin.Forms

type App() =
    inherit Application(MainPage = MainPage())

    do Device.SetFlags([| "MediaElement_Experimental" |])

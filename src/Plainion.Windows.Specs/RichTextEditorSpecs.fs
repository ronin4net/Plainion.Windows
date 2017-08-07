﻿namespace Plainion.Windows.Specs.Controls.Text

open System.Threading
open NUnit.Framework
open Plainion.Windows.Controls.Text
open Plainion.Windows.Specs.Controls.Text
open FsUnit

[<Apartment(ApartmentState.STA)>]
[<Spec>]
module ``Given a RichtTextEditor with multiple occurences of a search text`` =

    let create() =
        let editor = new RichTextEditor()
        editor.Document <- newDocument "f# is concise. F# is functional. F# is great ;-)"
        editor

    [<Test>]
    let ``<When> searching for all occurences <Then> all words should be highlighted``() =
        let editor = create()

        editor.Search("f#", SearchMode.All) |> should be True

        editor.Document |> shouldHighlight "f#" (count 3)

    [<Test>]
    let ``<When> searching forward and backward <Then> correct offsets should be found``() =
        let editor = create()

        editor.Search("f#", SearchMode.Initial) |> should be True

        editor.Document |> shouldHighlight "f#" (at 1)
        
        editor.Search("f#", SearchMode.Next) |> should be True

        editor.Document |> shouldHighlight "f#" (at 18)

        editor.Search("f#", SearchMode.Next) |> should be True

        editor.Document |> shouldHighlight "f#" (at 36)

        editor.Search("f#", SearchMode.Previous) |> should be True

        editor.Document |> shouldHighlight "f#" (at 18)

        editor.Search("f#", SearchMode.Previous) |> should be True

        editor.Document |> shouldHighlight "f#" (at 1)

[<Apartment(ApartmentState.STA)>]
[<Spec>]
module ``Given a RichtTextEditor with zero occurences of a search text`` =

    let create() =
        let editor = new RichTextEditor()
        editor.Document <- newDocument "f# is concise. F# is functional. F# is great ;-)"
        editor

    [<Test>]
    let ``<When> searching <Then> nothing is found``() =
        let editor = create()

        editor.Search("java", SearchMode.Initial) |> should be False

        editor.Document |> shouldHighlight "java" (count 0)

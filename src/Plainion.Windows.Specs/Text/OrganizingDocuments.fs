﻿namespace Plainion.Windows.Text

open System
open System.Threading
open FsUnit
open NUnit.Framework
open Plainion.IO.MemoryFS
open Plainion.Windows.Controls.Text

[<Feature>]
module ``Manage multiple documents``=

    [<Apartment(ApartmentState.STA)>]
    [<Scenario>]
    module ``Given an empty NoteBook`` =
        [<When>]
        let ``<When> adding a DocumentStore with documents and folders <Then> navigation page is building a tree accordingly``() =
            let notebook = new NoteBook()

            let fs = new FileSystemImpl()
            let store = new FileSystemDocumentStore(fs.Directory("/x"))
            store.Initialize()

            store.Create("/d1").Body |> addText "d1"
            store.Create("/s1/d2").Body |> addText "d2"
            store.Create("/s2/d3").Body |> addText "d3"

            notebook.DocumentStore <- store

            notebook.myNavigation.Root.Children |> should haveCount 3
            notebook.myNavigation.Root.Children.[1].Children |> should haveCount 1
            notebook.myNavigation.Root.Children.[2].Children |> should haveCount 1

    [<Apartment(ApartmentState.STA)>]
    [<Scenario>]
    module ``Given a NoteBook with documents`` =
        open Plainion.Windows.Controls.Tree
        open Plainion.Windows.Interactivity.DragDrop
        open System.Windows
        open System.Windows.Threading
        open System.Windows.Documents

        [<When>]
        let ``<When> deleting a navigation node <Then> DocumentStore is updated accordingly``() =
            let notebook = new NoteBook()

            let fs = new FileSystemImpl()
            let store = new FileSystemDocumentStore(fs.Directory("/x"))
            store.Initialize()

            store.Create("/d1").Body |> addText "d1"
            store.Create("/s1/d2").Body |> addText "d2"

            notebook.DocumentStore <- store

            let node = notebook.myNavigation.Root.Children.[1].Children.[0]
            notebook.myNavigation.DeleteCommand.Execute(node)

            store.TryGet("/s1/d2") |> should be Null

        [<When>]
        let ``<When> selecting a navigation node <Then> respective document is shown in NotePad``() =
            let notebook = new NoteBook()

            let fs = new FileSystemImpl()
            let store = new FileSystemDocumentStore(fs.Directory("/x"))
            store.Initialize()

            store.Create("/d1").Body |> addText "d1"
            store.Create("/s1/d2").Body |> addText "d2"

            notebook.DocumentStore <- store

            let node = notebook.myNavigation.Root.Children.[1].Children.[0]
            node.IsSelected <- true

            notebook.myNotePad.Document |> should equal (store.Get("/s1/d2").Body)

        [<When>]
        let ``<When> dropping a document on a folder <Then> document is moved to the folder``() =
            let notebook = new NoteBook()

            let fs = new FileSystemImpl()
            let store = new FileSystemDocumentStore(fs.Directory("/x"))
            store.Initialize()

            store.Create("/d1") |> ignore
            store.Create("/s1/d2") |> ignore

            notebook.DocumentStore <- store

            let request = new NodeDropRequest()
            request.DroppedNode <- notebook.myNavigation.Root.Children.[0]
            request.DropTarget <-  notebook.myNavigation.Root.Children.[1]
            request.Location <- DropLocation.InPlace

            notebook.myNavigation.DropCommand.Execute(request)

            store.TryGet("/d1") |> should be Null
            let s1 = store.Root.Entries.[0] :?> Folder
            s1.Entries.[0].Title |> should equal "d2"
            s1.Entries.[1].Title |> should equal "d1"

        [<When>]
        let ``<When> dropping a document on an empty document <Then> target document is converted into folder and only new document is added as child``() =
            NavigationNodeFactory.Dispatcher <- Dispatcher.CurrentDispatcher
            let notebook = new NoteBook()

            let fs = new FileSystemImpl()
            let store = new FileSystemDocumentStore(fs.Directory("/x"))
            store.Initialize()

            store.Create("/d1") |> ignore
            store.Create("/d2") |> ignore

            notebook.DocumentStore <- store

            let request = new NodeDropRequest()
            request.DroppedNode <- notebook.myNavigation.Root.Children.[0]
            request.DropTarget <-  notebook.myNavigation.Root.Children.[1]
            request.Location <- DropLocation.InPlace

            notebook.myNavigation.DropCommand.Execute(request)
            DoEventsSync()

            store.TryGet("/d1") |> should be Null
            let d2 = store.Root.Entries.[0] :?> Folder
            d2.Entries |> should haveCount 1
            d2.Entries.[0].Title |> should equal "d1"

        [<When>]
        let ``<When> dropping a document on a document with content <Then> target document is converted into folder and both documents are added as children``() =
            NavigationNodeFactory.Dispatcher <- Dispatcher.CurrentDispatcher
            let notebook = new NoteBook()

            let fs = new FileSystemImpl()
            let store = new FileSystemDocumentStore(fs.Directory("/x"))
            store.Initialize()

            store.Create("/d1").Body.Blocks.Add(new Paragraph(new Run("doc 1")))
            store.Create("/d2").Body.Blocks.Add(new Paragraph(new Run("doc 2")))

            notebook.DocumentStore <- store

            let request = new NodeDropRequest()
            request.DroppedNode <- notebook.myNavigation.Root.Children.[0]
            request.DropTarget <-  notebook.myNavigation.Root.Children.[1]
            request.Location <- DropLocation.InPlace

            notebook.myNavigation.DropCommand.Execute(request)
            DoEventsSync()

            store.TryGet("/d1") |> should be Null
            let d2 = store.Root.Entries.[0] :?> Folder
            d2.Entries.[0].Title |> should equal "d2"
            d2.Entries.[1].Title |> should equal "d1"




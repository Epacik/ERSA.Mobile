open System;
open Microsoft.FSharp.Collections

// let's get a key from env var
let key = System.Environment.GetEnvironmentVariable("ERSA_API_KEY", System.EnvironmentVariableTarget.User)

// init api client
let api = ERSA.Mobile.AdminApi.Client(key)

//get list of links 
let links = api.ListLinksAsync() |> Async.AwaitTask |> Async.RunSynchronously

//define some functions
let printLink (link: ERSA.Mobile.AdminApi.Link) = printfn $"Path: {link.Path}\nTarget {link.Target}\n";
let printLinkWO (link: ERSA.Mobile.AdminApi.LinkWithOpengraph) = printfn $"Path: {link.Path}\nTarget {link.Target}\n";
let printOpenGraph (og: ERSA.Mobile.AdminApi.OpengraphTag) = printfn $"Tag: {og.Tag}, Content: {og.Content}"

// print all of links
for index in 0 .. links.Length - 1 do
    printLink links[index];

    // get link data with og tags
    let link = api.GetLinkDataAsync links[index].Path |> Async.AwaitTask |> Async.RunSynchronously
    
    
    printLinkWO link
    //print all of the of tags
    for n in 0 .. link.OpengraphTags.Length - 1 do
    
        printOpenGraph link.OpengraphTags[n]
        let openGraph = api.GetOpenGraphTagAsync (int link.OpengraphTags[n].ID) |> Async.AwaitTask |> Async.RunSynchronously
        printOpenGraph openGraph
    done

    printfn ""
done

/// --------------
/// TESTING LINKS
/// --------------

let random = Random();

// create a link with a random path
let link = ERSA.Mobile.AdminApi.LinkToAdd.op_Explicit struct ($"test{random.NextInt64 (420, 666)}", "https://www.youtube.com/", false)

// try to add a new link
let mutable result = api.AddLinkAsync link |> Async.AwaitTask |> Async.RunSynchronously
printf $"was link added: {result.Success}; message: {result.Message}"

//get a last link on the list (not necessarly the one we tried to add)
let lastLink = api.ListLinksAsync() |> Async.AwaitTask |> Async.RunSynchronously |> Array.last

printfn "\nlast link"
printLink lastLink

// create a link to update
let linkToUpdate = ERSA.Mobile.AdminApi.Link.op_Explicit struct (lastLink.Id, lastLink.Path, lastLink.Target + "_UPDATED", not lastLink.HideTarget)

// try to update link
result <- api.UpdateLinkAsync linkToUpdate |> Async.AwaitTask |> Async.RunSynchronously
printf $"was link updated: {result.Success}; message: {result.Message}"

// try to get link data of recently updated link
let updatedLink = api.GetLinkDataAsync (lastLink.Id.ToString()) |> Async.AwaitTask |> Async.RunSynchronously
printLinkWO updatedLink


open Parser
open Evaluator
open CS334






/////TO IMPLEMENT
/// - Adjust for inflation
/// - Allow initialcapital only one time

[<EntryPoint>]
let main argv =


    let input = startAndReadInput ()
    // printfn "%s" input


    

    match parse input with
    | Some ast ->
        let pythonCode = evaluate input
        displayOutput pythonCode
        0
        
    | None -> 
        printfn "Invalid Stock Transations, please try again."
        1
    


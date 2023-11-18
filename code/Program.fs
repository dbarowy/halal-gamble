open Parser
open Evaluator
open CS334






[<EntryPoint>]
let main argv =


    let input = startAndReadInput ()

    match parse input with
    | Some ast ->
        displayOutput (evaluate input)
        0
        
    | None -> 
        printfn "Invalid Stock Transations, please try again."
        1


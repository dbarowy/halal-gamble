open Parser
open Evaluator
open CS334
open Math
open Chart




(*
TO IMPLEMENT notes

- Adjust for inflation
- Allow initialcapital only one time
- Make sure users dont sell more than they buy
- what if i never sell
*)



[<EntryPoint>]
let main argv =

    let input = startAndReadInput ()
    // printAST input

    let ast = parse input
    match ast with
    | Some ast ->
        let dataStructure = evaluate ast
        // let calculatedData = calculate dataStructure
        // visualize calculatedData
        printfn "%A" dataStructure
        0
        
    | None -> 
        printfn "Invalid Stock Transations, please try again."
        1
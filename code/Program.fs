open Parser
open Evaluator
open CS334
open Math




(*
TO IMPLEMENT notes

- Adjust for inflation
- Allow initialcapital only one time
- Make sure users dont sell more than they buy [done]
- what if i never sell
*)



[<EntryPoint>]
let main argv =

    let input = startAndReadInput ()
    // printAST input

    let ast = parse input
    match ast with
    | Some ast ->
        let userTransactions = evaluate ast
        calculate userTransactions
        0
        
    | None -> 
        printfn "Invalid Stock Transations, please try again."
        1



(*
start
initialcapital(100)
buy(tsla,10)
next
output(portfolio)
exit
*)
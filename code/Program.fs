open Parser
open Evaluator
open CS334
open Math


(* 
* The main function of the application. It reads input, parses it into an AST, 
* evaluates the AST for transactions, performs calculations, displays ouyput and returns an exit status.

* @param argv: Command line arguments.
* @return: Exit status code (0 for success, 1 for parsing failure).
*)
[<EntryPoint>]
let main argv =
    let input = startAndReadInput ()
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
TO IMPLEMENT notes

- Adjust for inflation
- Allow initialcapital only one time
- Make sure users dont sell more than they buy [done]
- what if i never sell
- give option to save my pdf portfolio and graphs
*)
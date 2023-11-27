open Parser
open Evaluator
open CS334




(*
TO IMPLEMENT notes

- Adjust for inflation
- Allow initialcapital only one time
- Make sure users dont sell more than they buy
*)



[<EntryPoint>]
let main argv =


    let input = startAndReadInput ()
    // printfn "%s" input

    printAST input







    

    // match parse input with
    // | Some ast ->
    //     let mathcode = evaluate input
    //     let calculatedOutput = calculate mathcode
    //     displayOutput calculatedOutput
    //     0
        
    // | None -> 
    //     printfn "Invalid Stock Transations, please try again."
    //     1
    

(*
initialcapital(100) 
buy(gold,50)  
next
sell(gold,50)
next
output(bargraph)
exit
*)
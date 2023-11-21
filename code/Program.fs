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
    printfn "%s" input
    // 0







    match parse input with
    | Some ast ->
        printfn "%A" ast
        0
        
    | None -> 
        printfn "Invalid Stock Transations, please try again."
        1

    // match parse input with
    // | Some ast ->
    //     let pythonCode = evaluate input
    //     displayOutput pythonCode
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
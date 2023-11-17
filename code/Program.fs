open Parser
open Evaluator
open CS334


let inputTest i = 
    printfn "iter - %d" i
    let input1 = System.Console.ReadLine()
    let input2 = System.Console.ReadLine()
    let input3 = System.Console.ReadLine()
    let input4 = System.Console.ReadLine()
    let input4 = System.Console.ReadLine()

    let result = input1 + input2 + input3 + input4
    printfn "%s" result 
    System.Console.ReadLine() |> ignore





[<EntryPoint>]
let main argv =
    inputTest 1
    0



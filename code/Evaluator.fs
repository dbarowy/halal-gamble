module Evaluator
open AST


//sprintf "\"%sB\": %d, " (stock.ToString()) amount
//Have not yet put the YEAR of transaction inside the string
let evalBuy (buy: Buy): string =
    let stock = buy.stock
    let amount = buy.buy
    sprintf "\"%sB\": %d, " (stock.ToString()) amount


let evalSell (sell: Sell): string = 
    let stock = sell.stock
    let amount = sell.sell
    sprintf "\"%sS\": %d, " (stock.ToString()) amount



let evalInitialCapital (initial: InitialCapital): string =
    sprintf "initial = %A\n\nprogram = {\n" initial

let evalCommand (command: Command): string =
    match command with
    | BuyCommand buy -> evalBuy buy
    | SellCommand sell -> evalSell sell
    | InitialCapitalCommand capital -> evalInitialCapital capital


let evalOutput (output: Output): string = 
    match output with
    | Bargraph -> "\"bargraph\": True, "
    | Timeseries -> "\"timeseries\": True, "
    | Portfolio -> "\"portfolio\": True, "


let evalLine (line: Line): string = 
    match line with
    | Command command -> evalCommand command
    | Output output -> evalOutput output


let rec evalProgram (program: Program) : string =
    match program with
    | [] -> "\n}\n"
    | l::ls -> (evalLine l) + (evalProgram ls)

let evaluate (program: Program) =
    "python code goes here" 
    + (evalProgram program) +
    "more python code goes here"



(* We want the evaluator to generate the following python code (or something similar to this)
Keep in mind that F# dictionary is not the same notation as python dictionary. Might need string manipulation to get the key and value

initial = 1000

program = {
    "GOLD2016B": 40,
    "GOLD2018S": 40,
    "SLVR2016B": 100,
    "SLVR2017B": 100,
    "SLVR2020S": 150,
    "TSLA2016B": 100,
    "TSLA2017B": 100,
    "TSLA2018B": 100,
    "TSLA2019B": 100,
    "TSLA2020S": 400,
    "portfolio": True,
    "bargraph": True,
    "timeseries": True
}

*)
module Evaluator
open AST
open System.Collections.Generic


// let transactionsx = dict [
//     "GOLD2016B", 40
//     "GOLD2018S", 40
//     "SLVR2016B", 100
//     "SLVR2017B", 100
//     "SLVR2020S", 150
//     "TSLA2016B", 100
//     "TSLA2017B", 100
//     "TSLA2018B", 100
//     "TSLA2019B", 100
//     "TSLA2020S", 400
//     "portfolio", 1
//     "bargraph", 1
//     "timeseries", 1
// ]
let transactions = new Dictionary<string, int>()
let capital = Array.create 6 0

//sprintf "\"%sB\": %d, " (stock.ToString()) amount
//Have not yet put the YEAR of transaction inside the string
let evalBuy (buy: Buy) =
    let key = (buy.stock.ToUpper() + (buy.year.ToString()) + "B")
    transactions.Add(key, buy.buy)
    capital[buy.year - 2015] <- capital[buy.year] - buy.buy
    key + " " + buy.buy.ToString()


 
let evalSell (sell: Sell) = 
    let key = (sell.stock.ToUpper() + (sell.year.ToString()) + "S")
    transactions.Add(key, sell.sell)
    capital[sell.year - 2015] <- capital[sell.year] + sell.sell
    key + " " + sell.sell.ToString()



let evalInitialCapital (initial: InitialCapital) =
        capital[0] <- capital[0] + initial.amount
        initial.initial + " " + initial.amount.ToString()



let evalCommand (command: Command) =
    match command with
    | BuyCommand buy -> evalBuy buy
    | SellCommand sell -> evalSell sell
    | InitialCapitalCommand capital -> evalInitialCapital capital


let evalOutput (output: Output) = 
    match output with
    | Bargraph -> 
                transactions.Add("bargraph", 1)
                "bargraph"
    | Timeseries -> 
                    transactions.Add("timeseries", 1)
                    "timeseries"
    | Portfolio -> 
                    transactions.Add("portfolio", 1)
                    "portfolio"


let evalLine (line: Line) = 
    match line with
    | Command command -> evalCommand command
    | Output output -> evalOutput output


let rec evalProgram (program: Program) =
    match program with
    | [] -> ""
    | l::ls -> (evalLine l) + (evalProgram ls)

let evaluate (program: Program) =
    (evalProgram program)


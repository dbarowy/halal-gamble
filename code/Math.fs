module Math
open Evaluator


//Rates
// type YearlyRates = {
//     s2016: float option
//     s2017: float option
//     s2018: float option
//     s2019: float option
//     s2020: float option
// }

// type AssetRates = {
//     b2015: YearlyRates
//     b2016: YearlyRates
//     b2017: YearlyRates
//     b2018: YearlyRates
//     b2019: YearlyRates
// }

// type Rates = {
//     GOLD: AssetRates
//     SLVR: AssetRates
//     TSLA: AssetRates
// }

// let rates = {
//     GOLD = {
//         b2015 = { s2016 = Some 1.9; s2017 = Some 0.1; s2018 = Some 0.7; s2019 = Some 1.2; s2020 = Some 1.3 }
//         b2016 = { s2016 = None; s2017 = None; s2018 = Some -0.7; s2019 = Some -1.2; s2020 = Some -1.3 }
//         b2017 = { s2016 = None; s2017 = None; s2018 = Some -0.7; s2019 = Some -1.2; s2020 = Some -1.3 }
//         b2018 = { s2016 = None; s2017 = None; s2018 = None; s2019 = Some -1.2; s2020 = Some -1.3 }
//         b2019 = { s2016 = None; s2017 = None; s2018 = None; s2019 = None; s2020 = Some -1.3 }
//     }
//     SLVR = {
//         b2015 = { s2016 = Some 1.9; s2017 = Some 0.1; s2018 = Some 0.7; s2019 = Some 1.2; s2020 = Some 1.3 }
//         b2016 = { s2016 = None; s2017 = None; s2018 = Some -0.7; s2019 = Some -1.2; s2020 = Some -1.3 }
//         b2017 = { s2016 = None; s2017 = None; s2018 = Some -0.7; s2019 = Some -1.2; s2020 = Some -1.3 }
//         b2017 = { s2016 = None; s2017 = None; s2018 = Some -0.7; s2019 = Some -1.2; s2020 = Some -1.3 }
//         b2018 = { s2016 = None; s2017 = None; s2018 = None; s2019 = Some -1.2; s2020 = Some -1.3 }
//         b2019 = { s2016 = None; s2017 = None; s2018 = None; s2019 = None; s2020 = Some -1.3 }
//     }
//     TSLA = {
//         b2015 = { s2016 = Some 1.9; s2017 = Some 0.1; s2018 = Some 0.7; s2019 = Some 1.2; s2020 = Some 1.3 }
//         b2016 = { s2017 = Some 0.3; s2018 = Some -0.7; s2019 = Some -1.2; s2020 = Some -1.3 }
//         b2017 = { s2016 = None; s2017 = None; s2018 = Some -0.7; s2019 = Some -1.2; s2020 = Some -1.3 }
//         b2018 = { s2016 = None; s2017 = None; s2018 = None; s2019 = Some -1.2; s2020 = Some -1.3 }
//         b2019 = { s2016 = None; s2017 = None; s2018 = None; s2019 = None; s2020 = Some -1.3 }
//     }
// }


// let rates = {
//     "GOLD20152016": 1.9
//     "GOLD20152017": 0.1
//     "GOLD20152018": 0.7
//     "GOLD20152019": 1.2, "GOLD20152020": 1.3,
//     "GOLD20162017": 0.3, "GOLD20162018": -0.7, "GOLD20162019": -1.2, "GOLD20162020": -1.3,
//     "GOLD20172018": -0.7, "GOLD20172019": -1.2, "GOLD20172020": -1.3,
//     "GOLD20182019": -1.2, "GOLD20182020": -1.3,
//     "GOLD20192020": -1.3,

//     "SLVR20152016": 1.9, "SLVR20152018": 0.7, "SLVR20152017": 0.1, "SLVR20152019": 1.2, "SLVR20152020": 1.3,
//     "SLVR20162017": 0.3, "SLVR20162018": -0.7, "SLVR20162019": -1.2, "SLVR20162020": -1.3,
//     "SLVR20172018": -0.7, "SLVR20172019": -1.2, "SLVR20172020": -1.3,
//     "SLVR20182019": -1.2, "SLVR20182020": -1.3,
//     "SLVR20192020": -1.3,

//     "TSLA20152016": 1.9, "TSLA20152018": 0.7, "TSLA20152017": 0.1, "TSLA20152019": 1.2, "TSLA20152020": 1.3,
//     "TSLA20162017": 0.3, "TSLA20162018": -0.7, "TSLA20162019": -1.2, "TSLA20162020": -1.3,
//     "TSLA20172018": -0.7, "TSLA20172019": -1.2, "TSLA20172020": -1.3,
//     "TSLA20182019": -1.2, "TSLA20182020": -1.3,
//     "TSLA20192020": -1.3
// }

let initial = Evaluator.capital[0]
let capital = Evaluator.capital
let transactions = Evaluator.transactions
let output = []


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




let getOutput =
    if transactions.ContainsKey("bargraph") then
        output = output @ ["bargraph"] |> ignore

    if transactions.ContainsKey("timeseries") then
        output = output @ ["timeseries"] |> ignore
    
    if transactions.ContainsKey("portfolio") then
        output = output @ ["portfolio"] |> ignore



let calculate (input: string) =
    "calculated output"



//these are the variables i need to generate from this Math.fs file
//let initial = 1000
//let output = ["timeseries"; "bargraph"; "portfolio"]

let totalYearlyProfit = [34 ; 41; -3; 16; -12; 8]
let yearlyCapital = [1000; 960; 850; 1070; 990; 590]

let portfolioValueWithProfit = [1000; 960; 850; 1034; 1010; 850]

//StockTransactions
let stocks = ["GOLD"; "TSLA"; "SLVR"] //MUST BE UPPERCASE
let starts = [100; 100; 100]
let ends = [110; 50; 50]
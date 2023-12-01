module Math
open System.Collections.Generic

let rates = dict [
    "GOLD20152016", 1.09
    "GOLD20152017", 1.22
    "GOLD20152018", 1.2
    "GOLD20152019", 1.43
    "GOLD20152020", 1.79

    "GOLD20162017", 1.13
    "GOLD20162018", 1.11
    "GOLD20162019", 1.31
    "GOLD20162020", 1.64

    "GOLD20172018", 0.98
    "GOLD20172019", 1.2
    "GOLD20172020", 1.45

    "GOLD20182019", 1.18
    "GOLD20182020", 1.47

    "GOLD20192020", 0.8


    "SLVR20152016", 1.15
    "SLVR20152017", 1.22
    "SLVR20152018", 1.12
    "SLVR20152019", 1.29
    "SLVR20152020", 1.91

    "SLVR20162017", 1.06
    "SLVR20162018", 0.97
    "SLVR20162019", 1.12
    "SLVR20162020", 1.66

    "SLVR20172018", 0.91
    "SLVR20172019", 1.05
    "SLVR20172020", 1.56

    "SLVR20182019", 1.15
    "SLVR20182020", 1.71

    "SLVR20192020", 1.49


    "TSLA20152016", 0.89
    "TSLA20152017", 1.298
    "TSLA20152018", 1.38
    "TSLA20152019", 1.743
    "TSLA20152020", 11.761

    "TSLA20162017", 1.46
    "TSLA20162018", 1.56
    "TSLA20162019", 1.97
    "TSLA20162020", 13.21

    "TSLA20172018", 1.06
    "TSLA20172019", 1.34
    "TSLA20172020", 8.99

    "TSLA20182019", 1.27
    "TSLA20182020", 8.53

    "TSLA20192020", 6.72
]

let years = ["2016"; "2017"; "2018"; "2019"; "2020"]

//-----------------Evaluated Variables-----------------//

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

// let transactions = transactionsx |> Seq.map (fun kvp -> (kvp.Key, float(kvp.Value))) |> Seq.toList
let transactions = Evaluator.transactions |> Seq.map (fun kvp -> (kvp.Key, float(kvp.Value))) |> Seq.toList
let initial = float(Evaluator.capital[0])


//-----------------Calculated Variables-----------------//
let mutable output = []
let yearlyCapital = Array.create 6 0.0
yearlyCapital[0] <- initial
let yearlybuySell = Array.create 6 0.0
let totalYearlyProfit = Array.create 6 0.0
let portfolioValueWithProfit = Array.create 6 0.0
portfolioValueWithProfit[0] <- initial

//StockTransactions
let stocksIndividual : IDictionary<string, float[]> = dict [
    "GOLDB", Array.create 5 0.0
    "GOLDS", Array.create 5 0.0
    "TSLAB", Array.create 5 0.0
    "TSLAS", Array.create 5 0.0
    "SLVRB", Array.create 5 0.0
    "SLVRS", Array.create 5 0.0
]
let profitIndividual = dict [
    "GOLD", Array.create 1 0.0
    "TSLA", Array.create 1 0.0
    "SLVR", Array.create 1 0.0

]
let stocks = ["GOLD"; "TSLA"; "SLVR"] //MUST BE UPPERCASE
let starting = Array.create 3 0.0
let ending = Array.create 3 0.0


//-----------------Working Functions-----------------//

let rec calculateYearlyBuySell transactions =
    match transactions with
    | [] -> yearlybuySell
    | (k, v)::ts ->
        if k = "portfolio" || k = "bargraph" || k = "timeseries" then
            calculateYearlyBuySell ts
        else
            let year = k.Substring(k.Length - 5, 4)
            let ttype = k.Substring(k.Length - 1, 1)

            if ttype = "B" then
                yearlybuySell[int(year) - 2015] <- yearlybuySell[int(year) - 2015] - v
                calculateYearlyBuySell ts
                
            elif ttype = "S" then
                yearlybuySell[int(year) - 2015] <- yearlybuySell[int(year) - 2015] + v
                calculateYearlyBuySell ts
            else
                calculateYearlyBuySell ts

let rec startsEnds (transactions: list<string * float>) =
    match transactions with
    | [] -> starting, ending
    | (k, v)::ts ->
        if k = "portfolio" || k = "bargraph" || k = "timeseries" then
            startsEnds ts
        else
            let stock = k.Substring(0, k.Length - 5)
            let year = k.Substring(k.Length - 5, 4)
            let index = stocks |> Seq.findIndex (fun x -> x = stock)
            let ttype = k.Substring(k.Length - 1, 1)

            if ttype = "B" then
                starting[index] <- starting[index] + v
                startsEnds ts
                
            elif ttype = "S" then

                ending[index] <- ending[index] + v
                if ending[index] > starting[index] then
                    printfn "Nah dude, you cannot sell more than you buy for %s. Basic maths yo!\n" stock
                    printfn "You bought $%.2f and sold $%.2f in %s\nStart again." starting[index] ending[index] year
                    exit 1
                startsEnds ts
            else
                startsEnds ts

let calculateYearlyCapital (yearlybuySell: float list) =
    let rec helper i =
        match i with
        | 5 -> yearlyCapital
        | i ->
            yearlyCapital[i + 1] <- yearlyCapital[i] + yearlybuySell[i + 1]
            helper (i + 1)
    helper 0
    
let rec getOutputTypes (transactions: list<string * float>) =
    match transactions with
    | [] -> output
    | (k, s)::ts ->
        if k = "portfolio" || k = "bargraph" || k = "timeseries" then
            output <- output @ [k]
            getOutputTypes ts
        else
            getOutputTypes ts

let rec calculateStocksIndividual (transactions: list<string * float>) =
    match transactions with
    | [] -> stocksIndividual
    | (k, v)::ts ->
        if k = "portfolio" || k = "bargraph" || k = "timeseries" then
            calculateStocksIndividual ts
        else
            let stock = k.Substring(0, k.Length - 5)
            let year = k.Substring(k.Length - 5, 4)
            let ttype = k.Substring(k.Length - 1, 1)

            let key = stock + ttype
            stocksIndividual[key][int(year) - 2016] <- float(stocksIndividual[key][int(year) - 2016]) + float(v)
            calculateStocksIndividual ts

let rec calculatePortfolioValueWithProfit years =
    match years with
    | [] -> portfolioValueWithProfit
    | y::ys ->
        let index = int(y) - 2015
        portfolioValueWithProfit[index] <- yearlyCapital[index] + totalYearlyProfit[index]
        calculatePortfolioValueWithProfit ys

//-----------------Progress Functions-----------------//


let rec calculateTotalYearlyProfit years =
    match years with
    | [] -> totalYearlyProfit
    | y::ys ->
        let index = int(y) - 2016

        //How much sold every year
        let gold = stocksIndividual["GOLDS"][index]
        let slvr = stocksIndividual["SLVRS"][index]
        let tsla = stocksIndividual["TSLAS"][index]
        // printfn "%A" y
        // printfn "GOld: %A" gold
        // printfn "SLVR: %A" slvr
        // printfn "TSLA: %A" tsla


    
            
        //but i cant calcualte rates from only previous years to curent year. i have to consider when the stock was bought and what if it awas bought in multiple steps
        let goldRate = rates["GOLD" + (string(int(y) - 1)) + y]
        let slvrRate = rates["SLVR" + (string(int(y) - 1)) + y]
        let tslaRate = rates["TSLA" + (string(int(y) - 1)) + y]

        
        
        // printfn "GOLD RATE: %A" goldRate
        // printfn "SLVR RATE: %A" slvrRate
        // printfn "TSLA RATE: %A" tslaRate

        let goldProfit = float(gold) * goldRate
        let slvrProfit = float(slvr) * slvrRate
        let tslaProfit = float(tsla) * tslaRate

        // printfn "%A" goldProfit
        // printfn "%A" slvrProfit
        // printfn "%A" tslaProfit

        profitIndividual["GOLD"][0] <- profitIndividual["GOLD"][0] + goldProfit
        profitIndividual["SLVR"][0] <- profitIndividual["SLVR"][0] + slvrProfit
        profitIndividual["TSLA"][0] <- profitIndividual["TSLA"][0] + tslaProfit
        totalYearlyProfit[index + 1] <- goldProfit + slvrProfit + tslaProfit
        calculateTotalYearlyProfit ys



let rec calculateTotalYearlyProfit2 years =
    match years with
    | [] -> totalYearlyProfit
    | y::ys ->
        let index = int(y) - 2016

        //How much sold every year
        let gold = stocksIndividual["GOLDS"][index]
        let slvr = stocksIndividual["SLVRS"][index]
        let tsla = stocksIndividual["TSLAS"][index]
        // printfn "%A" y
        // printfn "GOld: %A" gold
        // printfn "SLVR: %A" slvr
        // printfn "TSLA: %A" tsla


    
            
        //but i cant calcualte rates from only previous years to curent year. i have to consider when the stock was bought and what if it awas bought in multiple steps
        let goldRate = rates["GOLD" + (string(int(y) - 1)) + y]
        let slvrRate = rates["SLVR" + (string(int(y) - 1)) + y]
        let tslaRate = rates["TSLA" + (string(int(y) - 1)) + y]

        
        
        // printfn "GOLD RATE: %A" goldRate
        // printfn "SLVR RATE: %A" slvrRate
        // printfn "TSLA RATE: %A" tslaRate

        let goldProfit = float(gold) * goldRate
        let slvrProfit = float(slvr) * slvrRate
        let tslaProfit = float(tsla) * tslaRate

        // printfn "%A" goldProfit
        // printfn "%A" slvrProfit
        // printfn "%A" tslaProfit

        profitIndividual["GOLD"][0] <- profitIndividual["GOLD"][0] + goldProfit
        profitIndividual["SLVR"][0] <- profitIndividual["SLVR"][0] + slvrProfit
        profitIndividual["TSLA"][0] <- profitIndividual["TSLA"][0] + tslaProfit
        totalYearlyProfit[index + 1] <- goldProfit + slvrProfit + tslaProfit
        calculateTotalYearlyProfit2 ys








    


let calculate (input: Dictionary<string,float>) =
    startsEnds transactions |> ignore
    let buysells = calculateYearlyBuySell transactions
    let buySellTemp = buysells |> Array.toList
    calculateYearlyCapital buySellTemp |> ignore
    getOutputTypes transactions |> ignore
    calculateStocksIndividual transactions |> ignore
    calculateTotalYearlyProfit years |> ignore
    calculatePortfolioValueWithProfit years |> ignore

    Chart.visualize 
        output 
        initial 
        (totalYearlyProfit  |> Array.toList)
        (yearlyCapital |> Array.toList)
        (portfolioValueWithProfit |> Array.toList)
        stocks 
        (starting |> Array.toList)
        (ending |> Array.toList)
        (yearlybuySell |> Array.toList)
    |> ignore



(*these are the variables i need to generate from this Math.fs file
axxlet initial = 1000 
axxlet output = ["timeseries"; "bargraph"; "portfolio"]

xxlet totalYearlyProfit = [34 ; 41; -3; 16; -12; 8]
axxlet yearlyCapital = [1000; 960; 850; 1070; 990; 590]

xxlet portfolioValueWithProfit = [1000; 960; 850; 1034; 1010; 850]

//StockTransactions
axxlet stocks = ["GOLD"; "TSLA"; "SLVR"] //MUST BE UPPERCASE
axxlet starts = [100; 100; 100]
axxlet ends = [110; 50; 50]
*)
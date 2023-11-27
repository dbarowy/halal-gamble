module Parser
open AST
open Combinator


let numberParser = 
    (pmany1 pdigit |>> (fun digits -> stringify digits |> int))

let GOLDparser = pstr "gold" |>> (fun x -> x)
let SLVRParser = pstr "slvr" |>> (fun x -> x)
let TSLAParser = pstr "tsla" |>> (fun x -> x)

let stockParser: Parser<string> = 
    (GOLDparser <|> SLVRParser <|> TSLAParser) 
    |>> (fun x -> x |> string)
    

let buyParser : Parser<Command> =
    pseq 
        (pright (pstr "buy(") stockParser) 
        (pseq 
            (pbetween (pchar ',') numberParser (pchar ','))
            (pleft numberParser (pchar ')'))
            (fun (amount, year) -> (amount, year)))
        (fun (stock, (amount, year)) -> BuyCommand({ stock = stock; buy = amount; year = year }))

let sellParser : Parser<Command> =
    pseq 
        (pright (pstr "sell(") stockParser) 
        (pseq 
            (pbetween (pchar ',') numberParser (pchar ','))
            (pleft numberParser (pchar ')'))
            (fun (amount, year) -> (amount, year)))
        (fun (stock, (amount, year)) -> SellCommand({ stock = stock; sell = amount; year = year }))


let initialCapitalParser : Parser<Command> =
    let amountAndYearParser = 
        pseq
            (pbetween
                (pstr "initialcapital(") 
                (numberParser)
                (pchar ','))
            (pleft numberParser (pchar ')'))
    amountAndYearParser (fun (amount, year) -> InitialCapitalCommand({initial = "INITIAL"; amount = amount; year = year}))


let commandParser: Parser<Line> = 
    buyParser <|> sellParser <|> initialCapitalParser
    |>> (fun x -> Command(x))



let bargraphParser = 
    pleft
        (pstr "bargraph,")
        numberParser
        |>> (fun _ -> Bargraph)


let timeseriesParser = 
    pleft
        (pstr "timeseries,")
        numberParser
        |>> (fun _ -> Timeseries)


let portfolioParser = 
    pleft
        (pstr "portfolio,")
        numberParser
        |>> (fun _ -> Portfolio)


let graphParser: Parser<Output> = bargraphParser <|> timeseriesParser <|> portfolioParser


let outputParser: Parser<Line> = 
    pbetween 
        (pstr "output(") (graphParser) (pchar ')') 
    |>> (fun output -> Output(output))


// We don't worry about parsing newline (/n) since we are stripping all new lines from the input text
let lineParser: Parser<Line> = commandParser <|> outputParser


let programParser: Parser<Program> = 
    let singleLineParser = lineParser |>> (fun t -> [t])
    let multiLineParser =
        pseq
            singleLineParser
            (pmany0 singleLineParser)
            (fun (l,ls) -> l @ List.concat ls) 
    multiLineParser

let grammar = pleft programParser peof
//eof ->



(*
    Parses a string and returns an AST if the string is valid. Otherwise, returns None.

    @input: The string to parse.
    @returns: An AST if the string is valid. Otherwise, None.
*)
let parse (input: string) : Program option =
    let i = prepare input
    match grammar i with
    | Success(ast, _) -> 

        //Debug print
        // printf "%A" ast

        Some ast
    | Failure(pos,rule) ->
        printfn "Invalid expression."
        let msg = sprintf "Cannot parse input at position %d in rule '%s':" pos rule
        let diag = diagnosticMessage 20 pos input msg
        printf "%s" diag
        None


let printAST (input : string) = 
    match parse input with
    | Some ast ->
        printfn "%A" ast
        0
        
    | None -> 
        printfn "Invalid Stock Transations, please try again."
        1
module Parser
open AST
open Combinator


let numberParser = 
    (pmany1 pdigit |>> (fun digits -> stringify digits |> int))

let GOLDparser = pstr "gold" |>> (fun x -> x)
let SLVRParser = pstr "slvr" |>> (fun x -> x)
let TSLAParser = pstr "tsla" |>> (fun x -> x)

let stockParser: Parser<string> = GOLDparser <|> SLVRParser <|> TSLAParser
    
    
let buyParser =
        pseq
            (pright (pstr "buy(")  stockParser)
            (pbetween (pchar ',') numberParser (pchar ')'))
            (fun (stock, amount) -> BuyCommand({stock = stock; buy = amount}))

let sellParser =
    pseq
        (pright (pstr "sell(")  stockParser)
            (pbetween (pchar ',') numberParser (pchar ')'))
            (fun (stock, amount) -> SellCommand({stock = stock; sell = amount}))

//Probably need to use variable to store this value?
let initialCapitalParser =
    pbetween 
        (pstr "initialcapital(") 
        (numberParser)
        (pchar ')')
        |>> (fun v -> InitialCapitalCommand({initial = "INITIAL"; amount = v}))

    

let commandParser: Parser<Line> = 
    buyParser <|> sellParser <|> initialCapitalParser
    |>> (fun x -> Command(x))



let bargraphParser = pstr "bargraph" |>> (fun _ -> Bargraph)
let timeseriesParser = pstr "timeseries" |>> (fun _ -> Timeseries)
let portfolioParser = pstr "portfolio" |>> (fun _ -> Portfolio)

let graphParser: Parser<Output> = bargraphParser <|> timeseriesParser <|> portfolioParser

let outputParser: Parser<Line> = 
    pbetween 
        (pstr "output(") (graphParser) (pchar ')') 
    |>> (fun output -> Output(output))

//Prolly parse new line here? How about the last line in the string? force it in Library.fs?
let lineParser: Parser<Line> = commandParser <|> outputParser

//Do i need to parse newline?

//yes
//pmany1 (p1 <|> p2)
//for this case (peof <|> (pright pnewline peof) 


//no - remove whitespace
//builtin f# System.String.Join("\n", lines)
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

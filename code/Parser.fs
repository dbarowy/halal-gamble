module Parser

open AST
open Combinator


(*
   BNF Grammar

    <stock> ::= GOLD | SLVR | TSLA
    <transactionAmount> ::= <d><transactionAmount> | <d>
    <d> ::= 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9

    <command> ::= buy(<stock>, <transactionAmount>) | sell(<stock>, <transactionAmount>) | initialCapital(<transactionAmount>) //Dictionary and Array
    
    <line> ::= <command> | <output> 
    <program> ::= <line> | <line><program>

    <output> ::= output(<graph>)
    <graph> ::= bargraph | timeseries | portfolio
*)
let numberParser = 
    (pmany1 pdigit |>> (fun digits -> stringify digits |> int))

let GOLDparser = pstr "GOLD" |>> (fun _ -> GOLD)
let SLVRParser = pstr "SLVR" |>> (fun _ -> SLVR)
let TSLAParser = pstr "TSLA" |>> (fun _ -> TSLA)

let stockParser: Parser<Stock> = GOLDparser <|> SLVRParser <|> TSLAParser
    
let buyParser: Parser<Command> =
        pseq 
        (pstr "buy(") 
        (pseq stockParser (pchar ',') (fun (stock, _) -> stock))
        (pseq numberParser (pchar ')') (fun (amount, _) -> amount))
        (fun (stock, amount) -> Buy({stock = stock; amount = amount}))

let sellParser: Parser<Command> =
    pseq 
        (pstr "sell(") 
        (pseq stockParser (pchar ',') (fun (stock, _) -> stock))
        (pseq numberParser (pchar ')') (fun (amount, _) -> amount))
        (fun (stock, amount) -> Sell({stock = stock; amount = amount}))
        

//Probably need to use variable to store this value?
let initialCapitalParser: Parser<Command> = 
    pbetween 
        (pstr "initialCapital(") 
        numberParser 
        (pchar ')') 
    |>> (fun _ -> InitialCapital)

let commandParser = buyParser <|> sellParser <|> initialCapitalParser

let bargraphParser = pstr "bargraph" |>> (fun _ -> Bargraph)
let timeseriesParser = pstr "timeseries" |>> (fun _ -> Timeseries)
let portfolioParser = pstr "portfolio" |>> (fun _ -> Portfolio)

let graphParser: Parser<Output> = bargraphParser <|> timeseriesParser <|> portfolioParser

let outputParser: Parser<Line> = 
    pbetween 
        (pstr "output(") (graphParser) (pchar ')') 
    |>> (fun output -> Output(output))


let lineParser: Parser<Line> = commandParser <|> outputParser

//Do i need to parse newline?
let programParser: Parser<Program> = 
    pmany1 lineParser

let grammar = pleft programParser peof

(*
    Parses a string and returns an AST if the string is valid. Otherwise, returns None.

    @input: The string to parse.
    @returns: An AST if the string is valid. Otherwise, None.
*)
let parse (input: string) : Program option =
    let i = prepare input
    match grammar i with
    | Success(ast, _) -> Some ast
    | Failure(pos,rule) ->
        printfn "Invalid expression."
        let msg = sprintf "Cannot parse input at position %d in rule '%s':" pos rule
        let diag = diagnosticMessage 20 pos input msg
        printf "%s" diag
        None

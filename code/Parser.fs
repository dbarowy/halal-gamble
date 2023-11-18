module Parser

open AST
open Combinator


(*
    BNF Grammar
    
    <program> ::= <command> | <control> | <output>

    <stock> ::= GOLD | SILVER | AMAZON | TESLA | AMDS | INTEL | MICROSOFT | FACEBOOK | GOOGLE | APPLE...
    <transactionAmount> ::= <d><number> | <d>
    <d> ::= 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9
    <command> ::= buy (<stock>, <transactionAmount>) | sell (<stock>, <transactionAmount>) | initialCapital(<transactionAmount>)
    <control> ::= start | next | exit
    <output> ::= graph(<graph>) | report(<report>)
    <graph> ::= bargraph | timeseries
    <report> ::= portfolio | statement | analysis
*)

let stockParser : Parser<Program> = failwith "TODO"

let transactionAmountParser  : Parser<Program>= failwith "TODO"

let controlParser : Parser<Program> = failwith "TODO"

let outputParser : Parser<Program> = failwith "TODO"

let commandParser : Parser<Program> = failwith "TODO"

let programParser : Parser<Program> = commandParser <|> controlParser <|> outputParser


let canvasParser =
    let singleLine = programParser |>> (fun t -> [t])
    let multipleLines = 
        pseq
            singleLine 
            (pmany0 
                (pright 
                    (pstr "\n") 
                    singleLine
                )
            )
            (fun (l, ls) -> l @ List.concat ls)
    multipleLines



let grammar = pleft canvasParser peof


(*
    Parses a string and returns an AST if the string is valid. Otherwise, returns None.

    @input: The string to parse.
    @returns: An AST if the string is valid. Otherwise, None.
*)
let parse (input: string) : Canvas option =
    let i = prepare input
    match grammar i with
    | Success(ast, _) -> Some ast
    | Failure(_,_) -> None



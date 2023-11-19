module AST


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

type Stock = 
    | GOLD of string
    | SLVR of string
    | TSLA of string

type Buy = {stock: Stock; amount: int}
type Sell = {stock: Stock; amount: int}
type InitialCapital = InitialCapital of int

type Command = Buy | Sell | InitialCapital

type Output = Bargraph | Timeseries | Portfolio

type line =
    | Command of Command
    | Output of Output

type Program = line list
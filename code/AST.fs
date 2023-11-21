module AST


(*
   BNF Grammar

    <stock> ::= GOLD | SLVR | TSLA
    <transactionAmount> ::= <d><transactionAmount> | <d>
    <d> ::= 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9

    <command> ::= buy(<stock>, <transactionAmount>, <year>) | sell(<stock>, <transactionAmount>, <year>) | initialCapital(<transactionAmount>) //Dictionary and Array
    
    <line> ::= <command> | <output> 
    <program> ::= <line> | <line><program>

    <output> ::= output(<graph>)
    <graph> ::= bargraph | timeseries | portfolio
*)


type Buy = {stock: string; buy: int; year: int}
type Sell = {stock: string; sell: int; year: int}
type InitialCapital = {initial: string; amount: int; year: int}

type Command = 
    | BuyCommand of Buy
    | SellCommand of Sell
    | InitialCapitalCommand of InitialCapital
    
type Bargraph = string
type Timeseries = string
type Portfoio = string

type Output = 
    |Bargraph 
    |Timeseries 
    |Portfolio

type Line = Command of Command | Output of Output
type Program = Line list


(*
initialcapital(100)
sell(tsla, 40)
next
buy(gold,100)
sell(tsla,40)
next
next
next
buy(slvr,40)
exit
output(portfolio)
output(bargraph)
exit
*)
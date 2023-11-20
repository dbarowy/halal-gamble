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




//Probably want to put year as a field too?? Or else how do i keep track of when transaction was made?
type Buy = {stock: string; buy: int}
type Sell = {stock: string; sell: int}
type InitialCapital = {initial: string; amount: int}

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
initialcapital(100) -> InitialCapital{initial = "INITIAL"; amount = 100}
sell(TSLA, 40)
output(portfolio)
output(bargraph)
exit
*)
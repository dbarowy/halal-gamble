module AST


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

type Stock = 
    | GOLD
    | SILVER
    | AMAZON
    | TESLA
    | AMD
    | INTEL
    | MICROSOFT
    | FACEBOOK
    | GOOGLE
    | APPLE

type TransactionAmount = TransactionAmount of int

type Command =
    | Buy of Stock * TransactionAmount
    | Sell of Stock * TransactionAmount
    | InitialCapital of TransactionAmount

type Control =
    | Next
    | Exit

type Graph = BarGraph | TimeSeries

type Report = Portfolio | Statement | Analysis

type Output =
    | Graph of Graph
    | Report of Report

type Program =
    | Command of Command
    | Control of Control
    | Output of Output

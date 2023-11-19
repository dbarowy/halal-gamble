module CS334
open System

(*
    Prints the usage message and exits the program with exit code 1.
*)
let usage () = 
    printfn "Usage: start\n
    Select how much mone you want to invest: initialCapital(<howMuchMoneyYouWantToStartWith>)
    To perform actions: buy(<stockName>, <howMuchToBoy>) or sell(<stockName>, <howMuchToSell>)\n
    To see outputs: output(<graph>)\t<graph> is either bargraph or timeseries or portfolio\n
    Avaialble stocks: GOLD, SLVR, TSLA\n
    To go to the next year: next\n
    To exit the game: exit\n

    Try running again.
    "
    exit 1




(*
    Prints the start message for the game
*)
let printStartMessage () = 
    printfn "Welcome to Halal Gamble: The Stock Simulation Game\n
    The rules are simple: You start with a certain cash and you can buy and sell stocks. The goal is to make as much money as possible.\n
        
    Once you type \"start\", you will be taken to a webpage where you can see the graph of the stock prices from 2010 to 2015. Right now in time, image you are in 2016 and you will be able to make trades based on the data you see on the webpage. Once you are done, you can type \"next\" and now you are in 2017. You will be taken to a webpage showing stock prices and news from 2010-2016 and you can make trades based on the information you see. This continues until 2020 and at the end you can compare your results with the real outcome of the market.\n
        
    You can type \"exit\" at any time to exit the game.\n

    Type \"start\" to begin the game.\n"




(*
    Takes multiline user input from the console 5 times and returns it as a string. If user types "exit" at any time, the function returns the input.
    If users types "next" the counter is increased by 1 and the function is called recursively.

    @return the user input in string format
*)
let takeUserInput () = 
    let rec takeUserInputHelper (counter : int) (input : string) = 

        let userInput = Console.ReadLine()
        let newInput = input + userInput + "\n"

        match counter, userInput with
        | 5, _ -> 
            printfn "You have reached the end of the game. Your result is being generated."
            input
        | _, "exit" -> 
            printfn "You have exited the game. Your result is being generated."
            input
        | _, "next" -> 
            printfn "You are now in %d" (2016 + counter)
            takeUserInputHelper (counter + 1) input
        | _, _ -> takeUserInputHelper counter newInput

    (takeUserInputHelper 1 "")




(*
    Removes all whitespace characters from a given string.

    @input: The string from which to remove whitespace.
    @Returns: A new string with all whitespace characters removed.
*)
let removeWhitespace (input: string) : string =
    input 
    |> Seq.filter (fun c -> not (System.Char.IsWhiteSpace(c)) || c = '\n') 
    |> Seq.toArray 
    |> String




(*
    Turns all characters in a given string to lowercase.

    @input: The string to turn lowercase.
    @Returns: A new string all lowercase.
*)
let toLower (input: string) : string =
    input.ToLower()




(*
    Formats the user input by removing all whitespace and turning all characters to lowercase.

    @input: The string to format.
    @Returns: A new string with all whitespace removed and all characters lowercase.
*)
let formatInput (input: string) : string =
    input |> removeWhitespace |> toLower



(*
    Starts the game

    @returns true if the user input is "start", false otherwise
*)
let startGame () = 
    let input = Console.ReadLine()
    if formatInput input = "start" then
        printfn "You have started the game. You are now in 2016."
        true
    else
        false




(*
    Displays game rules, reads the user input and returns it as a string.

    @return the user input in string format
*)
let startAndReadInput () = 
    printStartMessage ()
    let start = startGame ()
    if start = true then
        let input = takeUserInput ()
        formatInput input
    else
        usage ()





let displayOutput (output: int) = 
    printfn "Your result is %d" output
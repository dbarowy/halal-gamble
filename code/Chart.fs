module Chart
open Plotly.NET
open Aspose.Pdf
open Aspose.Pdf.Text
open System.Diagnostics

let years = [2015; 2016; 2017; 2018; 2019; 2020]



//-----------------FUNCTIONS-----------------//
let drawTimeseries (yearlyCapital: int list) (portfolioValueWithProfit: int list) =
    let timeseries = 
        [   Chart.Line(x = years, y = yearlyCapital, Name = "Yearly Capital Change")
            Chart.Line(x = years, y = portfolioValueWithProfit, Name = "Total Portfolio Value") ]
        |> Chart.combine
        |> Chart.withTitle "Time Series of Portfolio Value and Yearly Capital Change w/ Time (2015-2020)" 
        |> Chart.withXAxisStyle (TitleText = "Amount in $")
        |> Chart.withYAxisStyle (TitleText = "Time")
    timeseries


let drawBargraph (stocks: string list) (starts: int list) (ends: int list) =
    let bargraph = 
        [
            Chart.Column (starts, stocks, Name="Purchased Stock");
            Chart.Column (ends, stocks, Name="Result Stock w/ Profit/Loss")
        ]
        |> Chart.combine
        |> Chart.withTitle "Bar Graph of Individual Stock Transactions (2015-2020)"
        |> Chart.withXAxisStyle (TitleText = "Amount in $")
    bargraph


let drawPortfolio  (initial: int) (totalYearlyProfit: int list) (yearlyCapital: int list) (portfolioValueWithProfit: int list) (stocks: string list) (starts: int list) (ends: int list) =

    //Create pdf
    let pdf_doc = new Document()
    let page = pdf_doc.Pages.Add()


    //Build strings

    let date = System.DateTime.Now
    let netProfit = totalYearlyProfit |> List.sum  

    //i want to describe the strings like actually in a stock portfolio statement
    let title = "Stock Portfolio and Statement as of " + string date + "\n\n"
    let description = 
        "Your Initial Capital: $" + string initial + "\n\n" +
        "Net Profit: $" + string netProfit + "\n\n"


    let breakdowns = 
        "Your Yearly Capital by Years: \n\n" +
        "2015: $" + string yearlyCapital[0] + "\n" +
        "2016: $" + string yearlyCapital[1] + "\n" +
        "2017: $" + string yearlyCapital[2] + "\n" +
        "2018: $" + string yearlyCapital[3] + "\n" +
        "2019: $" + string yearlyCapital[4] + "\n" +
        "2020: $" + string yearlyCapital[5] + "\n\n" +

        "Your Total Yearly Profit by Years: \n\n" +
        "2015: $" + string totalYearlyProfit[0] + "\n" +
        "2016: $" + string totalYearlyProfit[1] + "\n" +
        "2017: $" + string totalYearlyProfit[2] + "\n" +
        "2018: $" + string totalYearlyProfit[3] + "\n" +
        "2019: $" + string totalYearlyProfit[4] + "\n" +
        "2020: $" + string totalYearlyProfit[5] + "\n\n" +

        "Your Portfolio Value with Profit by Years: \n\n" +
        "2015: $" + string portfolioValueWithProfit[0] + "\n" +
        "2016: $" + string portfolioValueWithProfit[1] + "\n" +
        "2017: $" + string portfolioValueWithProfit[2] + "\n" +
        "2018: $" + string portfolioValueWithProfit[3] + "\n" +
        "2019: $" + string portfolioValueWithProfit[4] + "\n" +
        "2020: $" + string portfolioValueWithProfit[5] + "\n\n" +

        "Your Stock Transactions: \n\n" +
        "Stock: " + string stocks[0] + "\n" +
        "Start: $" + string starts[0] + "\n" +
        "End: $" + string ends[0] + "\n\n" +
        "Stock: " + string stocks[1] + "\n" +
        "Start: $" + string starts[1] + "\n" +
        "End: $" + string ends[1] + "\n\n" +
        "Stock: " + string stocks[2] + "\n" +
        "Start: $" + string starts[2] + "\n" +
        "End: $" + string ends[2] + "\n\n"


    let footer = "Portfolio generated electronically by Halal Gambler at " + string date + "\n\n "


    let titleF = new TextFragment(title)
    let descriptionF = new TextFragment(description)
    let breakdownsF = new TextFragment(breakdowns)
    let footerF = new TextFragment(footer)

    // Styles
    titleF.TextState.Font <- FontRepository.FindFont("TimesNewRoman")
    titleF.TextState.FontSize <- 15.0f
    titleF.TextState.FontStyle <- FontStyles.Bold
    titleF.TextState.Underline <- true

    descriptionF.TextState.Font <- FontRepository.FindFont("TimesNewRoman")
    descriptionF.TextState.FontSize <- 11.0f

    breakdownsF.TextState.Font <- FontRepository.FindFont("TimesNewRoman")
    breakdownsF.TextState.FontSize <- 11.0f

    footerF.TextState.Font <- FontRepository.FindFont("TimesNewRoman")
    footerF.TextState.FontSize <- 8.0f
    footerF.TextState.FontStyle <- FontStyles.Italic



    page.Paragraphs.Add((titleF))
    page.Paragraphs.Add((descriptionF))
    page.Paragraphs.Add((breakdownsF))
    page.Paragraphs.Add((footerF))

    //Save and open pdf
    let timeNow = System.DateTime.Now
    let formattedTime = timeNow.ToString("HH-mm_dd-MM-yyyy tt")
    let filename = "Portfolio_Halal_" + formattedTime + ".pdf"
    pdf_doc.Save(filename)

    let psi = new ProcessStartInfo(filename)
    psi.UseShellExecute <- true
    Process.Start(psi) |> ignore


//-----------------MAIN-----------------//
let visualize (output: string list) (initial: int) (totalYearlyProfit: int list) (yearlyCapital: int list) (portfolioValueWithProfit: int list) (stocks: string list) (starts: int list) (ends: int list) =
    
    if List.contains "portfolio" output then
        drawPortfolio initial totalYearlyProfit yearlyCapital portfolioValueWithProfit stocks starts ends



    if List.contains "timeseries" output && List.contains "bargraph" output then
        let barGraph = drawBargraph stocks starts ends
        let timeSeries = drawTimeseries yearlyCapital portfolioValueWithProfit
        [timeSeries; barGraph] |> Chart.Grid(2, 1) |> Chart.show


    elif List.contains "timeseries" output then
        drawTimeseries yearlyCapital portfolioValueWithProfit  |> Chart.show


    elif List.contains "bargraph" output then
        drawBargraph stocks starts ends  |> Chart.show

    else
        ()


//-----------------EXAMPLE INPUT-----------------//

let initial = 1000
let output = ["timeseries"; "bargraph"; "portfolio"]

let totalYearlyProfit = [34 ; 41; -3; 16; -12; 8]
let yearlyCapital = [1000; 960; 850; 1070; 990; 590]

let portfolioValueWithProfit = [1000; 960; 850; 1034; 1010; 850]

//StockTransactions
let stocks = ["GOLD"; "TSLA"; "SLVR"] //MUST BE UPPERCASE
let starts = [100; 100; 100]
let ends = [110; 50; 50]

visualize output initial totalYearlyProfit yearlyCapital portfolioValueWithProfit stocks starts ends

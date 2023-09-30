open Hw4
open Hw4.Parser
open Hw4.Calculator

[<EntryPoint>]
let main args =
    try
        let optionsOfCalc = parseCalcArguments args
        let result = (calculate optionsOfCalc.arg1 optionsOfCalc.operation optionsOfCalc.arg2)
        printfn $"Result: {result}" 
    with e ->
        printfn $"Smth was wrong: {e.Message}"
    0
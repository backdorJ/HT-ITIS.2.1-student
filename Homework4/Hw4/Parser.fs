module Hw4.Parser

open System
open Hw4.Calculator
open Microsoft.FSharp.Core


type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation
}

let isArgLengthSupported (args : string[]) =
    args.Length = 3

let parseOperation (arg : string) =
    match arg with
    | "+" -> CalculatorOperation.Plus
    | "-" -> CalculatorOperation.Minus
    | "/" -> CalculatorOperation.Divide
    | "*" -> CalculatorOperation.Multiply
    | _ -> ArgumentException() |> raise
    
let parseStringToDouble(arg : string) =
    match Double.TryParse(arg) with
    | (true, value) -> value
    | _ -> ArgumentException("Incorrect numeric entry") |> raise    

let parseCalcArguments(args : string[]) =
    if not (isArgLengthSupported args) then
        ArgumentException("There must be three arguments!") |> raise
    let val1 = parseStringToDouble(args[0])
    let operation = parseOperation(args[1])
    let val2 = parseStringToDouble(args[2])
    {arg1 = val1; operation = operation; arg2 = val2 };
    
    
    
    
module Hw5.Parser

open System
open Hw5.Calculator
open Hw5.MaybeBuilder

let isArgLengthSupported (args:string[]): Result<'a,'b> =
    if args.Length <> 3 then
        Error Message.WrongArgLength
    else
        Ok args
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | Calculator.plus -> Ok(arg1, CalculatorOperation.Plus ,arg2)
    | Calculator.minus -> Ok(arg1, CalculatorOperation.Minus, arg2)
    | Calculator.divide -> Ok(arg1, CalculatorOperation.Divide, arg2)
    | Calculator.multiply -> Ok(arg1, CalculatorOperation.Multiply, arg2)
    | operation  -> Error Message.WrongArgFormatOperation

let parseStringToDouble (arg : string) =
    match Double.TryParse(arg) with
    | true, value -> Ok value
    | _ -> Error Message.WrongArgFormat

let rec parseArgs (args: string[]): Result<('a * CalculatorOperation * 'b), Message> =
    maybe {
        let! argsFinally =  (args.[0], args.[1], args.[2]) |> isOperationSupported
        let! arg1 = argsFinally.Item1 |> parseStringToDouble
        let operation = argsFinally.Item2
        let! arg2 = argsFinally.Item3 |> parseStringToDouble
        return (arg1, operation, arg2)
    }

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    if operation = CalculatorOperation.Divide && arg2 = 0.0 then
        Error Message.DivideByZero
    else Ok(arg1, operation , arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    maybe { 
        let! argLengthSupported = args |> isArgLengthSupported
        let! getParseArgs = argLengthSupported |> parseArgs
        let! dividingNotByZero = getParseArgs |> isDividingByZero
        
        return dividingNotByZero
    }    
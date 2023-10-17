module Hw6.Calculator.Calc
open System

type CalculatorOperation =
     | Plus = 0
     | Minus = 1
     | Multiply = 2
     | Divide = 3

[<Literal>] 
let Plus = "+"

[<Literal>] 
let Minus = "-"

[<Literal>] 
let Multiply = "*"

[<Literal>] 
let Divide = "/"

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline calculate (value1 : double, operation: string, value2:double) =
    match operation with
    | Plus -> Ok $"{value1 + value2}"
    | Minus -> Ok $"{value1 - value2}"
    | Multiply -> Ok $"{value1 * value2}"
    | Divide -> if value2 <> 0.0 then Ok $"{value1 / value2}"
                else Ok("DivideByZero")
    | _ -> Error $"Could not parse value '{operation}'"
    
let convertOperation operation =
    match operation with
    | "Plus" -> Plus
    | "Minus" -> Minus
    | "Multiply" -> Multiply
    | "Divide" -> Divide
    | _ -> operation
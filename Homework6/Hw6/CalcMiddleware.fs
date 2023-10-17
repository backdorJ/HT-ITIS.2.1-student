module Hw6.CalcMiddleware

open Giraffe
open Hw6.Calculator
open MaybeBuilder
open ArgsOfCalc
open Calc


let calculatorHandler: HttpHandler =
    fun next ctx ->
        let result: Result<string, string> = maybe {
            let! args = ctx.TryBindQueryString<ArgsOfCalc>()
            let! resultOfCalc = calculate (args.value1, args.operation |> convertOperation, args.value2)
            return resultOfCalc
        }

        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx

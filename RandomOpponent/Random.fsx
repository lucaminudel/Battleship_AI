open System

let (|Grep|_|) expr input =
  let r = System.Text.RegularExpressions.Regex.Match(input, expr)
  if r.Success then
    Some([for g in r.Groups -> g.Value].Tail)
  else
    None

let seed = new Random()
let rnd() = seed.Next(9)
let rndOrientation() = if rnd() % 2 = 0 then "horizontal" else "vertical"

while true do
  let input = Console.ReadLine()
  match input with
  | "get-name" -> Console.WriteLine("RandomF#")
  | "get-version" -> Console.WriteLine("0.1")
  | "new-game" -> ()
  | Grep(@"place-ship (\d)") res -> Console.WriteLine("{0} {1} {2}", rnd(), rnd(), rndOrientation())
  | "get-shot" -> Console.WriteLine("{0} {1}", rnd(), rnd())
  | "shot-hit" -> ()
  | Grep(@"shot-hit-and-sunk (\d) (\d) (\d) (\w)") res -> ()
  | "shot-miss" -> ()
  | Grep(@"opponent-shot (\d) (\d)") res -> ()
  | "game-won" -> ()
  | "game-lost" -> ()
  | "match-over" -> exit 0
  | _ -> Console.Error.WriteLine("unexpected input: " + input)

#load "Protocol.fsx"
open System
open Protocol

let seed = new System.Random()
let rnd() = seed.Next(9)
let rndOrientation() = if rnd() % 2 = 0 then "horizontal" else "vertical"
let mutable possibleShots = [] 

while true do
  let input = Console.ReadLine()
  match input with
  | GetName -> Console.WriteLine("RandomF#")
  | GetVersion -> Console.WriteLine("0.1")
  | NewGame ->
    possibleShots <- [for y = 0 to 9 do for x = 0 to 9 do yield (x,y)] |> List.sortWith (fun a b -> rnd() % 2)
  | PlaceShip(size) -> Console.WriteLine("{0} {1} {2}", rnd(), rnd(), rndOrientation())
  | GetShot ->
    let shot = possibleShots.Head
    possibleShots <- possibleShots.Tail
    Console.WriteLine("{0} {1}", fst shot, snd shot)
  | ShotHit -> ()
  | ShotHitAndSunk(ship) -> ()
  | ShotMiss -> ()
  | OpponentShot(location) -> ()
  | GameWon -> ()
  | GameLost -> ()
  | MatchOver -> exit 0
  | Unknown(input) -> Console.Error.WriteLine("unexpected input: " + input)

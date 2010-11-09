open System

let (|Grep|_|) expr input =
  let r = System.Text.RegularExpressions.Regex.Match(input, expr)
  if r.Success then
    Some([for g in r.Groups -> g.Value].Tail)
  else
    None

type Orientation =
| Vertical 
| Horizontal
  with static member Parse (input:string) = if input.ToLower() = "vertical" then Vertical else Horizontal
type Point = {x:int; y:int}
type Ship = {size:int; location:Point; orientation:Orientation}

let (|GetName|GetVersion|Unknown|) input = 
  match input with
  | "get-name" -> GetName
  | "get-version" -> GetVersion
  | _ -> Unknown(input)

let (|NewGame|PlaceShip|GameWon|GameLost|MatchOver|Unknown|) input = 
  match input with
  | "new-game" -> NewGame
  | Grep(@"place-ship (\d)") res -> PlaceShip(Int32.Parse(res.Head))
  | "game-won" -> GameWon
  | "game-lost" -> GameLost
  | "match-over" -> MatchOver
  | _ -> Unknown(input)

let (|GetShot|ShotHit|ShotMiss|ShotHitAndSunk|OpponentShot|Unknown|) input =
  match input with
  | "get-shot" -> GetShot
  | "shot-hit" -> ShotHit
  | Grep(@"shot-hit-and-sunk (\d) (\d) (\d) (\w)") res -> 
    ShotHitAndSunk({size = Int32.Parse(res.[0]); location = {x = Int32.Parse(res.[1]); y = Int32.Parse(res.[2])}; orientation = Orientation.Parse(res.[3])})
  | "shot-miss" -> ShotMiss
  | Grep(@"opponent-shot (\d) (\d)") res -> OpponentShot({x = Int32.Parse(res.[0]); y = Int32.Parse(res.[1])})
  | _ -> Unknown(input)

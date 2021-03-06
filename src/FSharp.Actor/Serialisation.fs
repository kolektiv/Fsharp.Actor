﻿namespace FSharp.Actor

open System.IO
open System.Runtime.Serialization.Formatters.Binary
open FSharp.Actor.Types

module Serialisers = 
    
    let Binary = 
        let formatter = new BinaryFormatter()
        let isEmpty (body:byte[]) = 
            Array.forall (fun b -> b = 0uy) body
        
        let serialise o =
            use ms = new MemoryStream()
            formatter.Serialize(ms, o)
            ms.ToArray()
        
        let deserialise body = 
            if not <| isEmpty body
            then
                use ms = new MemoryStream(body) 
                formatter.Deserialize(ms)
            else null

        { new ISerialiser with
              member x.Serialise(payload) = serialise payload
              member x.Deserialise(body) = deserialise body 
        }





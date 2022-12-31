module Smoosh.Attributes

open System

[<AttributeUsage(AttributeTargets.Property)>] type SmooshAttribute () = inherit Attribute ()
[<AttributeUsage(AttributeTargets.Property)>] type   Utf8Attribute () = inherit Attribute ()

type Attr = Utf8 | Smoosh

namespace functions

open Kubeless.Functions

type helloget = unit

module helloget =
    let rec handler (k8Event:Event, k8Context:Context) : string =
        "Hello " + k8Event.Data.ToString()
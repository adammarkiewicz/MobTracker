import React from "react";
import "./App.css";
import * as signalR from "@aspnet/signalr";

function App() {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl("/api")
    .configureLogging(signalR.LogLevel.Information)
    .build();

  connection.on("ReceiveLocation", location => {
    const div = document.createElement("div");
    div.textContent = "Received location: " + location;
    document.getElementById("app").appendChild(div);
  });

  connection.start().then(function() {
    console.log("Connected!!!");

    console.log("Sending location request!");
    connection.invoke("GetLocation");
  });
  return <div id="app" className="App"></div>;
}

export default App;

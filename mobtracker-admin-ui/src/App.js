import React from "react";
import "./App.css";
import * as signalR from "@aspnet/signalr";

function App() {
  const but = document.createElement("button");
  but.innerText = "Click";
  but.onclick = function changeContent() {
    fetch("/test")
      .then(response => {
        return response.text();
      })
      .then(myText => {
        console.log(myText);

        const div = document.getElementById("root");
        const item = document.createElement("div");
        item.innerText = myText;
        div.appendChild(item);
      });
  };
  document.getElementById("root").appendChild(but);

  const connection = new signalR.HubConnectionBuilder()
    .withUrl("/api")
    .configureLogging(signalR.LogLevel.Information)
    .build();

  connection.on("ReceiveLocation", location => {
    const div = document.createElement("div");
    div.textContent = "Received location: " + location;
    document.getElementById("root").appendChild(div);
  });

  connection.start().then(function() {
    console.log("Connected!!!");

    console.log("Sending location request!");
    connection.invoke("GetLocation");
  });
  return <div id="app" className="App"></div>;
}

export default App;
